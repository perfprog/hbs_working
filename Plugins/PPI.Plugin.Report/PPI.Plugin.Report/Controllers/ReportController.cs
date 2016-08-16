using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Linq.Dynamic;
using Microsoft.AspNet.Identity;
using System.Net;

namespace PPI.Plugin.Report.Controllers
{
    using MvcSiteMapProvider;
    using PPI.Platform.Core.Entities;
    using PPI.Platform.Mvc.Attributes;
    using PPI.Plugin.Report.Properties;
    using PPI.Plugin.Report.Models;
    using Platform.Mvc.HtmlHelpers.Model;
    using PPI.Platform.Mvc.Extentions;    
    using PPI.Platform.Core.Attributes;
    using System;
    using System.IO;
    using System.Web.Routing;
    using System.Configuration;
    using System.Text;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
        

    [ExportController("Report"), PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize(Roles="Admin,GroupAdmin")]
    public class ReportController : PluginBaseController
    {
        [MvcSiteMapNodeAttribute(Title = "Reports", ParentKey = "Home", Key = "Reports", Order = 6, Attributes = @"{""cssIcontype"":""fa fa-fw fa-bar-chart""}")] 
        public ActionResult Index(int? page,string search, string group ) 
        {
            var model = new ReportIndexViewModel();
            model.LockExportButton = false;
            PagingInfo PagingInfo = new PagingInfo()
            {
                CurrentPage = page.GetValueOrDefault(1),
                PageCount = 5,
                PageSize = int.Parse(this.CurrentPageSize),
                search = search
            };
            int selectedGroup = 0;
            if (string.IsNullOrWhiteSpace(group))
            {
                selectedGroup = this.CurrentProgram;
                model.LockExportButton = true;
            }
            if (search != null && search != "")
            {
                var pos = search.IndexOf("GroupId=");
                if (pos != -1)
                {
                    int.TryParse(search.Substring(pos + 8, search.Length - (pos + 8)), out selectedGroup);
                    this.SetCurrentProgram(selectedGroup);
                    // lock the send multi check boxes if there are multible groups
                    model.LockExportButton = true;
                }
                try
                {
                    if (User.IsInRole("GroupAdmin"))
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()                        
                        .Where(t => t.GroupParticipant.Group.CreatedBy == CurrentUserId)
                        .Where(search)
                        .Count();
                        model.SurveyGroupParticipants = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()                            
                            .Where(t => t.GroupParticipant.Group.CreatedBy == CurrentUserId)
                            .Where(search)
                            .ToList().OrderBy(m => m.GroupParticipant.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    else
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()                        
                        .Where(search)
                        .Count();
                        model.SurveyGroupParticipants = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()                            
                            .Where(search)
                            .ToList().OrderBy(m => m.GroupParticipant.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    
                }
                catch
                {
                    if (User.IsInRole("GroupAdmin"))
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()                        
                        .Where(t => t.GroupParticipant.Group.CreatedBy == CurrentUserId)
                        .Count();
                        model.SurveyGroupParticipants = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()                            
                            .Where(t => t.GroupParticipant.Group.CreatedBy == CurrentUserId)
                            .ToList().OrderBy(m => m.GroupParticipant.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    else
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()                        
                        .Count();
                        model.SurveyGroupParticipants = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()                            
                            .ToList().OrderBy(m => m.GroupParticipant.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    

                    ModelState.AddModelError(string.Empty,Resources_Report_Web.View_Actions_SearchError);
                }
            }
            else
            {
                if (User.IsInRole("GroupAdmin"))
                {
                    if (selectedGroup != 0)
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                        .Where(t => t.GroupParticipant.Group.CreatedBy == CurrentUserId)     
                        .Where(m => m.GroupParticipant.GroupId == selectedGroup)
                        .Count();
                        model.SurveyGroupParticipants = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                            .Where(t => t.GroupParticipant.Group.CreatedBy == CurrentUserId)
                            .Where(m => m.GroupParticipant.GroupId == selectedGroup)
                            .ToList().OrderBy(m => m.GroupParticipant.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    else
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                        .Where(t => t.GroupParticipant.Group.CreatedBy == CurrentUserId)
                        .Count();
                        model.SurveyGroupParticipants = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                            .Where(t => t.GroupParticipant.Group.CreatedBy == CurrentUserId)
                            .ToList().OrderBy(m => m.GroupParticipant.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    
                }
                else
                {
                    if (selectedGroup != 0)
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                           .Count();
                        model.SurveyGroupParticipants = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                            .Where(t => t.GroupParticipant.GroupId == selectedGroup)
                            .ToList().OrderBy(m => m.GroupParticipant.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    else
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                        .Count();
                        model.SurveyGroupParticipants = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                            .ToList().OrderBy(m => m.GroupParticipant.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    
                }
                
            }                        
            if (model.SurveyGroupParticipants.Count() == 0)
            {
                //wire up so no null faults on the labesl
                var clearGroup = new List<SurveyGroupParticipant>();
                clearGroup.Add(new SurveyGroupParticipant() { GroupParticipant = new GroupParticipant() });                                
                model.SurveyGroupParticipants = clearGroup;                
            }
            model.PagingInfo = PagingInfo;
            
            if (User.IsInRole("GroupAdmin"))
            {
                ViewBag.GroupListId = new SelectList(_IPlatformUnitOfWork.IGroupRepository.AsQueryable().Where(m => m.CreatedBy == this.CurrentUserId), "Id", "Name", selectedGroup);
            }
            else
            {
                ViewBag.GroupListId = new SelectList(_IPlatformUnitOfWork.IGroupRepository.AsQueryable(), "Id", "Name", selectedGroup);
            }
            return View(model);
        }
    
        public ActionResult GenerateNetworkSegmentReport(int participantId)
        {
            _HtmlToPdf.SetAllMargins(0);
            _HtmlToPdf.PageOrientation = Platform.Core.Pdf.PageOrientation.Landscape;
            _HtmlToPdf.PageSize = Platform.Core.Pdf.PageSize.Letter;
            var Root = Request.Url.GetLeftPart(UriPartial.Authority);
            var Participant = _IPlatformUnitOfWork.IParticipantRepository.AsQueryable().FirstOrDefault(m => m.Id == participantId);
            var ReportPath = Root + "/GenerateReport/NetworkSegmentReport?participantId=" + participantId;
            var GroupName = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().FirstOrDefault(m => m.ParticipantId == participantId);
            string Group = "";
            if (GroupName != null)
                Group = GroupName.Group.Name.Replace(" ", "_");
            byte[] buffer = _HtmlToPdf.ConvertUrlToByteArray(ReportPath);

            return File(buffer, "application/pdf", Participant.Person.Last_Name + "_" + Participant.Person.First_Name + "_" + Group + "_Network_Diagnostic_Survey.pdf");
        }

        public async Task<ActionResult> ExportType(string[] participantIds, string exportType)
        {
            ActionResult retVal = null;
            switch (exportType)
            { 
                case "Export" :                    
                    //Pass as temp data because the query string will get overloaded
                    if (!TempData.ContainsKey("participants"))
                        TempData.Add("participants",participantIds);                    
                    else
                        TempData["participants"] = participantIds;
                    return RedirectToAction("CompositeReport", "GenerateReport");   
                    break;
                case  "Download" :
                    DownloadNetworkSegmentReports(participantIds);                    
                    break;
            }
            return RedirectToAction("Index");
        }
        
        public async Task DownloadNetworkSegmentReports(string[] participantIds)
        {
            try
            {
                var baseAddress = ConfigurationManager.AppSettings["PPI.Platform.ReportServices"] + "Reports";
                var http = (HttpWebRequest)WebRequest.Create(new Uri(baseAddress));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";
                var ReportRequest = new PPI.Platform.Core.Models.RequestReports();
                var user = User.Identity.GetUserId();
                var emailaddress = _IPlatformUnitOfWork.IAspNetUserRepository.AsQueryable().FirstOrDefault(m => m.Id == user);
                ReportRequest.EmailAddress = emailaddress.Email;
                ReportRequest.ParticipantIds = participantIds;
                ReportRequest.ReportActionRoot = Request.Url.GetLeftPart(UriPartial.Authority);
                string parsedContent = JsonConvert.SerializeObject(ReportRequest);
                ASCIIEncoding encoding = new ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(parsedContent);

                Stream newStream = http.GetRequestStream();
                newStream.Write(bytes, 0, bytes.Length);
                newStream.Close();
                var response = await Task.Run(() => http.GetResponse());

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
