using System.ComponentModel.Composition;

namespace PPI.Plugin.Report.Controllers
{

    using PPI.Platform.Mvc.Controller;
    using PPI.Plugin.Report.Core.Domain;
    using PPI.Platform.Core.Attributes;
        
    public class PluginBaseController : BaseController
    {
        [Import]
        protected IReportUnitOfWork _IReportUnitOfWork;
    
    }
}