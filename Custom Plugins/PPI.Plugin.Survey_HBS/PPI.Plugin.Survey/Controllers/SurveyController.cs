using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.Linq.Dynamic;
using System.Collections.Generic;

namespace PPI.Plugin.Survey.Controllers
{
    using MvcSiteMapProvider;
    using PPI.Platform.Mvc.Attributes;
    using PPI.Plugin.Survey.Models;
    using PPI.Platform.Mvc.Extentions;
    using PPI.Platform.Mvc.HtmlHelpers.Model;
    using PPI.Platform.Core.Entities;
    using PPI.Plugin.Survey.Properties;
    using PPI.Plugin.Survey.Core.Extentions;
    using PPI.Platform.Core.Attributes;
    using PPI.Platform.Core.Attributes;
        

    [ExportController("Survey"), PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    public class SurveyController : PluginBaseController
    {
    
        // GET: Administration        
        
        [Authorize(Roles = "GroupAdmin,Admin")]
        [RestoreModelStateFromTempData]   
        public ActionResult Index(int? page,int? surveyId, string search,string group)
        {                                   
            var model = new SurveyIndexViewModel();
            model.LockSendAllButtons = true;
            
            // get the model error from CheckIndex
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
            }
            if (search != null && search != "")
            {
                var pos = search.IndexOf("GroupId=");
                if (pos != -1)
                {
                    int.TryParse(search.Substring(pos + 8, search.Length - (pos + 8)), out selectedGroup);
                    this.SetCurrentProgram(selectedGroup);
                    // lock the send multi check boxes if there are multible groups
                    model.LockSendAllButtons = false;
                }

                try
                {
                    if (User.IsInRole("GroupAdmin"))
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                        .Where(m => m.SurveyId == surveyId || !surveyId.HasValue)
                        .Where(t => t.GroupParticipant.Group.CreatedBy == CurrentUserId)
                        .Where(search)
                        .Count();
                        model.SurveyGroupParticipants = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                            .Where(m => m.SurveyId == surveyId || !surveyId.HasValue)
                            .Where(t => t.GroupParticipant.Group.CreatedBy == CurrentUserId)
                            .Where(search)
                            .ToList().OrderBy(m => m.GroupParticipant.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    else
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                        .Where(m => m.SurveyId == surveyId || !surveyId.HasValue)
                        .Where(search)
                        .Count();
                        model.SurveyGroupParticipants = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                            .Where(m => m.SurveyId == surveyId || !surveyId.HasValue)
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
                        .Where(m => m.SurveyId == surveyId || !surveyId.HasValue)
                        .Where(t => t.GroupParticipant.Group.CreatedBy == CurrentUserId)
                        .Count();
                        model.SurveyGroupParticipants = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                            .Where(m => m.SurveyId == surveyId || !surveyId.HasValue)
                            .Where(t => t.GroupParticipant.Group.CreatedBy == CurrentUserId)
                            .ToList().OrderBy(m => m.GroupParticipant.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    else
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                        .Where(m => m.SurveyId == surveyId || !surveyId.HasValue)
                        .Count();
                        model.SurveyGroupParticipants = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                            .Where(m => m.SurveyId == surveyId || !surveyId.HasValue)
                            .ToList().OrderBy(m => m.GroupParticipant.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    

                    ModelState.AddModelError(string.Empty, Resources_Survey_Web.View_Actions_SearchError);
                }
            }
            else
            {
                if (User.IsInRole("GroupAdmin"))
                {
                    if (selectedGroup != 0)
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                        .Where(m => m.SurveyId == surveyId || !surveyId.HasValue)
                        .Where(t => t.GroupParticipant.Group.CreatedBy == CurrentUserId)
                        .Where(z => z.GroupParticipant.GroupId == selectedGroup)
                        .Count();
                        model.SurveyGroupParticipants = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                            .Where(m => m.SurveyId == surveyId || !surveyId.HasValue)
                            .Where(t => t.GroupParticipant.Group.CreatedBy == CurrentUserId)
                            .Where(z => z.GroupParticipant.GroupId == selectedGroup)
                            .ToList().OrderBy(m => m.GroupParticipant.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    else
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                        .Where(m => m.SurveyId == surveyId || !surveyId.HasValue)
                        .Where(t => t.GroupParticipant.Group.CreatedBy == CurrentUserId)
                        .Count();
                        model.SurveyGroupParticipants = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                            .Where(m => m.SurveyId == surveyId || !surveyId.HasValue)
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
                            .Where(m => m.SurveyId == surveyId || !surveyId.HasValue)
                            .Where(z => z.GroupParticipant.GroupId == selectedGroup)
                            .Count();
                        model.SurveyGroupParticipants = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                            .Where(m => m.SurveyId == surveyId || !surveyId.HasValue)
                            .Where(z => z.GroupParticipant.GroupId == selectedGroup)
                            .ToList().OrderBy(m => m.GroupParticipant.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    else
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                        .Where(m => m.SurveyId == surveyId || !surveyId.HasValue)
                        .Count();
                        model.SurveyGroupParticipants = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable()
                            .Where(m => m.SurveyId == surveyId || !surveyId.HasValue)
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
                ViewBag.GroupIdList = new SelectList(_IPlatformUnitOfWork.IGroupRepository.AsQueryable().Where(m => m.CreatedBy == this.CurrentUserId), "Id", "Name", selectedGroup);
            }
            else
            {
                ViewBag.GroupIdList = new SelectList(_IPlatformUnitOfWork.IGroupRepository.AsQueryable(), "Id", "Name", selectedGroup);
            }
            
            //var list = _IPlatformUnitOfWork.IGenericTypeRepository.GetGenericTypes(this.CurrentCulture,"SurveyParticipant_StatusType",null);            
            return View(model);
        }
        // GET: Administration/Create
        [HttpGet]
        [Authorize(Roles = "GroupAdmin,Admin")]
        //Hard Coded for HBS has only one survey
        [MvcSiteMapNodeAttribute(Title = "Survey", ParentKey = "Home", Key = "Survey", Order = 4,  Attributes = @"{""surveyId"":""1"",""cssIcontype"":""fa fa-fw fa-check""}")] 
        [SetTempDataModelStateAttribute]
        public ActionResult CheckIndex(int? surveyId)
        {
            //Check for groups that are not in the Survey and add them 1010 (not invited
            if (User.IsInRole("GroupAdmin"))
            {
                if (surveyId.HasValue)
                {
                    var GroupParticipantsNotInSurvey = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                        .Where(s => s.Group.CreatedBy == this.CurrentUserId)
                        .Where(t => t.Group.GroupEmails.Any()) // Dont generate surveys if the email haven't been setup //again tied to email plugin
                        .Where(m => m.SurveyGroupParticipants.Count == 0);

                    var GroupParticipantIds = GroupParticipantsNotInSurvey.Select(m => m.Id).ToList();
                    GenerateSurveyGroupParticipants(GroupParticipantIds, surveyId.Value);
                }
                //send a model error if there are groups without Email setups
                var GroupsWithNoEmail = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                    .Select(m => m.Group)
                    .Where(t => t.CreatedBy == this.CurrentUserId)
                    .Where(m => m.GroupEmails.Count == 0);
                if (GroupsWithNoEmail.Count() > 0)
                {
                    ModelState.AddModelError(string.Empty, Resources_Survey_Web.Survey_Index_NoEmail);
                }
                var PerserveModelState = new ModelStateDictionary();
                PerserveModelState.Merge(ModelState);
                TempData["ModelErrors"] = PerserveModelState;
            }
            else
            {
                if (surveyId.HasValue)
                {
                    var GroupParticipantsNotInSurvey = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                        .Where(t => t.Group.GroupEmails.Any()) // Dont generate surveys if the email haven't been setup //again tied to email plugin
                        .Where(m => m.SurveyGroupParticipants.Count == 0);

                    var GroupParticipantIds = GroupParticipantsNotInSurvey.Select(m => m.Id).ToList();
                    GenerateSurveyGroupParticipants(GroupParticipantIds, surveyId.Value);
                }
                //send a model error if there are groups without Email setups
                var GroupsWithNoEmail = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                    .Select(m => m.Group).Where(m => m.GroupEmails.Count == 0);
                if (GroupsWithNoEmail.Count() > 0)
                {
                    ModelState.AddModelError(string.Empty, Resources_Survey_Web.Survey_Index_NoEmail);
                }
                var PerserveModelState = new ModelStateDictionary();
                PerserveModelState.Merge(ModelState);
                TempData["ModelErrors"] = PerserveModelState;
            }
            
            
            
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "GroupAdmin,Admin,Participant")]
        public void SurveySetStatus(List<int> participantId, int statusId)
        {
            _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.SurveySetStatus(participantId, statusId);
            _IPlatformUnitOfWork.Commit();        
        }
        [Authorize(Roles = "Participant")]
        public ActionResult SurveyComplete(List<int> participantId,int statusId)
        {
            _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.SurveySetStatus(participantId, statusId);
            _IPlatformUnitOfWork.Commit();
            return View();
        }
        protected void GenerateSurveyGroupParticipants(List<int> GroupParticipantIds, int surveyId)
        { 
            foreach (var item in GroupParticipantIds)
	        {
                //hard coded 1010 as the type for not invited
                _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.Add(new SurveyGroupParticipant() { SurveyId = surveyId, GroupParticipantId = item, StatusGenericTypeValueId = 1010});
	        }
            _IPlatformUnitOfWork.Commit();            
        }
        [Authorize(Roles = "GroupAdmin,Admin")]
        public string DashboardCount(string type)
        {
            var retval = "";
            if (type == "surveyreturnCount")
            {

                if (User.IsInRole("GroupAdmin"))
                {
                    retval = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable().Where(m => m.GroupParticipant.Group.CreatedBy == this.CurrentUserId).Count().ToString();
                }
                else
                {
                    retval = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable().Count().ToString();
                }                
            }
            return retval;
        }
        [Authorize(Roles = "Participant")]
        public ActionResult DashboardParticipant(string aspNetUsersId)
        {
            var model = _IPlatformUnitOfWork.ISurveyGroupParticipantRepository.AsQueryable().Where(m => m.GroupParticipant.Participant.Person.AspNetUsersId == aspNetUsersId);
            return PartialView("_SurveyParticipant", model);
        }
    }
}
