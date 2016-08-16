﻿using System.Configuration;
using System.Linq;
using System;
using System.Reflection;
using System.IO;
using System.Web.Hosting;

namespace PPI.Plugin.Email.Infrastructure
{
    using PPI.Platform.Mvc.Attributes;
    using PPI.Platform.Core.Entities;
    using PPI.Platform.Core.Attributes;

    public class Startup
    {
        const string PluginName = "PPI.Plugin.Email";
        /// <summary>
        /// Need Default Constructor for Dynamic invocations
        /// See Platform.Web.MefConfig.InitilizePlugins
        /// </summary>
        public Startup()
        {  
            
        }
        
        public dynamic StartupPlugin(dynamic HostSharedData)
        {
            bool Loaded = false;
            Configuration ConfigurationManager = (Configuration)HostSharedData.WebConfig;
            //var Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            var AppSetting_Loaded = ConfigurationManager.AppSettings.Settings.AllKeys.FirstOrDefault(m => m.Equals(PluginName + "_Loaded"));
            if (!string.IsNullOrEmpty(AppSetting_Loaded))
                Loaded = bool.Parse(ConfigurationManager.AppSettings.Settings[PluginName + "_Loaded"].Value);
            else
                ConfigurationManager.AppSettings.Settings.Add(new KeyValueConfigurationElement(PluginName + "_Loaded", "False"));
            if (!Loaded)
            {                                
                    //inject via webconfig the EF metadata if its not there for our plug in
                SetEFConnectionString(ConfigurationManager);
                    //inject via webconfig the MVCSiteMap metadata if its not there for our plug in
                MvcSiteMapSetting(ConfigurationManager);
                ConfigurationManager.AppSettings.Settings[PluginName + "_Loaded"].Value = "True";
                    
                //Register the plugin                
                var Plugin = new RegisteredPlugin();
                Plugin.InstallationDate = DateTime.Now;
                Plugin.Name = PluginName;
                var Executing = Assembly.GetExecutingAssembly();
                Plugin.Version = AssemblyName.GetAssemblyName(Executing.Location).Version.ToString();
                Plugin.Custom = false;                
                Plugin.Description = Executing.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)
                                     .OfType<AssemblyDescriptionAttribute>()
                                     .FirstOrDefault().Description;
                Plugin.FullPath = Executing.Location;
                //Plugin.Dashboards.Add(AddDashboardItems(HostSharedData, Executing));
                HostSharedData.Plugins.Add(Plugin);            
            }
            return HostSharedData;
        }       
        private void SetEFConnectionString(Configuration manager)
        {
            string Meta = "metadata=res://*/Entities.Email.csdl|res://*/Entities.Email.ssdl|res://*/Entities.Email.msl";
            bool connectionstringTest = false;
            
            System.Configuration.ConnectionStringSettings existingPlatformConnection = null;
            foreach (System.Configuration.ConnectionStringSettings item in manager.ConnectionStrings.ConnectionStrings)
            {
                if (item.Name == "EmailEntities")
                {
                    connectionstringTest = true;
                }
                else if (item.Name == "PlatformEntities")
                {
                    existingPlatformConnection = new ConnectionStringSettings("EmailEntities", item.ConnectionString.Replace(item.ConnectionString.Split(char.Parse(";")).First(), Meta), item.ProviderName);
                    //item;
                }
            }
            if (!connectionstringTest)
            {
                //need to clone PlatformEntities connection string and create the plugins AdministrationEntities                
                manager.ConnectionStrings.ConnectionStrings.Add(existingPlatformConnection);                
            }               
        }
        private void MvcSiteMapSetting(Configuration manager)
        {
            var Meta = PluginName;
            var MvcSiteMapProvider_IncludeAssembliesForScan = manager.AppSettings.Settings["MvcSiteMapProvider_IncludeAssembliesForScan"].Value;
            if (MvcSiteMapProvider_IncludeAssembliesForScan.IndexOf(Meta) == -1)
            {
                MvcSiteMapProvider_IncludeAssembliesForScan += ";" + Meta;
                manager.AppSettings.Settings["MvcSiteMapProvider_IncludeAssembliesForScan"].Value = MvcSiteMapProvider_IncludeAssembliesForScan;
            }
            
        }

        private Dashboard AddDashboardItems(dynamic HostSharedData, Assembly assembly)
        {
            var DashboardItem = new Dashboard();
            DashboardItem.PartialViewName = "_AdminDashboard";
            DashboardItem.Role = "Admin";
            return DashboardItem;
        }
        
    }
}