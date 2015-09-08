using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Infrastructure.Web.Controllers;

namespace Infrastructure.Web.Services
{
    public class AppControllerActivator : IHttpControllerActivator
    {
        private readonly IHttpControllerActivator _default = new DefaultHttpControllerActivator();

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor,
                                      Type controllerType)
        {
            if (!SpaApp.Config.EnableConfigurationController && controllerType == typeof(ConfigurationController))
                return null;
            if (!SpaApp.Config.EnableClientLog && controllerType == typeof(LogController))
                return null;
            return _default.Create(request, controllerDescriptor, controllerType);
        }
    }
}
