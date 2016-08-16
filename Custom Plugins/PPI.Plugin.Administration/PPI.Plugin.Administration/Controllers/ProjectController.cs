using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.Net;
using Microsoft.AspNet.Identity;

namespace PPI.Plugin.Administration.Controllers
{
    using MvcSiteMapProvider;
    using PPI.Platform.Mvc.Attributes;
    using PPI.Platform.Mvc.HtmlHelpers.Model;
    using PPI.Plugin.Administration.Models;
    using PPI.Plugin.Administration.Properties;
    using PPI.Platform.Core.Entities;
    using PPI.Platform.Core.Attributes;
        
    [ExportController("Project"), PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize]
    public class ProjectController : PluginBaseController
    {        
        // GET: Project
        [Authorize(Roles = "GroupAdmin,Admin")]
        [MvcSiteMapNodeAttribute(Title = "Program", ParentKey = "Home", Key = "Project", Order = 1,  Attributes = @"{""cssIcontype"":""fa fa-folder-open""}")]
      
        public ActionResult Index(int? page, string search)
        {
            var model = new ProjectIndexViewModel();
            PagingInfo PagingInfo = new PagingInfo()
            {
                CurrentPage = page.GetValueOrDefault(1),
                PageCount = 5,
                PageSize = int.Parse(this.CurrentPageSize),
                search = search
            };

            if (search != null && search != "")
            {
                try
                {
                    if (User.IsInRole("GroupAdmin"))
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                        .Where(m => m.CreatedBy == this.CurrentUserId)
                        .Where(search)
                        .Count();

                        model.Projects = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                        .Where(m => m.CreatedBy == this.CurrentUserId)
                        .Where(search)
                        .ToList().OrderBy(m => m.Name)
                        .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                        .Take(PagingInfo.PageSize);
                    }
                    else
                    { 
                    PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                    .Where(search)
                    .Count();
                    model.Projects = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                        .Where(search)
                        .ToList().OrderBy(m => m.Name)
                        .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                        .Take(PagingInfo.PageSize);
                    }
                }
                catch
                {
                    if (User.IsInRole("GroupAdmin"))
                    {
                        PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                            .Where(m => m.CreatedBy == this.CurrentUserId)
                            .Count();

                        model.Projects = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                            .Where(m => m.CreatedBy == this.CurrentUserId)
                            .ToList().OrderBy(m => m.Name)
                            .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                            .Take(PagingInfo.PageSize);
                    }
                    else
                    { 
                    PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()                            
                            .Count();

                    model.Projects = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()                        
                        .ToList().OrderBy(m => m.Name)
                        .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                        .Take(PagingInfo.PageSize);
                    }
                    ModelState.AddModelError(string.Empty, Resources_Administration_Web.View_Actions_SearchError);
                }
                
            }
            else
            {
                if (User.IsInRole("GroupAdmin"))
                {
                    PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                        .Where(m => m.CreatedBy == this.CurrentUserId)
                        .Count();
                    model.Projects = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                        .Where(m => m.CreatedBy == this.CurrentUserId)
                        .ToList().OrderBy(m => m.Name)
                        .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                        .Take(PagingInfo.PageSize);
                }
                else
                { 
                PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupRepository.AsQueryable().Count();
                model.Projects = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                    .ToList().OrderBy(m => m.Name)
                    .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                    .Take(PagingInfo.PageSize);
                }
            }            
            model.PagingInfo = PagingInfo;
            return View(model);
        }
        [MvcSiteMapNodeAttribute(Title = "Add Program", ParentKey = "Project", Attributes = @"{ ""visibility"": ""SiteMapPathHelper,!*"",""cssIcontype"":""fa fa-plus""}")]
        [Authorize(Roles = "GroupAdmin,Admin")]
        public ActionResult Create()
        {
            var model = new Group();
            model.CreatedBy = this.CurrentUserId;
            model.EmailAddress = User.Identity.Name;
            model.StartDate = DateTime.Today;
            model.EndDate = DateTime.Today.AddDays(7);
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "GroupAdmin,Admin")]
        public ActionResult Create(Group group)
        {
            if (ModelState.IsValid)
            {
                group.CreatedBy = this.CurrentUserId;
                _IPlatformUnitOfWork.IGroupRepository.Add(group);                             
                _IPlatformUnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(group);
        }
        [Authorize(Roles = "GroupAdmin,Admin")]
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }
            Group model;
            if (User.IsInRole("GroupAdmin"))
            {
                model = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                .Where(m => m.CreatedBy == this.CurrentUserId)
                .FirstOrDefault(m => m.Id == Id);
            }
            else
            {
                model = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()                    
                    .FirstOrDefault(m => m.Id == Id);
            }            
            if (model == null)
            {
                return HttpNotFound();
            }            
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "GroupAdmin,Admin")]        
        public ActionResult Edit(Group group)
        {
            Group model;
            if (User.IsInRole("GroupAdmin"))
            {
                model = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                .Where(m => m.CreatedBy == this.CurrentUserId)
                .FirstOrDefault(m => m.Id == group.Id);
            }
            else
            {
                model = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                    .FirstOrDefault(m => m.Id == group.Id);
            }            
            
            if (model == null)
            {
                return HttpNotFound();
            }
            model.Name = group.Name;
            model.Administrator = group.Administrator;
            model.StartDate = group.StartDate;
            model.EndDate = group.EndDate;
            model.EmailAddress = group.EmailAddress;
            _IPlatformUnitOfWork.IGroupRepository.Update(model);
            _IPlatformUnitOfWork.Commit();

            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "GroupAdmin,Admin")]
        public ActionResult Delete(int? groupId)
        {
            if (groupId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group model;
            if (User.IsInRole("GroupAdmin"))
            {
                model = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                    .Where(m => m.CreatedBy == this.CurrentUserId)
                    .FirstOrDefault(m => m.Id == groupId);
            }
            else
            {
                model = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()                        
                        .FirstOrDefault(m => m.Id == groupId);
            }
            
            if (model == null)
            {
                return HttpNotFound();
            }
            _IPlatformUnitOfWork.IGroupRepository.Delete(model);
            _IPlatformUnitOfWork.Commit();
            return RedirectToAction("Index");
        }
    }
}
