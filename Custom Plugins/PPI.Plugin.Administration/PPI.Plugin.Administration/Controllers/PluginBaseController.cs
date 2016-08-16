using System.ComponentModel.Composition;

namespace PPI.Plugin.Administration.Controllers
{

    using PPI.Platform.Mvc.Controller;
    using PPI.Plugin.Administration.Core.Domain;
    using PPI.Platform.Core.Attributes;
    
    public class PluginBaseController : BaseController
    {
        [Import]
        protected IAdministrationUnitOfWork _IAdministrationUnitOfWork;
    
    }
}