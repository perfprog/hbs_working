
namespace PPI.Platform.Core.Logging.Interface
{
    public interface ILogging
    {
        void LogInfo (string message, params object[] args);       
        void LogError(string error, params object[] args);
        void LogTrace(string message, params object[] args);
        void LogDebug(string message, params object[] args);
        void LogWarn(string message, params object[] args);
        void LogFatal(string message, params object[] args);        
    }
}
