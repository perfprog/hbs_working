[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(PPI.Platform.Web.MefConfig), "Start")]

namespace PPI.Platform.Web

{
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using System.Reflection;
    using System.Web.Hosting;
    using System.Web.Compilation;
    using System.Collections.Generic;
    using System.IO;
    using System.Configuration;
    using System.Dynamic;    
    using System.ComponentModel.Composition;
    using PPI.Platform.Web.Infrastructure.DependencyResolvers;
    using PPI.Platform.Web.Infrastructure.Factory;
    using PPI.Platform.Core.Entities;
    using PPI.Platform.Core.Domain;
    using System.ComponentModel.Composition.Hosting;
    using PPI.Platform.Core.Logging.Interface;
    using PPI.Platform.Core.Attributes;
    using PPI.Platform.Core.EFHelpers;        
    using System.Data.Entity.Infrastructure.Interception;
    [Logging(AttributeExclude = true)]        
    public class MefConfig
    {
        [Import]
        private ILogging _ILogger;
        [Import]
        private  IPlatformUnitOfWork _IPlatformUnitOfWork;
        [Import]
        private IDbCommandInterceptor _LoggingCommandInterceptor;

        private static List<string> _SearchPaths = new List<string>();
        private static List<string> files = new List<string>();
        private CompositionContainer container = null;
        [Logging(AttributeExclude = true)]
        public MefConfig()
        {            
            container = new CompositionContainerFactory().CreateDefaultContainer(_SearchPaths);
            CompositionBatch compositionBatch = new CompositionBatch();
            compositionBatch.AddPart(this);
            container.Compose(compositionBatch);
            DependencyResolver.SetResolver(new MefDependencyResolver(container));
            ControllerBuilder.Current.SetControllerFactory(new PluginControllerFactory(container));
            //Here to setup logging interceptor for EF, couldn't use DBConfiguration 
            //because MEF wouldn't load the imports
            System.Data.Entity.Infrastructure.Interception.DbInterception.Add(_LoggingCommandInterceptor);
            LoggingAttribute.Logger = _ILogger;
            _ILogger.LogInfo("Logger Is Active");
        }                
        
        public void RegisterMefPlugins()
        {
            _ILogger.LogTrace("Start Register Plugins");
            ViewEngines.Engines.Clear();            
            ViewEngines.Engines.Add(new PPI.Platform.Mvc.Views.PlugInViewEngine(_SearchPaths));
            var current = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            ClearPlugins(current);
            InitilizePlugins(current);
        }
        
        private void ClearPlugins(Configuration currentWebConfig)
        {
            var DeleteList = new List<RegisteredPlugin>();
            
            foreach (var item in _IPlatformUnitOfWork.IRegisteredPluginRepository.AsQueryable().Where(m => m.Name != "Core"))
            {                
                if (!File.Exists(item.FullPath))
                { 
                    //Plugin is gone remove from db and save set the web config correctly;
                    DeleteList.Add(item);
                    _ILogger.LogInfo("Plugin Queued To Be Removed {0}", item.Name);
                    currentWebConfig.AppSettings.Settings.Remove(item.Name + "_Loaded");                    
                }
            }
            if (DeleteList.Count >0)
            { 
                _IPlatformUnitOfWork.IRegisteredPluginRepository.Delete(DeleteList);
                _IPlatformUnitOfWork.Commit();
                _ILogger.LogInfo("Plugins Removed {0}", DeleteList.Count);
            }
        }

     
        public void InitilizePlugins(Configuration currentWebConfig)
        {
            //Use a Dynamic Object for Added Flexiblity down the road
            dynamic SharedData = new ExpandoObject();
            SharedData.WebConfig = currentWebConfig;
            var Executing = Assembly.GetExecutingAssembly();
            SharedData.HostVersion = AssemblyName.GetAssemblyName(Executing.Location).Version.ToString();
            SharedData.Plugins = new List<RegisteredPlugin>();
            foreach (var item in files)            
                {
                    //These are plugins call static class StartupPlugin
                    string startupClass = item.Split(char.Parse("\\")).Last().Replace(".dll",".Infrastructure") + ".Startup";
                    _ILogger.LogDebug("StartupClass: {0}", startupClass);
                    //var assembly = Assembly.LoadFrom(item);
                    var assembly = AppDomain.CurrentDomain.GetAssemblies().Where(m => m.Location == item).FirstOrDefault();
                    Type type = assembly.GetType(startupClass);                    
                    if (type != null)
                    {
                        _ILogger.LogDebug("Assemblies Scaned for Startup Class: {0}", type.FullName);
                        MethodInfo methodInfo = type.GetMethod("StartupPlugin");
                        if (methodInfo != null)
                        {
                            _ILogger.LogInfo("Plugins Found With StartupPlugin Method {0}", type.FullName);
                            object result = null;
                            object[] parametersArray = new object[] { SharedData };
                            object classInstance = Activator.CreateInstance(type, null);                        
                            //This works fine
                            try
                            {
                                _ILogger.LogInfo("Executing Invoke");
                                result = methodInfo.Invoke(classInstance, parametersArray);
                                SharedData = (ExpandoObject)result;     
                            }
                            catch (Exception ex)
                            {
                                _ILogger.LogError(ex.Message);
                                //remove this assembly from compisiton                                
                                //and app domain
                                
                            }
                                                                                                                                       
                        }
                    }
                }
            
            //Plugin is responsible for not registering its self here if its already registerd
            var PluginCount = (List<RegisteredPlugin>)SharedData.Plugins;
            if (PluginCount.Count() > 0)
            {
                _IPlatformUnitOfWork.IRegisteredPluginRepository.Add(PluginCount);
                _IPlatformUnitOfWork.Commit();
            }
            //Should be last because it could reset the site
            Configuration ConfigOjbect = (Configuration)SharedData.WebConfig;
            _ILogger.LogInfo("Saving WebConfig Changes from Plugins");
            ConfigOjbect.Save();
            
        }

        
        public static void Start()
        {
            // Default Bin for MEF Exports in the application directory
            _SearchPaths.Add(HostingEnvironment.MapPath("~/bin"));
            // Default Plugin Location for new MEF based Exports
            _SearchPaths.Add(HostingEnvironment.MapPath("~/Plugins"));
           
            foreach (var item in _SearchPaths)
            {
                //Ignore the default bin
                if (PPI.Platform.Mvc.Utility.Path.MapPathReverse(item) != "~\\bin")
                    files.AddRange(Directory.EnumerateFiles(item, "*.dll", SearchOption.AllDirectories));
            }

            foreach (var item in files)
            {
                var assembly = Assembly.LoadFrom(item);
                BuildManager.AddReferencedAssembly(assembly);                  
            }



            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;            
                        
        }         
        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
         {
             var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
             // Check we don't already have the assembly loaded
             foreach (var assembly in currentAssemblies)
             {
                 if (assembly.FullName == args.Name || assembly.GetName().Name == args.Name)
                 {
                     return assembly;
                 }
             }

             return null;

         }


    }
}