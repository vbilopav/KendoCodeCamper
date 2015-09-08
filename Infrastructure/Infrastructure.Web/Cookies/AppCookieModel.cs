namespace Infrastructure.Web.Cookies
{       
    public class AppCookieModel
    {
        public string AnonymId { get; set; }
        public string Culture { get; set; }
        public string Version { get; protected set; }

        public AppCookieModel()
        {
            this.AnonymId = "";
            this.Version = "1";
            this.Culture = "";
        }
    }    
}
