using System;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Web.Services;

namespace Infrastructure.Web.Filters
{
    public class MvcExceptionFilter: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            AppExceptionLogger.LogException(context.Exception);
            base.OnException(context);
        }
    }
}
