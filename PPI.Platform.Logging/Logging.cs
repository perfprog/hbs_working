using System.Diagnostics;
using System.ComponentModel.Composition;
using System;


namespace PPI.Platform.Logging
{
    using PPI.Platform.Core.Logging.Interface;
    using NLog;
    using NLog.Targets;
    using NLog.Config;    

    [Export(typeof(ILogging))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class Logging : ILogging
    {
        private static Logger _NLog;
        private static readonly Logging instance = new Logging();                
        static Logging()
        {                          
                _NLog = LogManager.GetLogger("CentralLogging");                         
        }
        private Logging() { }
        public static Logging Instance
        {
            get
            {
                return instance;
            }
        }
        
        public void LogError(string error, params object[] args)
        {
            ;
            _NLog.Error(error, args);            
        }

        public void LogInfo(string message, params object[] args)
        {

            _NLog.Info(message, args);            
        }

        public void LogTrace(string message, params object[] args)
        {            
            _NLog.Trace(message, args);                 
        }

        public void LogDebug(string message, params object[] args)
        {

            _NLog.Debug(message, args);                 
        }

        public void LogWarn(string message, params object[] args)
        {

            _NLog.Warn(message, args);                    
        }

        public void LogFatal(string message, params object[] args)
        {

            _NLog.Fatal(message, args);                 
        }
    }
}
