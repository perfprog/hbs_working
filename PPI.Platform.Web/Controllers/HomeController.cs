using System.Web.Mvc;
using System.ComponentModel.Composition;
using System.Linq;
namespace PPI.Platform.Web.Controllers
{
    using PPI.Platform.Mvc.Controller;
    using PPI.Platform.Mvc.Attributes;
    using PPI.Platform.Core.Attributes;
    using PPI.Platform.Web.Properties;
    using MvcSiteMapProvider;

    [ExportController("Home")]
    [PartCreationPolicy(CreationPolicy.NonShared)]      
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController() { }        
        
        public ActionResult Index()
        {
            var model = _IPlatformUnitOfWork.IDashboardRepository.AsQueryable();
            
            return View(model);
        }        
    }
}