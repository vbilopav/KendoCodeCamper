using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Infrastructure.Web.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlString RenderBundleScriptInline(this HtmlHelper helper, string bundleVirtualPath)
        {                
            Bundle bundle = BundleTable.Bundles.GetBundleFor(bundleVirtualPath);
            BundleResponse ctx = bundle.GenerateBundleResponse(
                new BundleContext(new HttpContextWrapper(HttpContext.Current), BundleTable.Bundles, string.Empty));
            return helper.Raw(SpaApp.GetScriptInline(ctx.Content));     
        }

        public static IHtmlString JsMinify(this HtmlHelper helper, Func<object, object> markup)
		{
			if (helper == null || markup == null)
			{
				return MvcHtmlString.Empty;
			} 
			string sourceJs = (markup.DynamicInvoke(helper.ViewContext) ?? String.Empty).ToString(); 
			return !BundleTable.EnableOptimizations ? 
                new MvcHtmlString(sourceJs) : 
                new MvcHtmlString(SpaApp.MinifyJavaScript(sourceJs));
		}

        public static IHtmlString CssMinify(this HtmlHelper helper, Func<object, object> markup)
		{
			if (helper == null || markup == null)
			{
				return MvcHtmlString.Empty;
			} 
			var sourceCss = (markup.DynamicInvoke(helper.ViewContext) ?? String.Empty).ToString(); 
			return !BundleTable.EnableOptimizations ? 
                new MvcHtmlString(sourceCss) : 
                new MvcHtmlString(SpaApp.CssMinify(sourceCss));
		}

        public static IHtmlString MainJavaScriptBuildInlineContent(this HtmlHelper helper)
        {
            return helper.Raw(SpaApp.GetScriptInline(SpaApp.MainJsBuildContent()));
        }

        public static IHtmlString GetGlobalScriptScriptInline(this HtmlHelper helper)
        {
            return helper.Raw(SpaApp.GetScriptInline(((SpaHttpApplication)HttpContext.Current.ApplicationInstance).GetAppInlineScript()));  
        }
    }
}