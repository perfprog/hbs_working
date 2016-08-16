using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.Web.Hosting;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace PPI.Platform.Core.Attributes
{
    using PPI.Platform.Core.Logging.Interface;
    using PPI.Platform.Core.Utility;
    using PPI.Platform.Mvc.Utility;

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class SecureAttribute : Attribute
    {

      
    }
    
}
