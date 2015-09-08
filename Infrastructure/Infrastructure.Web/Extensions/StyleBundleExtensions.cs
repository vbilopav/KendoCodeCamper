using System;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Infrastructure.Web.Extensions
{
    //
    //  The StyleBundleExtensions.cs class provides an extension method to the StyleBundle, 
    //  that injects a fallback script into the page that will load a local stylesheet when the CDN source fails. 
    //  To use it, call the .IncludeFallback() extension method on the StyleBundle object. 
    //  It is important to provide a class name, rule name and rule value from the stylesheet being loaded from an external CDN.
    //
    //  https://github.com/EmberConsultingGroup/StyleBundleFallback/
    //

    public static class StyleBundleExtensions
    {
        /// <summary>
        /// Include a stylesheet to fallback to when external CdnPath does not load.
        /// </summary>
        /// <param name="bundle"></param>
        /// <param name="fallback">Virtual path to fallback stylesheet</param>
        /// <param name="className">Stylesheet class name applied to test DOM element</param>
        /// <param name="ruleName">Rule name to test when the class is applied ie. width</param>
        /// <param name="ruleValue">Value to test when the class is applied ie. 1px</param>
        /// <returns></returns>
        public static StyleBundle IncludeFallback(this StyleBundle bundle, string fallback,
            string className = null, string ruleName = null, string ruleValue = null)
        {
            if (String.IsNullOrEmpty(bundle.CdnPath))
            {
                throw new Exception("CdnPath must be provided when specifying a fallback");
            }
            if (VirtualPathUtility.IsAppRelative(bundle.CdnPath))
            {
                bundle.CdnFallbackExpress(fallback);
            }
            else if (new[] { className, ruleName, ruleValue }.Any(String.IsNullOrEmpty))
            {
                throw new Exception(
                    "IncludeFallback for cross-domain CdnPath must provide values for parameters [className, ruleName, ruleValue].");
            }
            else
            {
                bundle.CdnFallbackExpress(fallback, className, ruleName, ruleValue);
            }
            return bundle;
        }

        private static StyleBundle CdnFallbackExpress(this StyleBundle bundle, string fallback,
            string className = null, string ruleName = null, string ruleValue = null)
        {
            bundle.Include(fallback);
            fallback = VirtualPathUtility.ToAbsolute(fallback);
            bundle.CdnFallbackExpression = String.IsNullOrEmpty(className) ?
                $@"function() {{
                var len = document.styleSheets.length;
                for (var i = 0; i < len; i++) {{
                    var sheet = document.styleSheets[i];
                    if (sheet.href.indexOf('{bundle.CdnPath}') !== -1) {{
                        var rules = sheet.rules || sheet.cssRules;
                        if (rules.length <= 0) {{
                            document.write('<link href=""{fallback}"" rel=""stylesheet"" type=""text/css"" />');
                        }}
                    }}
                }}
                return true;
                }}()"
                :
                $@"function() {{
                var loadFallback,
                    len = document.styleSheets.length;
                for (var i = 0; i < len; i++) {{
                    var sheet = document.styleSheets[i];
                    if (sheet.href.indexOf('{bundle.CdnPath}') !== -1) {{
                        var meta = document.createElement('meta');
                        meta.className = '{className}';
                        document.head.appendChild(meta);
                        var value = window.getComputedStyle(meta).getPropertyValue('{ruleName}');
                        document.head.removeChild(meta);
                        if (value !== '{ruleValue}') {{
                            document.write('<link href=""{fallback}"" rel=""stylesheet"" type=""text/css"" />');
                        }}
                    }}
                }}
                return true;
            }}()"; 
            bundle.CdnFallbackExpression = SpaApp.MinifyJavaScript(bundle.CdnFallbackExpression);
            return bundle;
        }
    }
}
