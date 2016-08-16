using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web.Mvc;

namespace PPI.Platform.Web.Infrastructure.DependencyResolvers
{
    using PPI.Platform.Core.Attributes;
    [Logging(AttributeExclude = true)]
    public class MefDependencyResolver : IDependencyResolver
    {
     private readonly CompositionContainer _container;

        public MefDependencyResolver(CompositionContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");

            _container = container;
            
            _container.ComposeExportedValue<CompositionContainer>(_container);
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            var name = AttributedModelServices.GetContractName(serviceType);

            return Enumerable.Any(_container.Catalog.Parts.SelectMany(part => part.ExportDefinitions), e => e.ContractName == name) ? _container.GetExportedValue<object>(name) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");

            var name = AttributedModelServices.GetContractName(serviceType);

            return Enumerable.Any(_container.Catalog.Parts.SelectMany(part => part.ExportDefinitions), e => e.ContractName == name) ? _container.GetExportedValues<object>(name) : _container.GetExportedValues<object>(name);
            //return _container.GetExportedValues<object>(name);
        }
    }
}
