using CodeFirstConfig;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Infrastructure.Web.Controllers
{    
    [RoutePrefix("Config")]
    public class ConfigurationController : ApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<HttpResponseMessage> Get()
        {
            if (User.IsInRole("_sysadmin") || !Request.IsLocal())
                return new HttpResponseMessage(HttpStatusCode.Forbidden);

            var format = ConfigSettings.Instance.ConfigFileFormat;
            if (HttpContext.Current.Request.QueryString.Count > 0)
            {
                if (!Enum.TryParse(HttpContext.Current.Request.QueryString[0], true, out format))
                    format = ConfigSettings.Instance.ConfigFileFormat;
            }
            Configurator.SerializeCurrent(HttpContext.Current.Response.Output, format);
            HttpContext.Current.Response.ContentType = "application/json charset=utf-8";
            await HttpContext.Current.Response.Output.FlushAsync();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
	}     
}