using System.Web.Mvc;

namespace PPI.Platform.Web.Controllers
{
    using MvcSiteMapProvider;
    [AllowAnonymous]
    public class ErrorController : Controller
    {        
        public ActionResult Http404()
        {
            Response.StatusCode = 404;

            return View();
        }
    }
}