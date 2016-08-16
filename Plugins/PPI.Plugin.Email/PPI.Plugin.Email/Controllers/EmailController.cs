using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace PPI.Plugin.Email.Controllers
{
    using MvcSiteMapProvider;
    using PPI.Platform.Core.Entities;
    using PPI.Platform.Mvc.Attributes;
    using PPI.Plugin.Email.Properties;
    using PPI.Plugin.Email.Models;
    using PPI.Platform.Mvc.HtmlHelpers.Model;
    using PPI.Platform.Mvc.Extentions;
    using PPI.Platform.Core.Email.Model;
    using PPI.Platform.Core.Utility;
    using PPI.Plugin.Email.Infrastructure;

    using System;

    
    [ExportController("Email"), PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    public class EmailController : PluginBaseController
    {        
        // GET: Administration                        
        [MvcSiteMapNodeAttribute(Title = "Email", ParentKey = "Home", Key = "Email", Order = 3, Attributes = @"{""cssIcontype"":""fa fa-fw fa-envelope""}")]
        [Authorize(Roles = "GroupAdmin,Admin")]
        public ActionResult CheckIndex()
        {
            ///check forewroups and create records for Email 
            ///then redirect tondex forormalperations
            IQueryable<Group> GroupsWithOutEmail;
            if (User.IsInRole("GroupAdmin"))
            {
                GroupsWithOutEmail = _IPlatformUnitOfWork.IGroupRepository.AsQueryable().Where(t => t.CreatedBy == this.CurrentUserId).Where(m => m.GroupEmails.Count == 0);
            }
            else
            {
                GroupsWithOutEmail = _IPlatformUnitOfWork.IGroupRepository.AsQueryable().Where(m => m.GroupEmails.Count == 0);
            }
            
           List<int> groupIds =new List<int>();
           foreach (var item in GroupsWithOutEmail)
           {
               groupIds.Add(item.Id);
           }
           GenerateDefaultEmailTemplateGroups(groupIds);

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "GroupAdmin,Admin")]
        public ActionResult Index(int? page)
        {
            var model =new EmailIndexViewModel();
            var ModelGroup =new List<EmailGrouping>();
            PagingInfo PagingInfo =new PagingInfo()
            {
                CurrentPage = page.GetValueOrDefault(1),
                PageCount = 5,
                PageSize =int.Parse(this.CurrentPageSize),
            };
           IEnumerable<Group> EmailGroups;
            if (User.IsInRole("GroupAdmin"))
            {
                EmailGroups = _IPlatformUnitOfWork.IGroupEmailRepository.AsQueryable().Select(m => m.Group).Distinct()
                 .Where(t => t.CreatedBy == this.CurrentUserId)
                 .ToList()
                 .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                 .Take(PagingInfo.PageSize);

                PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupEmailRepository.AsQueryable()
                    .Where(t => t.Group.CreatedBy == this.CurrentUserId)
                    .Select(m => m.Group).Distinct().Count();
            }
            else
            {
                EmailGroups = _IPlatformUnitOfWork.IGroupEmailRepository.AsQueryable().Select(m => m.Group).Distinct()                     
                     .ToList()
                     .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                     .Take(PagingInfo.PageSize);

                PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupEmailRepository.AsQueryable().Select(m => m.Group).Distinct().Count();
            }
            

            foreach (var item in EmailGroups)
	        {
                var CurrentGroup =new EmailGrouping();
                string EmailTypes= "";
                CurrentGroup.Group =item;
                foreach (var itemEmail in item.GroupEmails)
                {
                    EmailTypes = EmailTypes + " " +itemEmail.Email.EmailGenericTypeValue.Resource.ResourceValues.FirstOrDefault(m => m.CultureId == this.CurrentCulture).Value;
                }
                CurrentGroup.EmailTypes = EmailTypes;
                ModelGroup.Add(CurrentGroup);                
	        }
            model.GroupEmails = ModelGroup;                                    
            model.PagingInfo = PagingInfo;                        
                         
            return View(model);
        }
        [Authorize(Roles = "GroupAdmin,Admin")]
        public ActionResult Edit(int? groupId)
        {            
            var model =new EmailEditViewModel();
           if (groupId !=null)
            {
                if (User.IsInRole("GroupAdmin"))
                {
                    model.Emails = _IPlatformUnitOfWork.IGroupEmailRepository.AsQueryable()
                        .Where(t => t.Group.CreatedBy == this.CurrentUserId)
                        .Where(s => s.Email.EmailGenericTypeValueId != 10) //Reports
                        .Where(m => m.GroupId == groupId).Select(m => m.Email);
                    model.Group = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                        .Where(t => t.CreatedBy == this.CurrentUserId)
                        .FirstOrDefault(m => m.Id == groupId);
                }
                else
                {
                    model.Emails = _IPlatformUnitOfWork.IGroupEmailRepository.AsQueryable()
                        .Where(s => s.Email.EmailGenericTypeValueId != 10) //Reports
                        .Where(m => m.GroupId == groupId).Select(m => m.Email);
                    model.Group = _IPlatformUnitOfWork.IGroupRepository.AsQueryable().FirstOrDefault(m => m.Id == groupId); 
                }
               
            }
           if (model.Group ==null || model.Emails.Count() == 0)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "GroupAdmin,Admin")]
        [ValidateInput(false)]
        public ActionResult Edit(EmailEditPostViewModel model)
        {
           if (model.email ==null || model.email.Id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }
           if (ModelState.IsValid)
            {
                var OrginalEmail = _IPlatformUnitOfWork.IEmailRepository.AsQueryable().FirstOrDefault(m => m.Id == model.email.Id);
                OrginalEmail.DefaultFrom = model.email.DefaultFrom;
                OrginalEmail.Closing = model.email.Closing;
                OrginalEmail.Introduction = model.email.Introduction;
                OrginalEmail.Subject = model.email.Subject;
                _IPlatformUnitOfWork.IEmailRepository.Update(OrginalEmail);
                _IPlatformUnitOfWork.Commit();                
            }

            return RedirectToAction("Edit",new { @groupid = model.groupid });
        }        
        [Authorize(Roles = "GroupAdmin,Admin")]
        public ActionResult CheckEdit(int? groupId)
        {
            ///check forroup and create records for Email 
            ///then redirect tondex forormalperations
            ///
           if (groupId !=null)
            {
                var HasEmailSetup = _IPlatformUnitOfWork.IGroupEmailRepository.AsQueryable().Where(m => m.GroupId == groupId);
               if (HasEmailSetup.Count() == 0)
                {                    
                    //create defaults
                    _Logging.LogInfo("Setting Up Email Defaults for Group {0}", groupId);
                    GenerateDefaultEmailTemplateGroups(new List<int>(){groupId.GetValueOrDefault()});
                }               
            }

            return RedirectToAction("Edit",new { @groupId =groupId });
        }
        [Authorize(Roles = "GroupAdmin,Admin")]
        private void GenerateDefaultEmailTemplateGroups(List<int> groupIds)
        {            
            foreach (var groupIditem in groupIds)
            {
                var EmailTypes = _IPlatformUnitOfWork.IGenericTypeRepository.GetGenericTypes(this.CurrentCulture, "Email_EmailType", null);
                foreach (var item in EmailTypes)
                {
                    var Email =new Email();
                    Email.Closing = Resources_Email_Web.Email_Closing;
                    Email.DefaultFrom = Resources_Email_Web.Email_From;
                    Email.EmailGenericTypeValueId =int.Parse(item.Value); //Invititations
                    Email.Introduction = Resources_Email_Web.Email_Intro;
                    Email.Subject = Resources_Email_Web.Email_Subject;
                    Email.Template = false;                    
                    _IPlatformUnitOfWork.IEmailRepository.Add(Email);
                    var GroupEmail =new GroupEmail() { GroupId = groupIditem, Email = Email };
                    _Logging.LogDebug("Setting Up Email Defaults for Group {0} and EmailType {1}", groupIditem, item.Value);
                    _IPlatformUnitOfWork.IGroupEmailRepository.Add(GroupEmail);
                }
                _IPlatformUnitOfWork.Commit();
            }                                   
        }
        [HttpPost]
        [Authorize(Roles = "GroupAdmin,Admin")]
        public ActionResult SendEmail(List<int> recipients,List<int> groupEmailId)
        {
            var model =new EmailSendEmailPartialViewModel();            
            model.Sent = 0;
            model.Failed = 0;
            int CurrentEmailId = groupEmailId[0];//right now will only be one
            var EmailModel = _IPlatformUnitOfWork.IGroupEmailRepository.AsQueryable().FirstOrDefault(m => m.Id == CurrentEmailId).Email; 
            var GroupEmail = _IPlatformUnitOfWork.IGroupEmailRepository.AsQueryable().FirstOrDefault(m => m.Id == CurrentEmailId);
            
            //Commenting out until Secuty page is in place
            var Credentials = new NetworkCredential(
                MachineKeyWrapper.UnprotectUrlSafeString(ConfigurationManager.AppSettings["mailAccount"], "configuration"),
                MachineKeyWrapper.UnprotectUrlSafeString(ConfigurationManager.AppSettings["mailPassword"], "configuration")
            );
            
            var Configuration = new EmailConfiguration() {Credentials = Credentials };
            foreach (var item in recipients)
            {
                var Message = new MailMessage();
                Message.Body = EmailModel.Introduction;
                // Special Case for Invites to Send the Self Registration Page Link
                var CurrentParticipant = _IPlatformUnitOfWork.IPersonRepository.FirstOrDefault(m => m.Id == item).Participants.FirstOrDefault();
                if (EmailModel.EmailGenericTypeValueId == 8 || EmailModel.EmailGenericTypeValueId == 9) //Invititaions //Reminder
                { 
                    //generate a token and append the link for self register to the email                                                          
                        var Token = MachineKeyWrapper.ProtectUrlSafeString("Authenticated", CurrentParticipant.Id.ToString());
                        var SelfRegister = Url.Action("RegisterParticipant", "Account", new { participantid = CurrentParticipant.Id, Token = Token }, protocol: Request.Url.Scheme);
                        Message.Body += "<p>To begin the survey, click on the registration link below. You will be required to enter the unique username provided in this email, and to create a password. <b><font color='red'>This username and link are unique to you, so please do not forward or share.</font></b></p>";
                        Message.Body += "<p>We recommend that you write down your username and password, as you will need these in order to return to the survey if you exit at any point.  If you have already registered, the link below will automatically redirect you to the login page.</p>";
                        Message.Body += "<p><strong>User Name:</strong> HBS" + CurrentParticipant.Id.ToString() + "</p>";
                        Message.Body += "<p>Registration/Login: <a href=\"" + SelfRegister + "\">Click here</a></p>";                  
                }
                    Message.Body += EmailModel.Closing;
                //
                Message.IsBodyHtml = true;
                Message.To.Add(new MailAddress(_IPlatformUnitOfWork.IPersonRepository.GetById(item).Email));
                Message.From = new MailAddress(EmailModel.DefaultFrom);
                Message.Subject = EmailModel.Subject;
                try
                {
                    Message.Bcc.Add(new MailAddress(ConfigurationManager.AppSettings["bccAccount"]));
                    Message.Bcc.Add(new MailAddress(GroupEmail.Group.EmailAddress));
                }
                catch
                {
                    //continue
                }
                //Record Inprocess       
                RecordEmail.Update(Message, CurrentParticipant.Person, groupEmailId[0], 1236, _IPlatformUnitOfWork,""); // Inprocess
                _IEmail.Send(Message, Configuration);
                RecordEmail.Update(Message, CurrentParticipant.Person, groupEmailId[0], 1237, _IPlatformUnitOfWork, ""); // complete
                //Record Complete
                if (_IEmail.HasErrors)
                {
                    model.Failed++;
                    RecordEmail.Update(Message, CurrentParticipant.Person, groupEmailId[0], 1238, _IPlatformUnitOfWork,_IEmail.ErrorMessage); // errors
                }
                else
                    model.Sent++;
            }

            return PartialView("_SendEmailPartial",model);
        }

        public ActionResult PreviewEmail(int emailid)
        {
            var model = new EmailPreviewViewModel();
            var EmailModel = _IPlatformUnitOfWork.IEmailRepository.AsQueryable().FirstOrDefault(m => m.Id == emailid);
            model.Introduction = EmailModel.Introduction;
            model.Closing = EmailModel.Closing;
            
            model.StandardText += "<p>To begin the survey, click on the registration link below. You will be required to enter the unique username provided in this email, and to create a password. <b><font color='red'>This username and link are unique to you, so please do not forward or share.</font></b></p>";
            model.StandardText += "<p>We recommend that you write down your username and password, as you will need these in order to return to the survey if you exit at any point.  If you have already registered, the link below will automatically redirect you to the login page.</p>";
            model.StandardText += "<p><strong>User Name:</strong> HBS" + "XXXX" + "</p>";
            model.StandardText += "<p>Registration/Login: <a href=\"" + "XXXXX" + "\">Click here</a></p>";     
            return View(model);
        }
    }
}
