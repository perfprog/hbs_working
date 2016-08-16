
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PPI.Platform.Web
{
    using Forloop.HtmlHelpers;
    using PPI.Platform.Core.Logging.Interface;
    using PPI.Platform.Core.Attributes;
    [Logging(AttributeExclude = true)]      
    public class MvcApplication : System.Web.HttpApplication
    {
        
        protected void Application_Start()
        {
            ScriptContext.ScriptPathResolver = System.Web.Optimization.Scripts.Render;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MefConfig Mef = new MefConfig();            
            Mef.RegisterMefPlugins();
            var mmP = ModelMetadataProviders.Current;            
        }        
    }
}
