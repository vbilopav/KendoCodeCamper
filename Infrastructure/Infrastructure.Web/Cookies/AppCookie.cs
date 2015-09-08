using System;
using System.Web;
using Infrastructure.Web.Encoders;
using Newtonsoft.Json;

namespace Infrastructure.Web.Cookies
{           
    public class AppCookie
    {
        private readonly IEncoder _encoder;

        public AppCookieModel Retreive()
        {
            var c = HttpContext.Current.Request.Cookies[AppCookieConfig.Config.Name];
            if (c == null) return new AppCookieModel();
            try
            {
                return c.Value != null ? JsonConvert.DeserializeObject<AppCookieModel>(_encoder.Decode(c.Value))
                    : new AppCookieModel();
            }
            catch (Exception e)
            {
                Log.Error("Cookie could not be decoded with {0} properly! Switching to default values. Old cookie will be overwritten! Exception: {1}", 
                    new object[]{ _encoder.GetType().Name, e });
                return
                    new AppCookieModel();
            }
        }

        public void Update(AppCookieModel model)
        {
            var c = new HttpCookie(AppCookieConfig.Config.Name)
            {
                Path = AppCookieConfig.Config.Path,
                Expires = DateTime.Now.AddDays(AppCookieConfig.Config.ExpireDays),
                Value = _encoder.Encode(JsonConvert.SerializeObject(model, Formatting.None))
            };
            if (AppCookieConfig.Config.Domain != null) c.Domain = AppCookieConfig.Config.Domain;
            if (AppCookieConfig.Config.HttpOnly != null) c.HttpOnly = (bool)AppCookieConfig.Config.HttpOnly;
            if (AppCookieConfig.Config.Secure != null) c.Secure = (bool)AppCookieConfig.Config.Secure;
            if (AppCookieConfig.Config.Shareable != null) c.Shareable = (bool)AppCookieConfig.Config.Shareable;
            HttpContext.Current.Response.Cookies.Set(c);
        }

        public AppCookie(IEncoder encoder)
        {
            _encoder = encoder;
        }

        public AppCookie() : this(new PlainTextEncoder())
        {
        }
    }
}
