using System.Web.Mvc;
using PPI.Platform.Core.Attributes;


namespace PPI.Platform.Web
{
    [Logging(AttributeExclude = true)]
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
