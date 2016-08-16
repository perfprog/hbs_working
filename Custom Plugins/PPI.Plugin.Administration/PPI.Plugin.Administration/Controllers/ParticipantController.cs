using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.Net;
using System.IO;
using System.Web;


namespace PPI.Plugin.Administration.Controllers
{
    using MvcSiteMapProvider;
    using PPI.Platform.Mvc.Attributes;
    using PPI.Platform.Mvc.HtmlHelpers.Model;
    using PPI.Plugin.Administration.Properties;
    using PPI.Platform.Core.Entities;
    using PPI.Plugin.Administration.Core.Infrastructure;
    using PPI.Platform.Mvc.Utility;
    using PPI.Platform.Core.Attributes;
    

    [ExportController("Participant"), PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    
    public class ParticipantController : PluginBaseController
    {
        // GET: Participants
        [Authorize(Roles = "GroupAdmin,Admin")]
        [MvcSiteMapNodeAttribute(Title = "Participants", ParentKey = "Home", Key = "Participants", Order = 2, Attributes = @"{""cssIcontype"":""fa fa-user""}")]
        public ActionResult Index(int? page, string search, string group)
        {
            
            var model = new PPI.Plugin.Administration.Models.ParticipantIndexViewModel();
            model.PluggableActions = this.PluggableActions;
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
                //for the LoadParticipants Dropdown
                
                if (search.IndexOf("GroupId=") != -1)
                {
                    int.TryParse(search.Substring(8, search.Length - 8), out selectedGroup);
                    this.SetCurrentProgram(selectedGroup);
                }
                try
                {
                    //some search stuff     
                    if (User.IsInRole("GroupAdmin"))
                    {
                        ViewBag.GroupIdList = new SelectList(_IPlatformUnitOfWork.IGroupRepository.AsQueryable().Where(m => m.CreatedBy == this.CurrentUserId), "Id", "Name", selectedGroup);

                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                            .Where(m => m.Group.CreatedBy == this.CurrentUserId)
                            .Where(search)
                            .Count();
                        model.Participants = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                            .Where(m => m.Group.CreatedBy == this.CurrentUserId)
                            .Where(search)
                            .ToList().OrderBy(m => m.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    else
                    {
                        ViewBag.GroupIdList = new SelectList(_IPlatformUnitOfWork.IGroupRepository.AsQueryable(), "Id", "Name", selectedGroup);

                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                            .Where(search)
                            .Count();
                        model.Participants = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                            .Where(search)
                            .ToList().OrderBy(m => m.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    
                }
                catch
                {
                    if (User.IsInRole("GroupAdmin"))
                    {
                        ViewBag.GroupIdList = new SelectList(_IPlatformUnitOfWork.IGroupRepository.AsQueryable().Where(m => m.CreatedBy == this.CurrentUserId), "Id", "Name", selectedGroup);
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                            .Where(m => m.Group.CreatedBy == this.CurrentUserId)
                            .Count();
                        
                        model.Participants = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                            .Where(m => m.Group.CreatedBy == this.CurrentUserId)
                            .ToList().OrderBy(m => m.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    
                    }
                    else
                    {
                        ViewBag.GroupIdList = new SelectList(_IPlatformUnitOfWork.IGroupRepository.AsQueryable(), "Id", "Name", selectedGroup);

                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().Count();
                    
                        model.Participants = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                            .ToList().OrderBy(m => m.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    
                    

                    ModelState.AddModelError(string.Empty, Resources_Administration_Web.View_Actions_SearchError);
                }
            }
            else
            {
                //modifications for group holding the group
                if (User.IsInRole("GroupAdmin"))
                {
                    ViewBag.GroupIdList = new SelectList(_IPlatformUnitOfWork.IGroupRepository.AsQueryable().Where(m => m.CreatedBy == this.CurrentUserId), "Id", "Name", selectedGroup);
                    if (selectedGroup != 0)
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                        .Where(m => m.Group.CreatedBy == this.CurrentUserId)
                        .Where(m => m.GroupId == selectedGroup)
                        .Count();
                        model.Participants = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                            .Where(m => m.Group.CreatedBy == this.CurrentUserId)
                            .Where(m => m.GroupId == selectedGroup)
                            .ToList().OrderBy(m => m.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    else
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                         .Where(m => m.Group.CreatedBy == this.CurrentUserId)                         
                         .Count();
                        model.Participants = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                            .Where(m => m.Group.CreatedBy == this.CurrentUserId)                         
                            .ToList().OrderBy(m => m.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                }
                else
                {
                    ViewBag.GroupIdList = new SelectList(_IPlatformUnitOfWork.IGroupRepository.AsQueryable(), "Id", "Name", selectedGroup);
                    if (selectedGroup != 0)
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                            .Where(m => m.GroupId == selectedGroup)
                            .Count();
                        model.Participants = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                            .Where(m => m.GroupId == selectedGroup)
                            .ToList().OrderBy(m => m.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    else
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()                            
                            .Count();
                        model.Participants = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()                            
                            .ToList().OrderBy(m => m.Participant.Person.Last_Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                }
                
            
            }
            if (model.Participants == null)
                model.Participants = new List<GroupParticipant>();                       

            model.PagingInfo = PagingInfo;
            model.AspNetUsers = _IPlatformUnitOfWork.IAspNetUserRepository.AsQueryable();
            //Track the Group
            this.SetCurrentProgram(selectedGroup);

            return View(model);
        }
        [MvcSiteMapNodeAttribute(Title = "Add Participant", ParentKey = "Participants", Attributes = @"{ ""visibility"": ""SiteMapPathHelper,!*"",""cssIcontype"":""fa fa-plus""}")]
        [Authorize(Roles = "GroupAdmin,Admin")]
        public ActionResult Create()
        {
            var model = new PPI.Platform.Core.Entities.GroupParticipant();
            model.AddedDate = DateTime.Now;
            IEnumerable<Group> NewSelectList;
            if (User.IsInRole("GroupAdmin"))
            {
                NewSelectList = _IPlatformUnitOfWork.IGroupRepository.AsQueryable().Where(m => m.CreatedBy == this.CurrentUserId);
            }
            else
            {
                NewSelectList = _IPlatformUnitOfWork.IGroupRepository.AsQueryable();
            }
            
            ViewBag.GroupId = new SelectList(NewSelectList,"Id","Name");
            // seed the modle            
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "GroupAdmin,Admin")]
        public ActionResult Create(GroupParticipant groupParticipant)
        {

            IEnumerable<Group> NewSelectList;
            if (User.IsInRole("GroupAdmin"))
            {
                NewSelectList = _IPlatformUnitOfWork.IGroupRepository.AsQueryable().Where(m => m.CreatedBy == this.CurrentUserId);              
            }
            else
            {
                NewSelectList = _IPlatformUnitOfWork.IGroupRepository.AsQueryable();            
            }
            if (ModelState.IsValid)
            {                
                //Need to lookup existing People / AspNet Users first   
                //Removed to allow same email address
               // var ExistingParticipant = _IPlatformUnitOfWork.IParticipantRepository.AsQueryable().FirstOrDefault(m => m.Person.Email == groupParticipant.Participant.Person.Email);
                //var ExistingPerson = _IPlatformUnitOfWork.IPersonRepository.AsQueryable().FirstOrDefault(m => m.Email == groupParticipant.Participant.Person.Email);
                //if (ExistingParticipant != null)
                //{
                //        ModelState.AddModelError("ParticipantExists", "Participant already exists in another group");
                //        ViewBag.GroupId = new SelectList(NewSelectList, "Id", "Name");            
                //        return View(groupParticipant);                 
                //}
                //if (ExistingPerson != null)
               // { 
                    //Person / AspNet Check
               //     groupParticipant.Participant = new Participant() { Person = ExistingPerson };                    
               // }                
                _IPlatformUnitOfWork.IGroupParticipantRepository.Add(groupParticipant);
                _IPlatformUnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.GroupId = new SelectList(NewSelectList, "Id", "Name");
            return View(groupParticipant); 
        }
        [Authorize(Roles = "GroupAdmin,Admin")]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupParticipant model;
            if (User.IsInRole("GroupAdmin"))
            {
                model = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().FirstOrDefault(m => m.Id == Id && m.Group.CreatedBy == this.CurrentUserId);
            }
            else
            {
                model = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().FirstOrDefault(m => m.Id == Id);
            }
            if (User.IsInRole("GroupAdmin"))
            {
                ViewBag.SelectGroup = new SelectList(_IPlatformUnitOfWork.IGroupRepository.AsQueryable().Where(m => m.CreatedBy == this.CurrentUserId), "Id", "Name", model.GroupId.ToString());
            }
            else
            {
                ViewBag.SelectGroup = new SelectList(_IPlatformUnitOfWork.IGroupRepository.AsQueryable(), "Id", "Name", model.GroupId.ToString());
            }
            
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "GroupAdmin,Admin")]
        public ActionResult Edit(GroupParticipant groupParticipant)
        {
            GroupParticipant model;
            if (User.IsInRole("GroupAdmin"))
            {
                model = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().FirstOrDefault(m => m.Id == groupParticipant.Id && m.Group.CreatedBy == this.CurrentUserId);            
            }
            else
            {
                 model = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().FirstOrDefault(m => m.Id == groupParticipant.Id);            
            }
            
            if (model == null)
            {
                return HttpNotFound();
            }
            model.Participant.Person.First_Name = groupParticipant.Participant.Person.First_Name;
            model.Participant.Person.Last_Name = groupParticipant.Participant.Person.Last_Name;
            model.Participant.Person.Email = groupParticipant.Participant.Person.Email;
            model.GroupId = groupParticipant.GroupId;
            _IPlatformUnitOfWork.IGroupParticipantRepository.Update(model);
            _IPlatformUnitOfWork.Commit();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "GroupAdmin,Admin")]
        public ActionResult Delete(int? groupParticipantId)
        {
            if (groupParticipantId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GroupParticipant model;
            if (User.IsInRole("GroupAdmin"))
            {
                model = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().FirstOrDefault(m => m.Id == groupParticipantId && m.Group.CreatedBy == this.CurrentUserId);
            }
            else
            {
                model = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().FirstOrDefault(m => m.Id == groupParticipantId);
            }

            
            if (model == null)
            {
                return HttpNotFound();
            }            
            var OnlyParticipantCheck = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().Count(m => m.ParticipantId == model.ParticipantId);
            var Person = model.Participant.Person;
            
            if (OnlyParticipantCheck == 1)
            {                
                _IPlatformUnitOfWork.IPersonRepository.Delete(Person);
                _IPlatformUnitOfWork.IParticipantRepository.Delete(model.Participant);                
            }
            else
            {
                _IPlatformUnitOfWork.IParticipantRepository.Delete(model.Participant);    
            }
            _IPlatformUnitOfWork.Commit();
            return RedirectToAction("Index");
        }        
        [HttpPost]
        [Authorize(Roles = "GroupAdmin,Admin")]
        public ActionResult Load(int SelectedGroupId)
        {
            int i = 0;
            string savedFileName = "";
            foreach (string keyItem in Request.Files)
            {
                HttpPostedFileBase httpitem = Request.Files[keyItem] as HttpPostedFileBase;
                SecureHttpPostedFile item = new SecureHttpPostedFile(httpitem);
                if (item.ContentLength == 0)
                    continue; //will skip the rest 

                savedFileName = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadedFiles", System.IO.Path.GetFileName(item.FileName));
                //Use Encyption Classes                
                item.SaveAs(savedFileName);
                FileInfo fileInfo = new FileInfo(savedFileName);                    
                if (fileInfo.Exists)
                {
                    using (var reader = new CsvHelper.CsvReader(SecureHttpPostedFile.SecureStream(savedFileName)))
                    {                        
                        reader.Configuration.RegisterClassMap<ImportMaps.Participant_Map>();
                        var records = reader.GetRecords<Person>().ToList();
                        foreach (var personitem in records)
                        {
                            var participant = new Participant();
                            var person = new Person() { First_Name = personitem.First_Name, Last_Name = personitem.Last_Name, Email = personitem.Email };
                            participant.Person = person;
                            
                            var newGroupParticipant = new GroupParticipant();
                            newGroupParticipant.Participant = participant;
                            newGroupParticipant.GroupId = SelectedGroupId;

                            //var ExistingParticipant = _IPlatformUnitOfWork.IParticipantRepository.AsQueryable().FirstOrDefault(m => m.Person.Email == personitem.Email);
                            //var ExistingPerson = _IPlatformUnitOfWork.IPersonRepository.AsQueryable().FirstOrDefault(m => m.Email == personitem.Email);
                            //if (ExistingParticipant != null)
                            //{
                            //    // do not add they already exist in this group
                            //    continue;
                            //}
                            //else if (ExistingPerson != null)
                            //{
                            //    //Person / AspNet Check
                            //    newGroupParticipant.Participant = new Participant() { Person = ExistingPerson };
                            //}                                                       
                            //_IPlatformUnitOfWork.IParticipantRepository.Add(participant);                            
                            _IPlatformUnitOfWork.IGroupParticipantRepository.Add(newGroupParticipant);                                
                        }
                        _IPlatformUnitOfWork.Commit();
                        
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}