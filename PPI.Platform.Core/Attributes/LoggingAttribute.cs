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
    using PostSharp.Aspects;
    
    /// <summary> 
    /// Aspect that, when applied on a method, emits a trace message before and 
    /// after the method execution. 
    /// </summary> 
    /// <summary> 
    /// Aspect that, when applied on a method, emits a trace message before and 
    /// after the method execution. 
    /// </summary> 
    [Serializable]    
    public sealed class LoggingAttribute : OnMethodBoundaryAspect
    {
        private string methodName;
        public LoggingAttribute()
        { }
        
        public static ILogging Logger {get;set;}
        public override void RuntimeInitialize(MethodBase method)
        {                    
            base.RuntimeInitialize(method);
        }
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            this.methodName = method.DeclaringType.FullName + "." + method.Name;
        }

        /// <summary> 
        /// Method invoked before the execution of the method to which the current 
        /// aspect is applied. 
        /// </summary> 
        /// <param name="args">Unused.</param> 
        public override void OnEntry(MethodExecutionArgs args)
        {
            args.MethodExecutionTag = Stopwatch.StartNew();
            Logger.LogTrace("{0}: Enter", this.methodName);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            var sw = (Stopwatch)args.MethodExecutionTag;
            sw.Stop();
            Logger.LogTrace("{0}: Completion in {1} milliseconds",this.methodName, sw.ElapsedMilliseconds);
        }

        /// <summary> 
        /// Method invoked after failure of the method to which the current 
        /// aspect is applied. 
        /// </summary> 
        /// <param name="args">Unused.</param> 
        public override void OnException(MethodExecutionArgs args)
        {
            Trace.Unindent();

            StringBuilder stringBuilder = new StringBuilder(1024);

            // Write the exit message.    
            stringBuilder.Append(DateTime.Now.ToString());
            stringBuilder.Append(" - ");
            stringBuilder.Append(this.methodName);
            stringBuilder.Append('(');

            // Write the current instance object, unless the method 
            // is static. 
            object instance = args.Instance;
            if (instance != null)
            {
                stringBuilder.Append("this=");
                stringBuilder.Append(instance);
                if (args.Arguments.Count > 0)
                    stringBuilder.Append("; ");
            }

            // Write the list of all arguments. 
            for (int i = 0; i < args.Arguments.Count; i++)
            {
                if (i > 0)
                    stringBuilder.Append(", ");
                stringBuilder.Append(args.Arguments.GetArgument(i) ?? "null");
            }

            // Write the exception message. 
            stringBuilder.AppendFormat("): Exception ");
            stringBuilder.Append(args.Exception.GetType().Name);
            stringBuilder.Append(": ");
            stringBuilder.Append(args.Exception.Message);
            if (args.Exception.InnerException != null)
            {
                var current = args.Exception.GetBaseException();
                stringBuilder.AppendFormat("): Exception ");
                stringBuilder.Append(current.GetType().Name);
                stringBuilder.Append(": ");
                stringBuilder.Append(current.Message);
            }


            // Finally emit the error. 
            Logger.LogError(stringBuilder.ToString());

        }
    }

}

