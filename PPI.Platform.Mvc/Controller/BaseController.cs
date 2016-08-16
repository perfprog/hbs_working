using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.Composition;
using Microsoft.AspNet.Identity;


namespace PPI.Platform.Mvc.Controller
{
    using PPI.Platform.Core.Logging.Interface;
    using PPI.Platform.Core.Domain;
    using PPI.Platform.Mvc.Attributes;
    using PPI.Platform.Core.Attributes;
    using PPI.Platform.Mvc.Utility;
    using PPI.Platform.Mvc.HtmlHelpers.Model;
    using PPI.Platform.Core.Entities;
    using PPI.Platform.Core.Email.Interface;
    using PPI.Platform.Core.Pdf.Interface;
    using PPI.Platform.Core.Zip.Interface;

    [Export(typeof(BaseController))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
 
    public class BaseController : System.Web.Mvc.Controller
    {

        public int CurrentCulture { get; private set; }
        public string CurrentPageSize { get; private set; }

        public int CurrentProgram { get; private set; }
        public string CurrentUserId { get; set; }

        public IEnumerable<PluginAction> PluggableActions { get; private set; }
      
        [Import]        
        protected IPlatformUnitOfWork _IPlatformUnitOfWork;

        [Import]
        protected IEmail _IEmail;

        [Import]
        protected ILogging _Logging;
        
        [Import]
        protected IHtmlToPdf _HtmlToPdf;

        [Import]
        protected IZip _Zip;
        
        public BaseController() { }
        
        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {
            int local;                        
            if (!int.TryParse(Cookies.GetCookie("cultureId"), out local))
            {
                
                var localcultureName = requestContext.HttpContext.Request.UserLanguages != null && requestContext.HttpContext.Request.UserLanguages.Length > 0 ?
                requestContext.HttpContext.Request.UserLanguages[0] : "en-US"; //default to en-US
                var cultureCheck = _IPlatformUnitOfWork.ICultureRepository.AsQueryable().FirstOrDefault(m => m.Value == localcultureName);
                //TODO use the browser weights to get the best availible language
                _Logging.LogInfo("checking Browser Language settings: {0}", requestContext.HttpContext.Request.UserLanguages[0]);
                if (cultureCheck != null)
                {
                    local = cultureCheck.Id;
                    Cookies.SetCookie("cultureId", local.ToString(), TimeSpan.FromDays(1));
                }
                else
                {
                    _Logging.LogInfo("Language Not Available defaulting to English {0}", requestContext.HttpContext.Request.UserLanguages[0]);
                    Cookies.SetCookie("cultureId", "en-US", TimeSpan.FromDays(1));
                    local = _IPlatformUnitOfWork.ICultureRepository.AsQueryable().FirstOrDefault(m => m.Value == "en-US").Id;
                }
                
            }
            CurrentCulture = local;
            CurrentPageSize = Cookies.GetCookie("pageSize");
            if (CurrentPageSize == string.Empty)
            {
                CurrentPageSize = "10";
                Cookies.SetCookie("pageSize", "10", TimeSpan.FromDays(1));
            }

            int localProgram;
            
            if (!int.TryParse(Cookies.GetCookie("currentProgram"), out localProgram))
            {
                localProgram = 0;
                Cookies.SetCookie("currentProject", localProgram.ToString(), TimeSpan.FromDays(1));
            }
            CurrentProgram = localProgram;    
            return base.BeginExecute(requestContext, callback, state);
        }
        
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            ViewBag.CultureId = CurrentCulture;
            ViewBag.PageSize = CurrentPageSize;
            ViewBag.Program = CurrentProgram;
            CurrentUserId = User.Identity.GetUserId();
            if (CurrentUserId == null)
                _Logging.LogWarn("  CurrentUserId is Null");
            _Logging.LogInfo("  Current Controler Settings: culture:{0}  PageSize:{1}  UserId:{2}", CurrentCulture, CurrentPageSize, CurrentUserId);
            return base.BeginExecuteCore(callback, state);
        }

        public void SetCurrentProgram(int programId)
        {
            Cookies.SetCookie("currentProgram", programId.ToString(), TimeSpan.FromDays(1));
            CurrentProgram = programId;
        
        }
        public ActionResult SetPageSize(int pageSize, string redirectTo)
        {
            Cookies.SetCookie("pageSize", pageSize.ToString(), TimeSpan.FromDays(1));            
            CurrentPageSize = pageSize.ToString();
            string urlRedirect = "Index";
            if (redirectTo != "" || redirectTo != null)
                urlRedirect = redirectTo;
            _Logging.LogInfo("  Redirected To: {0}  pagesize: {1}", redirectTo);
            return RedirectToAction(urlRedirect);
        }        
    }
}
