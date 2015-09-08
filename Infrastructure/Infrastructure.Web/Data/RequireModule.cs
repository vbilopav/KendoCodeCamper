namespace Infrastructure.Web.Data
{
    public class RequireModule
    {
        public string Key { get; set; }
        public string Ver { get; set; }
        public string UrlFormat { get; set; }
        public virtual string GetModuleName() { return string.Format(UrlFormat, Key, Ver); }
        public RequireModule()
        {
            UrlFormat = "{0}.{1}";
            Ver = "1";
        }
    }
}