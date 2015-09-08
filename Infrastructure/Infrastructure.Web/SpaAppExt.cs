using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Threading;
using Infrastructure.Web.Data;
using Microsoft.Ajax.Utilities;
using System.Globalization;

namespace Infrastructure.Web
{
    using Cookies;

    public partial class SpaApp
    {
        public static string GetScriptsVersionType(ScriptsVersionType type)
        {
            if (type == ScriptsVersionType.Instance) return App.InstanceHash;
            if (type == ScriptsVersionType.Unique) return Guid.NewGuid().ToString().Substring(0, 8);
            if (type == ScriptsVersionType.AssemblyVersion)
                return AppAssembly.GetAssembly() == null ? "" : App.Config.Version;
            if (type == ScriptsVersionType.None) return "";
            if (type == ScriptsVersionType.Fixed) return Config.ScriptsVersion ?? "";
            DateTime date = DateTime.Now;
            if (type == ScriptsVersionType.Hours) return string.Format("{0:yyyyMMddHH}", date);
            if (type == ScriptsVersionType.Day) return string.Format("{0:yyyyMMdd}", date);
            return type == ScriptsVersionType.Month ? string.Format("{0:yyyyMM}", date) : "";
        }

        public static string GetScriptsVersionSufix()
        {
            return GetScriptsVersionType(Config.UseScriptsVersionType);
        }

        public static string GetResxScriptsVersionSufix()
        {
            return GetScriptsVersionType(Config.ResxScriptsVersionType);
        }

        public static bool GetShowPageLoadingProgress()
        {
            PageLoadingProgress e = Config.ShowPageLoadingProgress;
            if (e == PageLoadingProgress.Always) return true;
            if (e == PageLoadingProgress.RootOnly &&
                HttpContext.Current.Request.FilePath == "/") return true;
            if (e == PageLoadingProgress.NotCachedAlways &&
                HttpContext.Current.Request.Headers.Get("Cache-Control") == null) return true;
            return e == PageLoadingProgress.NotCachedRootOnly &&
                    HttpContext.Current.Request.Headers.Get("Cache-Control") == null &&
                    HttpContext.Current.Request.FilePath == "/";                  
        }

        public static string MinifyJavaScript(string script)
        {
            if (App.Config.IsDebugConfiguration) return script;
            return new Minifier().MinifyJavaScript(script, new CodeSettings
            {
                EvalTreatment = EvalTreatment.MakeImmediateSafe,
                PreserveImportantComments = false
            }); 
        }

        public static string CssMinify(string css)
        {
            if (App.Config.IsDebugConfiguration) return css;
            return new Minifier().MinifyStyleSheet(css, new CssSettings
            {
                CommentMode = CssComment.None
            }); 
        }

        private static readonly object MainLock = new object();
        private static volatile string _mainJsBuild = null;

        public static string MainJsBuildContent()
        {
            if (_mainJsBuild != null) return _mainJsBuild;
            lock (MainLock)
            {
                if (_mainJsBuild != null) return _mainJsBuild;
                return _mainJsBuild = string.Concat(File.ReadAllText(
                    HttpContext.Current.Server.MapPath(Config.MainJsModule)
                    ).Replace("</script>", "</scr\"+\"ipt>"), "require(['main']);");
            }
        }

        private static bool SetThreadCulture(string culture)
        {
            if (!Config.Cultures.Any(s => string.Equals(s, culture))) return false;
            Thread.CurrentThread.CurrentCulture =
                Thread.CurrentThread.CurrentUICulture =
                    new CultureInfo(culture);
            return true;
        }

        private static bool SetCulture(string culture)
        {
            if (SetThreadCulture(culture)) return true;
            if (culture.Length <= 2) return false;
            culture = culture.Substring(0, 2);
            return SetThreadCulture(culture);
        }

        public static void InitalizeCulture()
        {
            AppCookie appCookie = new AppCookie();
            AppCookieModel cookie = appCookie.Retreive();

            //Culture already set in cookie... good
            if (cookie.Culture != null)
            {
                if (SetCulture(cookie.Culture))
                    return;
            }

            // set first culture from Request.UserLanguages that maches config cultures list
            if (HttpContext.Current != null && HttpContext.Current.Request.UserLanguages != null)
            {
                foreach (var l in HttpContext.Current.Request.UserLanguages)
                {
                    var s = l.Split(';')[0];
                    if (s == null) continue;
                    if (!SetCulture(s)) continue;
                    cookie.Culture = s;
                    appCookie.Update(cookie);
                    return;
                }
            }

            // no culture in Request.UserLanguages maches config cultures list, set one from current thread if maches config
            if (SetCulture(Thread.CurrentThread.CurrentUICulture.Name))
            {
                cookie.Culture = Thread.CurrentThread.CurrentUICulture.Name;
                appCookie.Update(cookie);
                return;
            }

            // current thread does not mache config cultures list, set the first one from config if exists
            string c = Config.Cultures.FirstOrDefault();
            if (c == null) return;
            SetCulture(c);
            cookie.Culture = c;
            appCookie.Update(cookie);
        }

        public static string LibVersion(Library library)
        {
            return Config.LibVersions[library];
        }

        public static string GetScript(string src, string attributeName, string attributeValue)
        {
            return string.Concat("<script type='text/javascript' ", attributeName, "='", attributeValue, "' src='", src, "'></script>");
        }

        public static string GetScript(string src, string[] attributes)
        {
            return string.Concat("<script type='text/javascript' ", string.Join(" ", attributes), " src='", src, "'></script>");
        }

        public static string GetScript(string src)
        {
            return string.Concat("<script type='text/javascript' src='", src, "'></script>");
        }

        public static string GetScriptInline(string script)
        {
            return string.Concat("<script type='text/javascript'>", script, "</script>");
        }
    }
}