using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Infrastructure.Web.Filters
{
    using Security;

    public class HttpAnonymousIdentificationFilter : ActionFilterAttribute 
    {
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            await base.OnActionExecutingAsync(actionContext, cancellationToken);
            AnonymousIdentificationManager.Initialize();            
        }        
    }
}
