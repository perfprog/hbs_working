using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PPI.Platform.Web.Startup))]
namespace PPI.Platform.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
