using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace Infrastructure.Web.Services
{    
    public class AppExceptionLogger : ExceptionLogger
    {
        public static void LogException(Exception e)
        {
            if (e != null)
            {
                Infrastructure.Log.Error(
                    message: e,
                    source: e.Source);
            }
        }

        public override void Log(ExceptionLoggerContext context)
        {
            LogException(context.Exception);
        }

        public override Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            LogException(context.Exception);
            return Task.FromResult(0);
        }
    }
}
