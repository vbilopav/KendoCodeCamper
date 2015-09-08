using System;
using System.Collections.Generic;
using Infrastructure.Web.Data;

namespace Infrastructure.Web
{
    using Views;
   
    public partial class SpaApp
    {
        private abstract class SpaAppConfigManager : ConfigManager<SpaApp> {}
        public static SpaApp Config { get { return SpaAppConfigManager.Config; } }
                
        public string MainJsModule { get; set; }
        public string Description { get; set; }
        public string ScriptsVersion { get; set; }
        public ScriptsVersionType UseScriptsVersionType { get; set; }
        public bool InlineScripts { get; set; }
        public string ResxScriptsVersion { get; set; }
        public ScriptsVersionType ResxScriptsVersionType { get; set; }
        public string GlobalJsAppObjectName { get; set; }
        public IEnumerable<string> Cultures { get; set; }
        public IEnumerable<string> StaticFoldersList { get; set; }
        public IEnumerable<string> StaticFilesList { get; set; }
        public string ErrorStaticPage { get; set; }
        public bool UseHashBangUrl { get; set; }
        public bool EnableClientLog { get; set; }
        public bool LogClientErrors { get; set; }
        public bool EnableConfigurationController { get; set; }
        public PageLoadingProgress ShowPageLoadingProgress { get; set; }
        public IEnumerable<RequireModule> DataModules { get; set; }
        public IDictionary<Library, JsLibraryReference> References { get; set; }
        public IDictionary<Library, string> LibVersions { get; set; }                
        public Engines ViewEngines { get; set; }
        public bool BundleOptimizationEnabled { get; set; }
        public IDictionary<string, Type> Views { get; set; }
        public string ScriptBundle { get; set; }
        public IEnumerable<string> ScriptBundleIncludes { get; set; }
        public string StyleBundle { get; set; }
        public IEnumerable<string> StyleBundleIncludes { get; set; }

        public SpaApp()
        {
            MainJsModule = App.Config.IsDebugConfiguration ? "~/App/main.js" : "~/App/build/main.js";
            ScriptsVersion = "1";
            UseScriptsVersionType = App.Config.IsDebugConfiguration ? ScriptsVersionType.Instance : ScriptsVersionType.AssemblyVersion;
            InlineScripts = false;
            ResxScriptsVersion = "1";
            ResxScriptsVersionType = App.Config.IsDebugConfiguration ? ScriptsVersionType.Instance : ScriptsVersionType.Fixed;
            GlobalJsAppObjectName = "_";
            Cultures = new[] { "en", "hr" };
            StaticFoldersList = new[] { "app", "scripts", "content", "fonts" };
            StaticFilesList = new[] { ".htm", ".html", ".css", ".js", ".gif", ".jpg", ".jpeg", ".bmp", ".ico", ".png", ".map", ".woff", ".ttf" };
            ErrorStaticPage = "/Error.html";
            UseHashBangUrl = true;
            LogClientErrors = !App.Config.IsDebugConfiguration;
            EnableClientLog = true;
            EnableConfigurationController = true;
            ShowPageLoadingProgress = PageLoadingProgress.NotCachedAlways;
            DataModules = new List<RequireModule>();
            LibVersions = new Dictionary<Library, string>
            {
                {Library.JQuery, "2.1.3"}, 
                {Library.KendoUi, "2015.1.318"},
                {Library.Bootstrap, "3.3.4"},
                {Library.RequireJs, "2.1.17"}
            };                              
            ViewEngines = Engines.PrecompiledViewEngine;
            BundleOptimizationEnabled = !App.Config.IsDebugConfiguration;
            References = new Dictionary<Library, JsLibraryReference>
            {
                {
                    Library.JQuery, new JsLibraryReference
                    { 
                        LocalUrl = "~/Scripts/libs/jquery-{0}.js", 
                        CdnUrl = "//ajax.googleapis.com/ajax/libs/jquery/{0}/jquery.min.js",
                        FallbackExp = "window.jQuery",
                        FallbackUrl = "~/Scripts/libs/jquery-{0}.min.js" 
                    }
                },
                { 
                    Library.Bootstrap, new JsLibraryReference
                    { 
                        LocalUrl = "~/Scripts/libs/bootstrap.js?v={0}", 
                        CdnUrl = "//maxcdn.bootstrapcdn.com/bootstrap/{0}/js/bootstrap.min.js",
                        FallbackExp = "$.fn.modal",
                        FallbackUrl = "~/Scripts/libs/bootstrap.min.js?v={0}"
                    }
                 },
                 { 
                    Library.RequireJs, new JsLibraryReference
                    { 
                        LocalUrl = "~/Scripts/libs/require.js?v={0}", 
                        CdnUrl = "//cdnjs.cloudflare.com/ajax/libs/require.js/{0}/require.min.js",
                        FallbackExp = "window.requirejs",
                        FallbackUrl = "~/Scripts/libs/require.min.js?v={0}" 
                    }
                 }
            };
            Views = new Dictionary<string, Type>        
                    {
                        {"~/Views/Home/Index.cshtml", typeof (Index)},                                                
                        {"~/Views/Home/Loading.cshtml", typeof (Loading)},      
                        {"~/Views/Home/Error.cshtml", typeof (Error)},      
                        {"~/Views/_ViewStart.cshtml", typeof (ViewStart)},
                        {"~/Views/Shared/_References.cshtml", typeof (References)},
                        {"~/Views/Shared/_Static.cshtml", typeof (Static)},
                        {"~/Views/Shared/_Layout.cshtml", typeof (Layout)}
                    };
            ScriptBundle = "~/Scripts/startup/{culture}";
            ScriptBundleIncludes = new[] { "~/Scripts/startup.js", "~/Scripts/resources/{culture}.js" };
            StyleBundle = "~/Content/css";
            var kendo = string.Format("~/Content/kendo/{0}/", LibVersions[Library.KendoUi]);
            StyleBundleIncludes =  new[]
            {
                string.Format("{0}kendo.common-bootstrap.min.css", kendo),
                string.Format("{0}kendo.bootstrap.min.css", kendo),
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/font-awesome.css",
                "~/Content/application.css"
            };
        }     
    }
}