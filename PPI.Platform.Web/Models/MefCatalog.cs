using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.IO;
using System.Reflection;

namespace PPI.Platform.Web.Models
{
    using PPI.Platform.Core.Attributes;
    [Logging(AttributeExclude = true)]
    public class MefCatalog : ComposablePartCatalog
    {
        #region Fields

        private readonly AggregateCatalog _catalog;

        #endregion

        #region Properties

        public override IQueryable<ComposablePartDefinition> Parts
        {
            get { return _catalog.Parts; }
        }

        #endregion

        #region Constructors

        public MefCatalog(string[] directory)
        {

            List<string> files = new List<string>();
            foreach (var item in directory)
            {
                files.AddRange(Directory.EnumerateFiles(item, "*.dll", SearchOption.AllDirectories));
            }
            
            _catalog = new AggregateCatalog();

            foreach (var file in files)
            {
                try
                {
                    var asmCat = new AssemblyCatalog(file);

                    if (asmCat.Parts.Any())
                    {
                        _catalog.Catalogs.Add(asmCat);                        
                    }
                    
                }
                catch (ReflectionTypeLoadException)
                {
                }
            }
        }

        #endregion
    }
}