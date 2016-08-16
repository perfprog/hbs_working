using System.Diagnostics;
using System.ComponentModel.Composition;


namespace PPI.Platform.Plugin.Default.Logging
{
    using PPI.Platform.Core.Logging.Interface;
    [Export(typeof(ILogging))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class Logging : ILogging
    {

        public void LogInformation(string message, params object[] args)
        {
            Trace.TraceInformation(message, args);
        }

        public void LogError(string error, params object[] args)
        {
            Trace.TraceError(error, args);
        }        
    }
}
