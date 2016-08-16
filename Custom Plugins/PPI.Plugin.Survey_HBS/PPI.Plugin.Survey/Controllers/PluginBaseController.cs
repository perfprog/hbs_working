using System.ComponentModel.Composition;

namespace PPI.Plugin.Survey.Controllers
{

    using PPI.Platform.Mvc.Controller;
    using PPI.Plugin.Survey.Core.Domain;
    using PPI.Platform.Core.Attributes;
    using System.Web.Mvc;

    [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
    public class PluginBaseController : BaseController
    {
        [Import]
        protected ISurveyUnitOfWork _ISurveyUnitOfWork;
    
    }
}