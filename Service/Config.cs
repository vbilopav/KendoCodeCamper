using Infrastructure;

namespace Service
{
    public class ServiceConfig
    {
        public string ImageUrl = "Content/images/photos/";
    }

    public class ServiceConfigManager : ConfigManager<ServiceConfig>
    {        
    }
}
