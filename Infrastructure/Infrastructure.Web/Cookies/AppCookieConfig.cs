using CodeFirstConfig;

namespace Infrastructure.Web.Cookies
{
    public class AppCookieConfig
    {
        private class AppCookieConfigManager : ConfigManager<AppCookieConfig> { }
        public static AppCookieConfig Config { get { return AppCookieConfigManager.Config; }}
        public string Name { get; set; }
        public string Path { get; set; }
        public int ExpireDays { get; set; }
        public string Domain { get; set; }
        public bool? HttpOnly { get; set; }
        public bool? Secure { get; set; }
        public bool? Shareable { get; set; }
        public AppCookieModel Default { get; set; }

        public AppCookieConfig()
        {
            Name = string.Concat(".", App.Config.Id.ToLower());
            Path = "/";
            ExpireDays = 365 * 4;
            //Domain = HttpContext.Current.Request.Url.Authority;
            Default = new AppCookieModel();
        }
    }
}
