using System;
using System.Web.Mvc;

namespace Infrastructure.Web.Filters
{
    using Security;

    public class MvcAnonymousIdentificationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            AnonymousIdentificationManager.Initialize();            
        }   
    }
}
