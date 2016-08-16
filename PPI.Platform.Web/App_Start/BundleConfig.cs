using System.Web.Optimization;
using PPI.Platform.Core.Attributes;

namespace PPI.Platform.Web
{
    [Logging(AttributeExclude = true)]
    public class BundleConfig
    {
        
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/_extentions.js",
                        "~/Scripts/bootstrap.js",                        
                        "~/Scripts/common.js"));



            bundles.Add(new StyleBundle("~/bundles/styles").Include(
                      "~/Content/bootstrap.css",                      
                      "~/Content/prettify.css",
                      "~/Content/themes/base/*.css"));


            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css", new CssRewriteUrlTransform()));

            BundleTable.EnableOptimizations = true;
        }
    }
}
