using System;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Infrastructure.Web.Security
{
    using Cookies;

    public class AnonymousIdentificationManager
    {
        public static string AnonymousType = "Anonym";

        public static void Initialize()
        {
            var cookie = new AppCookie();
            var val = cookie.Retreive();
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                HttpContext.Current.User = new GenericPrincipal(
                    new GenericIdentity(val.AnonymId, AnonymousType),
                    SecurityConfig.Config.AnonymRoles.ToArray());
            }
            if (!string.IsNullOrEmpty(val.AnonymId)) return;
            val.AnonymId = Guid.NewGuid().ToString();
            cookie.Update(val);            
        }

        public static string AnonymousId
        {
            get { return new AppCookie().Retreive().AnonymId; }
        }
    }
}
