using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;

namespace PPI.Platform.Web.Infrastructure.Factory
{
    using PPI.Platform.Web.Models;
    using PPI.Platform.Core.Attributes;
    [Logging(AttributeExclude = true)]
    public class CompositionContainerFactory
    {
        
        public CompositionContainer CreateDefaultContainer(List<string> paths)
        {
                        
            if (paths == null)
                throw new Exception("Unable to find the path");

            var catalog = new MefCatalog(paths.ToArray());

            return CreateContainer(catalog);
        }

        public CompositionContainer CreateContainer(params ComposablePartCatalog[] catalogs)
        {
            var aggregateCatalog = new AggregateCatalog(catalogs);
            return new CompositionContainer(aggregateCatalog);
        }
    }
}