using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.Composition;


namespace PPI.Platform.Web.Controllers
{
    using MvcSiteMapProvider;
    using PPI.Platform.Mvc.Controller;    
    using PPI.Platform.Core.Entities;
    using PPI.Platform.Mvc.Attributes;
    using PPI.Platform.Web.Properties;
    using PPI.Platform.Mvc.Extentions;

    [ExportController("RegisteredPlugins"),PartCreationPolicy(CreationPolicy.NonShared)]    
    [Authorize(Roles="SystemIT")]
    public class RegisteredPluginsController : BaseController
    {

        //Attributes = @"{ ""visibility"": ""SiteMapPathHelper,!*"" }
        // GET: RegisteredPlugins
        [MvcSiteMapNodeAttribute(Title = "Plugins", ParentKey = "Setup")]        
        public ActionResult Index()
        {            
            return View(_IPlatformUnitOfWork.IRegisteredPluginRepository.GetAll().ToList());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //_IPlatformUnitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
