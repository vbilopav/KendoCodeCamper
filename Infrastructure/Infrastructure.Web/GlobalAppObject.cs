using System;
using System.Dynamic;
using System.Linq;
using System.Globalization;
using System.Threading;
using Infrastructure.Web.Controllers;
using Infrastructure.Web.Data;
using Newtonsoft.Json;

namespace Infrastructure.Web
{
    using Security;
    using Cookies;

    public class GlobalAppObject
    {
        public static dynamic Create()
        {
            dynamic obj = new ExpandoObject();
            obj.debug = App.Config.IsDebugConfiguration;
            obj.culture = new
            {
                name = Thread.CurrentThread.CurrentUICulture.Name,
                value = Thread.CurrentThread.CurrentUICulture.NativeName
            };
            obj.user = SecurityConfig.GetUserInfo();
            obj.cultures = SpaApp.Config.Cultures.Select(c =>
            {
                var ci = new CultureInfo(c);
                return new
                {
                    name = ci.Name,
                    value = ci.NativeName,
                };
            });
            obj.resxVersion = string.Concat("?v=", SpaApp.GetResxScriptsVersionSufix());
            obj.errorpage = SpaApp.Config.ErrorStaticPage;
            obj.authorize = new
            {
                endpoint = SecurityConfig.Config.AuthorizeEndpointPath,
                publicid = SecurityConfig.Config.AuthorizePublicClientId
            };
            obj.cookiemodel = AppCookieConfig.Config;
            obj.hashbang = SpaApp.Config.UseHashBangUrl;
            obj.logerrors = SpaApp.Config.LogClientErrors;
            obj.logendpoint = LogControllerConfig.Config.LogEndpoint;
            obj.data = SpaApp.Config.DataModules
                .Select(s => new
                {
                    key = s.Key,
                    module = s.GetModuleName()
                });
            obj.kendover = SpaApp.LibVersion(Library.KendoUi);
            return obj;
        }
    }
}
