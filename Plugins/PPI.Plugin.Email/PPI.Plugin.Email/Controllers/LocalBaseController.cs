using System.ComponentModel.Composition;

namespace PPI.Plugin.Administration.Controllers
{

    using PPI.Platform.Mvc.Controller;
    using PPI.Plugin.Administration.Core.Domain;
    public class LocalBaseController : BaseController
    {
        [Import]
        protected IAdministrationUnitOfWork _IAdministrationUnitOfWork;
    
    }
}