using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.Composition;

namespace PPI.Plugin.Administration.Controllers
{
    using MvcSiteMapProvider;
    using PPI.Platform.Mvc.Attributes;

    [ExportController("Administration"),PartCreationPolicy(CreationPolicy.NonShared)]
    public class AdministrationController : PluginBaseController
    {
        const string AdminMenu = "Administration";
        // GET: Administration        
        [Logging]
        public ActionResult Index()
        {         
            return View();
        }

        // GET: Administration/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Administration/Create
        [HttpGet]
        public string DashboardCount(string type)
        {
            var retval = "";
            if (type == "projectreturnCount")
            {
                retval = _IPlatformUnitOfWork.IGroupRepository.AsQueryable().Count().ToString();
            }
            else if (type == "participantreturnCount")
                retval = _IPlatformUnitOfWork.IParticipantRepository.AsQueryable().Count().ToString();

                return retval;
        }

    }
}
