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

    [ExportController("Project"), PartCreationPolicy(CreationPolicy.NonShared)]
    public class ProjectController : PluginBaseController
    {        
        // GET: Project
        [MvcSiteMapNodeAttribute(Title = "Project", ParentKey = "Home", Key = "Project")]
        public ActionResult Index(int? page, string search)
        {
            var model = new ProjectIndexViewModel();
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
                    PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                    .Where(search)
                    .Count();
                    model.Projects = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                        .Where(search)
                        .ToList().OrderBy(m => m.Name)
                        .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                        .Take(PagingInfo.PageSize);
                }
                catch
                {
                    PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupRepository.AsQueryable().Count();
                    model.Projects = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                        .ToList().OrderBy(m => m.Name)
                        .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                        .Take(PagingInfo.PageSize);
                    
                    ModelState.AddModelError(string.Empty, Resources_Administration_Web.View_Actions_SearchError);
                }
                
            }
            else
            {
                PagingInfo.TotalRecords = _IPlatformUnitOfWork.IGroupRepository.AsQueryable().Count();
                model.Projects = _IPlatformUnitOfWork.IGroupRepository.AsQueryable()
                    .ToList().OrderBy(m => m.Name)
                    .Skip((PagingInfo.CurrentPage - 1) * PagingInfo.PageSize)
                    .Take(PagingInfo.PageSize);

            }
            model.PagingInfo = PagingInfo;
            return View(model);
        }
        [MvcSiteMapNodeAttribute(Title = "Add Project", ParentKey = "Project")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Group group)
        {
            if (ModelState.IsValid)
            {
                _IPlatformUnitOfWork.IGroupRepository.Add(group);
                _IPlatformUnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(group);
        }
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }
            var model = _IPlatformUnitOfWork.IGroupRepository.AsQueryable().FirstOrDefault(m => m.Id == Id);
            if (model == null)
            {
                return HttpNotFound();
            }            
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Group group)
        {            
            var model = _IPlatformUnitOfWork.IGroupRepository.AsQueryable().FirstOrDefault(m => m.Id == group.Id);
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
        public ActionResult Delete(int? groupId)
        {
            if (groupId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _IPlatformUnitOfWork.IGroupRepository.AsQueryable().FirstOrDefault(m => m.Id == groupId);
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
