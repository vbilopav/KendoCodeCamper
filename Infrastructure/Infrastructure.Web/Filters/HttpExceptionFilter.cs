using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Infrastructure.Web.Services;

namespace Infrastructure.Web.Filters
{
    //
    // obsolete
    //

    public class HttpExceptionFilter : ExceptionFilterAttribute 
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            AppExceptionLogger.LogException(context.Exception);
            base.OnException(context);
        }        
    }
}
