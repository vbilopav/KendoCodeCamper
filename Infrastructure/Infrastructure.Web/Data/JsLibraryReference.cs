namespace Infrastructure.Web.Data
{
    public class JsLibraryReference
    {
        public string LocalUrl { get; set; }
        public string CdnUrl { get; set; }
        public string FallbackExp { get; set; }
        public string FallbackUrl { get; set; }
        public bool Skip { get; set; }

        public JsLibraryReference()
        {
            Skip = false;
        }
    }
}