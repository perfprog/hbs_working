using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.IO;
namespace PPI.Platform.Mvc.Views
{
    using PPI.Platform.Mvc;
    public class PlugInViewEngine : RazorViewEngine 
    {
        public PlugInViewEngine(List<string> paths)
            : base()
        {
            foreach (var item in paths)
            {
                //ignore default bin as they are for MEF Exports not views
                if (Utility.Path.MapPathReverse(item) != "~\\bin")
                {
                    var folders = new DirectoryInfo(item);
                    var subs = folders.GetDirectories();
                    foreach (var subitem in subs)
                    {
                        string PluginRelativePath = Utility.Path.MapPathReverse(subitem.FullName);
                        AreaViewLocationFormats = Utility.Path.RedirectToPluginPath(AreaViewLocationFormats, PluginRelativePath).ToArray();
                        AreaMasterLocationFormats = Utility.Path.RedirectToPluginPath(AreaMasterLocationFormats, PluginRelativePath).ToArray();
                        AreaPartialViewLocationFormats = Utility.Path.RedirectToPluginPath(AreaPartialViewLocationFormats, PluginRelativePath).ToArray();
                        ViewLocationFormats = Utility.Path.RedirectToPluginPath(ViewLocationFormats, PluginRelativePath).ToArray();
                        MasterLocationFormats = Utility.Path.RedirectToPluginPath(MasterLocationFormats, PluginRelativePath).ToArray();
                        PartialViewLocationFormats = Utility.Path.RedirectToPluginPath(PartialViewLocationFormats, PluginRelativePath).ToArray();             
                    }

                }                    
            }
           
        
        }
    }
}
