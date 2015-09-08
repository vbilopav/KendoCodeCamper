using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Infrastructure.Web.Engine
{
    public class PrecompiledViewEngine : VirtualPathProviderViewEngine, IVirtualPathFactory
    {
        private readonly IDictionary<string, Type> _mappings;
        private readonly IViewPageActivator _viewPageActivator = DefaultViewPageActivator.Current;

        public PrecompiledViewEngine(IDictionary<string, Type> viewMappings)
        {                 
            base.ViewLocationFormats = new[] { "~/Views/{1}/{0}.cshtml" };
            base.PartialViewLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml", 
                "~/Views/Shared/{0}.cshtml", 
            };
            base.FileExtensions = new[] { "cshtml" };
            _mappings = viewMappings;
            this.ViewLocationCache = new PrecompiledViewLocationCache(this.ViewLocationCache);
            VirtualPathFactoryManager.RegisterVirtualPathFactory(this);
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            return Exists(virtualPath);
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return CreateViewInternal(partialPath, masterPath: null, runViewStartPages: false);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return CreateViewInternal(viewPath, masterPath, runViewStartPages: true);
        }

        private IView CreateViewInternal(string viewPath, string masterPath, bool runViewStartPages)
        {
            Type type;
            return _mappings.TryGetValue(viewPath, out type) 
                ? new PrecompiledView(viewPath, masterPath, type, runViewStartPages, base.FileExtensions) 
                : null;
        }

        public object CreateInstance(string virtualPath)
        {
            Type type;            
            return _mappings.TryGetValue(virtualPath, out type) ? _viewPageActivator.Create(null, type) : null;
        }

        public bool Exists(string virtualPath)
        {
            return _mappings.ContainsKey(virtualPath);
        }
    }
}
