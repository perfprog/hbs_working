using System.ComponentModel.Composition;

namespace PPI.Plugin.Email.Controllers
{

    using PPI.Platform.Mvc.Controller;
    using PPI.Plugin.Email.Core.Domain;
    using PPI.Platform.Core.Attributes;
        
    public class PluginBaseController : BaseController
    {
        [Import]
        protected IEmailUnitOfWork _IEmailUnitOfWork;
    
    }
}