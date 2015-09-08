using System.Web;
using System.Web.Mvc;

namespace Infrastructure.Web.Engine
{
    //
    // There's an issue in Mvc's View location's caching behavior. The cache key is derived from the assembly's type name
    // consequently it expects exactly one instance of a type to be registered. We workaround this by overriding the behavior of 
    // the ViewLocationCache to include a token that discriminates multiple view engine instances. 
    // 

    public class PrecompiledViewLocationCache : IViewLocationCache
    {
        private readonly string _prexif;
        private readonly IViewLocationCache _innerCache;

        public PrecompiledViewLocationCache(IViewLocationCache innerCache)
        {
            _prexif = "precompiled";
            _innerCache = innerCache;
        }

        public string GetViewLocation(HttpContextBase httpContext, string key)
        {
            return _innerCache.GetViewLocation(httpContext, string.Concat(_prexif, "_", key));
        }

        public void InsertViewLocation(HttpContextBase httpContext, string key, string virtualPath)
        {
            _innerCache.InsertViewLocation(httpContext, string.Concat(_prexif, "_", key), virtualPath);
        }
    }
}
