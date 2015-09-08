using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Infrastructure.Web.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Infrastructure.Web
{
    using Filters;
    using Security;
    using Services;
    using Engine;

    public class SpaHttpApplication : HttpApplication
    {
        public virtual dynamic GetAppObject()
        {
            return GlobalAppObject.Create();
        }

        public virtual string GetAppInlineScript()
        {
            string script = string.Concat("var ",
                SpaApp.Config.GlobalJsAppObjectName, "=",
                SpaApp.Config.GlobalJsAppObjectName, "||",
                JsonConvert.SerializeObject(GetAppObject(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }), ";\t\n");
            if (SpaApp.Config.UseScriptsVersionType != ScriptsVersionType.None)
            {
                script = string.Concat(script, "var require = { urlArgs: 'v=", SpaApp.GetScriptsVersionSufix(), "' };");
            }
            return script;
        }

        protected virtual void Application_Start(object sender, EventArgs e)
        {
            Log.Info("Application_Start");
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                StringEscapeHandling = StringEscapeHandling.EscapeHtml,
                Formatting = App.Config.IsDebugConfiguration ? Formatting.Indented : Formatting.None
            };
            if (SecurityConfig.Config.UseAntiForgeryToken)
                AntiForgeryConfig.SuppressIdentityHeuristicChecks = 
                    SecurityConfig.Config.AntiForgerySuppressIdentityHeuristicChecks;
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(ConfigureGlobalConfiguration);
            GlobalFilters.Filters.Add(new MvcExceptionFilter());
            if (SecurityConfig.Config.UseAnonymousIdentification)
                GlobalFilters.Filters.Add(new MvcAnonymousIdentificationFilter());
            RegisterDefaultRoutes(RouteTable.Routes);
            BundleTable.EnableOptimizations = SpaApp.Config.BundleOptimizationEnabled;
            RegisterDefaultJsBundles(BundleTable.Bundles, SpaApp.Config.ScriptBundle, SpaApp.Config.ScriptBundleIncludes);
            RegisterDefaultStyleBundles(BundleTable.Bundles, SpaApp.Config.StyleBundle, SpaApp.Config.StyleBundleIncludes);
            RegisterViewEngines(ViewEngines.Engines, SpaApp.Config.Views);
        }

        protected virtual void Application_End(object sender, EventArgs e)
        {
            Log.Info("Application_End");
            AppFinalizator.PerformCleanup();
        }
                       
        protected virtual void RegisterViewEngines(ViewEngineCollection viewEngines, 
            IDictionary<string, Type> precompiledViews)
        {
            IViewEngine razor = null, webForms = null;
            foreach (IViewEngine engine in viewEngines)
            {
                if (engine is RazorViewEngine)
                    razor = engine;
                if (engine is WebFormViewEngine)
                    webForms = engine;
            }
            viewEngines.Clear();
            if ((SpaApp.Config.ViewEngines & Engines.RazorViewEngine) == Engines.RazorViewEngine && razor != null)
                viewEngines.Add(razor);
            if ((SpaApp.Config.ViewEngines & Engines.WebFormViewEngine) == Engines.WebFormViewEngine && webForms != null)
                viewEngines.Add(webForms);
            if ((SpaApp.Config.ViewEngines & Engines.PrecompiledViewEngine) == Engines.PrecompiledViewEngine)
                viewEngines.Add(new PrecompiledViewEngine(precompiledViews));                          
        }

        protected virtual void ConfigureGlobalConfiguration(HttpConfiguration config)
        {   
            config.Filters.Add(new HttpExceptionFilter());
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling =
                PreserveReferencesHandling.None;
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.MapHttpAttributeRoutes();
            config.Services.Add(typeof(IExceptionLogger), new AppExceptionLogger());
            config.Services.Replace(typeof(IHttpControllerActivator), new AppControllerActivator());
            if (SecurityConfig.Config.UseAnonymousIdentification)
                config.Filters.Add(new HttpAnonymousIdentificationFilter());
        }

        protected virtual void RegisterDefaultRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Loading", "Loading", 
                new { controller = "Home", action = "Loading" });
            routes.MapRoute( "Default", "{controller}/{action}/{id}", 
                new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        protected virtual void RegisterDefaultJsBundles(BundleCollection bundles, 
            string scriptBundle, 
            IEnumerable<string> scriptBundleIncludes)
        {           
            bundles.IgnoreList.Clear();
            bundles.IgnoreList.Ignore("*.intellisense.js");
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            if (scriptBundle.Contains("{culture}"))
            {
                foreach (var culture in SpaApp.Config.Cultures)
                {
                    var bundle = new ScriptBundle(scriptBundle.Replace("{culture}", culture));
                    if (scriptBundleIncludes != null)
                        foreach (var script in scriptBundleIncludes)
                        {
                            bundle.Include(script.Replace("{culture}", culture));
                        }
                    bundles.Add(bundle);
                }
            }
            else
            {
                bundles.Add(new ScriptBundle(scriptBundle).Include(scriptBundleIncludes.ToArray()));
            }
        }

        protected virtual void RegisterDefaultStyleBundles(BundleCollection bundles, 
            string styleBundle,
            IEnumerable<string> styleBundleIncludes)
        {
            if (styleBundle.Contains("{culture}"))
            {
                foreach (var culture in SpaApp.Config.Cultures)
                {
                    var bundle = new StyleBundle(styleBundle.Replace("{culture}", culture));
                    if (styleBundleIncludes != null)
                        foreach (var script in styleBundleIncludes)
                        {
                            bundle.Include(script.Replace("{culture}", culture));
                        }
                    bundles.Add(bundle);
                }
            }
            else
            {
                bundles.Add(new StyleBundle(styleBundle).Include(styleBundleIncludes.ToArray()));
            }           
        }

        protected virtual void Application_Error(object sender, EventArgs e)
        {
            AppExceptionLogger.LogException(Server.GetLastError());
        }

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            if (SpaApp.Config.StaticFoldersList.Any(HttpContext.Current.Request.Url.ToString().ToLower().Contains)) return;
            if (SpaApp.Config.StaticFilesList.Any(HttpContext.Current.Request.Url.ToString().ToLower().Contains)) return;
            if (SpaApp.Config.Cultures == null || !SpaApp.Config.Cultures.Any()) return;
            SpaApp.InitalizeCulture();            
        }
    }
}
