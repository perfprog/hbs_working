using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Common;

namespace PPI.Platform.Core.EFHelpers
{
    using PPI.Platform.Core.Logging.Interface;
    using System.ComponentModel.Composition;

    [Export(typeof(IDbCommandInterceptor))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LoggingCommandInterceptor : IDbCommandInterceptor
    {
        [Import]
        private ILogging _ILogger;        
                
        public void NonQueryExecuting(
       DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            Log(command, interceptionContext);
        }

        public void NonQueryExecuted(
            DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            LogIfError(command, interceptionContext);
        }

        public void ReaderExecuting(
            DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            Log(command, interceptionContext);
        }

        public void ReaderExecuted(
            DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            LogIfError(command, interceptionContext);
        }

        public void ScalarExecuting(
            DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            Log(command, interceptionContext);
        }

        public void ScalarExecuted(
            DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            LogIfError(command, interceptionContext);
        }


        private void Log<TResult>(
        DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            if (!interceptionContext.IsAsync)
            {
                _ILogger.LogWarn("Non-async command used: {0} parameters: {1}", command.CommandText.Replace(Environment.NewLine, ""),FlattenParameters(command.Parameters));
            }
            else
                _ILogger.LogTrace("command used: {0} parameters: {1}", command.CommandText.Replace(Environment.NewLine, ""), FlattenParameters(command.Parameters));                        
        }

        private void LogIfError<TResult>(
            DbCommand command, DbCommandInterceptionContext<TResult> interceptionContext)
        {
            if (interceptionContext.Exception != null)
            {
                _ILogger.LogError("Command {0} parameters: {1} failed with exception {2}",
                     command.CommandText.Replace(Environment.NewLine, ""),FlattenParameters(command.Parameters),interceptionContext.Exception);
            }
        }

        private string FlattenParameters(DbParameterCollection parameters)
        {            
            var builder = new StringBuilder();
            int i = 0;
            foreach (DbParameter item in parameters)
            {             
                builder.Append("@" + i.ToString() + ":[");
                builder.Append(item.Value.ToString() + "] ");
                i++;
            }
            return builder.ToString();
        }


    }
}
