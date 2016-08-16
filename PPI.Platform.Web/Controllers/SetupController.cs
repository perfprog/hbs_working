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
    using PPI.Platform.Core.Utility;
    [ExportController("Setup"), PartCreationPolicy(CreationPolicy.NonShared)]    
    public class SetupController : BaseController
    {
        // GET: Setup
        [MvcSiteMapNodeAttribute(Title = "Setup", ParentKey = "Home", Key = "Setup", Order = 10)]
        [Authorize(Roles = "SystemIT")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "SystemIT")]
        [MvcSiteMapNodeAttribute(Title = "Encyption Keys", ParentKey = "Setup")]
        public ActionResult EncyptionKeys()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SystemIT")]
        public string EncyptionKeys(string valueText,string section,string button)
        {
            string retVal=Resources_Web.Setup_Encryp_Decrypt_Error;
            _Logging.LogInfo("{0} call with following attributes {1} : {2}", button, valueText, section);
            if (String.IsNullOrWhiteSpace(section))
            {
                section = null;
            }
            TagBuilder tag = new TagBuilder("label");
            
            //retVal = "<label class=\\"text-center text-error\\"></label>"
            
            try
            {
                switch (button)
                {
                    case "Encrypt":
                        {
                            if (String.IsNullOrWhiteSpace(section))
                            {
                                retVal = MachineKeyWrapper.ProtectUrlSafeString(valueText);
                            }
                            else
                            {
                                retVal = MachineKeyWrapper.ProtectUrlSafeString(valueText, section);
                            }
                            
                            tag.MergeAttribute("class", "text-center text-success");
                            break;
                        }
                    case "Decrypt":
                        {
                            if (String.IsNullOrWhiteSpace(section))
                            {
                                retVal = MachineKeyWrapper.UnprotectUrlSafeString(valueText);
                            }
                            else
                            {
                                retVal = MachineKeyWrapper.UnprotectUrlSafeString(valueText, section);
                            }
                            
                            tag.MergeAttribute("class", "text-center text-success");
                            break;
                        }
                    default:
                        break;
                }
                tag.SetInnerText(retVal);
                return tag.ToString();
            }
            catch
            {
                tag.MergeAttribute("class", "text-center text-warning");
                tag.SetInnerText(retVal);
                return tag.ToString();
            }

        }
    }
}