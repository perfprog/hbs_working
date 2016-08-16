using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.Composition;

namespace PPI.Plugin.Administration.Controllers
{
    using MvcSiteMapProvider;
    using PPI.Platform.Mvc.Attributes;
    using System.Threading.Tasks;
    using PPI.Platform.Core.Attributes;
    [ExportController("Administration"),PartCreationPolicy(CreationPolicy.NonShared)]
    [Authorize(Roles="GroupAdmin,Admin")]
    
    public class AdministrationController : PluginBaseController
    {        
        // GET: Administration/Create
        [HttpGet]
        public string DashboardCount(string type)
        {
            string retval = "0";
            if (type == "projectreturnCount")
            {
                if (User.IsInRole("GroupAdmin"))
                {
                    retval = _IPlatformUnitOfWork.IGroupRepository.AsQueryable().Where(m => m.CreatedBy == this.CurrentUserId).Count().ToString();
                }
                else
                {
                    retval = _IPlatformUnitOfWork.IGroupRepository.AsQueryable().Count().ToString();
                }
                
            }
            else if (type == "participantreturnCount")
            {
                if (User.IsInRole("GroupAdmin"))
                {
                    retval = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().Where(m => m.Group.CreatedBy == this.CurrentUserId).Count().ToString();
                }
                else
                {
                    retval = _IPlatformUnitOfWork.IGroupParticipantRepository.AsQueryable().Count().ToString();
                }
            }
            return retval;
        }

    }
}
