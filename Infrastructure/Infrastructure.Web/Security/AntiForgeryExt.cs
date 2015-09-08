using System;
using System.Web;
using System.Web.Helpers;

namespace Infrastructure.Web.Security
{
    public static class AntiForgeryExt
    {
        public static string FormToken()
        {
            return
                HttpContext.Current.Request.Headers.Get("__RequestVerificationToken");
        }

        public static string CookieToken()
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[AntiForgeryConfig.CookieName];
            return cookie != null ? cookie.Value : null;
        }

        public static string NewToken()
        {
            string newCookieToken;
            string formToken;
            AntiForgery.GetTokens(CookieToken(), out newCookieToken, out formToken);
            if (newCookieToken != null)
            {
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(AntiForgeryConfig.CookieName, newCookieToken));
            }
            return formToken;
        }
    }
}
