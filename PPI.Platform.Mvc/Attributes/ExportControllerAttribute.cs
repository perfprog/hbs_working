using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;



namespace PPI.Platform.Mvc.Attributes
{
    using PPI.Platform.Mvc.Controller;
    using PPI.Platform.Core.Attributes;
    [Logging(AttributeExclude = true)]
    public interface INameMetaData
	{
		string Name {get;}
	}
    [Logging(AttributeExclude=true)]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false), MetadataAttribute]
    public class ExportControllerAttribute : ExportAttribute, INameMetaData
    {
        public ExportControllerAttribute(string name)
            : base(typeof(BaseController))            
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
    
    
}
