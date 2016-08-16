using System;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.Composition.Hosting;



namespace PPI.Platform.Web.Infrastructure.Factory
{
    using PPI.Platform.Mvc.Controller;
    using PPI.Platform.Mvc.Attributes;
    using PPI.Platform.Core.Attributes;
    [Logging(AttributeExclude = true)]
    public class PluginControllerFactory : DefaultControllerFactory
    {
        private  CompositionContainer _container;
        public PluginControllerFactory(CompositionContainer container)
        { 
            _container = container;
        }
        public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName)
        {

            IController controller = null;

            if (controllerName != null)
            { 
               var export = _container.GetExports<BaseController, INameMetaData>()
                                                .Where(e => e.Metadata.Name.Equals(controllerName))
                                                .Select(e => e.Value)
                                                .FirstOrDefault();                
               if (export != null)
               {
                
                   controller = export;
               }               
            }

            if (controller == null)
            {
                return base.CreateController(requestContext, controllerName);                  
            }

            return controller;
        }


    }
}