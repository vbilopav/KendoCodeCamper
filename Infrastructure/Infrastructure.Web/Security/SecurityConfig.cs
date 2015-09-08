using System;
using System.Collections.Generic;
using System.Web;
using System.Dynamic;
using System.Security.Principal;

namespace Infrastructure.Web.Security
{
    public class SecurityConfig : ConfigManager<SecurityConfig>
    {
        public string AuthorizeEndpointPath = null; //not using currently
        public string AuthorizePublicClientId = App.Config.Id;
        public bool UseAntiForgeryToken = false;
        public bool AntiForgerySuppressIdentityHeuristicChecks = true;
        public bool UseAnonymousIdentification = true;
        public IEnumerable<string> AnonymRoles = new List<string>();

        public static dynamic GetUserInfo()
        {
            IPrincipal p = HttpContext.Current == null ? new GenericPrincipal(new GenericIdentity(""), new string[0]) : HttpContext.Current.User;
            dynamic info = new ExpandoObject();
            info.authenticated = !string.Equals(p.Identity.AuthenticationType,
                AnonymousIdentificationManager.AnonymousType) && p.Identity.IsAuthenticated;
            info.name = info.authenticated ? p.Identity.Name : Infrastructure._resx.Core.AnonymUser;
            if (Config.UseAntiForgeryToken)
                info.crsf = AntiForgeryExt.NewToken();
            return info;
        } 
    }
}
