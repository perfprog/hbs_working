using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Hosting;

namespace PPI.Platform.Mvc.Utility
{
    using PPI.Platform.Core.Attributes;
    [Logging(AttributeExclude=true)]
    public static class Path
    {
        public static string RedirectToPluginPath(string locationFormat, string PluginRelitivePath)
        {
            return locationFormat.Replace("~", PluginRelitivePath);
        }

        public static IEnumerable<string> RedirectToPluginPath(IEnumerable<string> locationFormats, string PluginRelitivePath)
        {
            var orginal = new List<string>();
            orginal.AddRange(locationFormats);
            orginal.AddRange(locationFormats.Select(item => RedirectToPluginPath(item, PluginRelitivePath)));
            return orginal;
        }
        public static string MapPathReverse(string fullServerPath)
        {
            return @"~\" + fullServerPath.Replace(HostingEnvironment.ApplicationPhysicalPath, String.Empty);
        }
    }
}
