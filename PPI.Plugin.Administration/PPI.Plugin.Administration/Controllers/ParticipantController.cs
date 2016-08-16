using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.Composition;

namespace PPI.Plugin.Administration.Controllers
{
    using MvcSiteMapProvider;
    using PPI.Platform.Mvc.Attributes;
    using PPI.Platform.Mvc.HtmlHelpers.Model;
    using PPI.Plugin.Administration.Models;
    using PPI.Plugin.Administration.Properties;
    using PPI.Platform.Core.Entities;
    using System.Net;

    [ExportController("Participant"), PartCreationPolicy(CreationPolicy.NonShared)]
    public class ParticipantController : PluginBaseController
    {
        // GET: Participants
        [MvcSiteMapNodeAttribute(Title = "Participants", ParentKey = "Home", Key = "Participants")]
        public ActionResult Index(int? page, string search)
        {
            
            var model = new PPI.Plugin.Administration.Models.ParticipantIndexViewModel();
            model.PluggableActions = this.PluggableActions;
            PagingInfo PagingInfo = new PagingInfo()
            {
                CurrentPage = page.GetValueOrDefault(1),
                PageCount = 5,
                PageSize = int.Parse(this.CurrentPageSize),                
            };

            if (search != null && search != "")
            {
                try
                {
                    //some search stuff                
                    PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                        .Where(search)
                        .Count();
                    model.Participants = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                        .Where(search)
                        .ToList().OrderBy(m => m.Participant.Person.Last_Name)
                        .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                        .Take(PagingInfo.PageSize);
                }
                catch
                {
                    PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().Count();
                    model.Participants = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                        .ToList().OrderBy(m => m.Participant.Person.Last_Name)
                        .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                        .Take(PagingInfo.PageSize);

                    ModelState.AddModelError(string.Empty, Resources_Administration_Web.View_Actions_SearchError);
                }
            }
            else
            {
                PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().Count();
                model.Participants = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable()
                    .ToList().OrderBy(m => m.Participant.Person.Last_Name)
                    .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                    .Take(PagingInfo.PageSize); 
            
            }
            if (model.Participants == null)
                model.Participants = new List<GroupParticipant>();                       

            model.PagingInfo = PagingInfo;
            
            return View(model);
        }
        [MvcSiteMapNodeAttribute(Title = "Add Participant", ParentKey = "Participants")]
        public ActionResult Create()
        {
            ViewBag.GroupId = new SelectList(_IPlatformUnitOfWork.IGroupRepository.AsQueryable(), "Id", "Name", null);
            return View();
        }
        [HttpPost]
        public ActionResult Create(GroupParticipant groupParticipant)
        {
            if (ModelState.IsValid)
            {
                _IPlatformUnitOfWork.IGroupParticipantRepository.Add(groupParticipant);
                _IPlatformUnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(groupParticipant); 
        }
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().FirstOrDefault(m => m.Id == Id);
            ViewBag.GroupId = new SelectList(_IPlatformUnitOfWork.IGroupRepository.AsQueryable(), "Id", "Name", model.GroupId);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(GroupParticipant groupParticipant)
        {
            var model = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().FirstOrDefault(m => m.Id == groupParticipant.Id);            
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
        public ActionResult Delete(int? groupParticipantId)
        {
            if (groupParticipantId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().FirstOrDefault(m => m.Id == groupParticipantId);
            if (model == null)
            {
                return HttpNotFound();
            }            
            var OnlyParticipantCheck = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().Count(m => m.ParticipantId == model.ParticipantId);
            var Person = model.Participant.Person;
            
            
            //Check if this is the only group the participant is in beacuse we will delete the Person if there is only this group we are removing
            //Referentral Integrety will take care of the rest.
            if (OnlyParticipantCheck == 1)
            {
                _IPlatformUnitOfWork.IPersonRepository.Delete(Person);                
            }
            else
            {
                _IPlatformUnitOfWork.IGroupParticipantRepository.Delete(model);            
            }
            _IPlatformUnitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}