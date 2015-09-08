﻿using System;
using Infrastructure;
﻿using Infrastructure.Web;
using Infrastructure.SqlCeConfig;

namespace Web
{   
    public class KendoCodeCamperSpaApplication : SpaHttpApplication
    {
        static KendoCodeCamperSpaApplication()
        {
            //SpaApp.Config.ViewEngines = Engines.PrecompiledViewEngine | Engines.RazorViewEngine;
            //SpaApp.Config.InlineScripts = true;
            //Configurator.ConfigureAsync(new SqlCeConfigOptions("CodeCamper"), new ConfigSettings { EnableTimer = true, TimerMinutes = 1 });
            //Configurator.ConfigureAsync(new SqlCeConfigOptions("CodeCamper")).ContinueWith(r =>
            //{
            //    if (r.Exception != null) Log.Error(r.Exception);
            //});
            Configurator.ConfigureAsync().ContinueWith(r =>
            {
                if (r.Exception != null) Log.Error(r.Exception);
            });
            //Configurator.Configure(new SqlCeConfigOptions("CodeCamper"));
            //Configurator.Configure();
        }



        protected override void Application_Start(object sender, EventArgs e)
        {
            base.Application_Start(sender, e);
            //Configurator.Configure(new SqlCeConfigOptions("CodeCamper"));
        }
    }
}