using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Infrastructure.Web.Views
{
    public class Index : WebViewPage
    {
        public override void Execute()
        {
            Layout = "~/Views/Shared/_Layout.cshtml";
            if (SpaApp.GetShowPageLoadingProgress())
            {               
                Write(Html.Partial("_Static", new StaticPageModel 
                { 
                    Title = Infrastructure._resx.Core.Loading,
                    Subtitle = "<span class=\"fa fa-cog fa-spin\" style=\"font-size: 10em;\"><span/>"
                })); 
            }
            Write(Html.Partial("_References"));
        }
    }
}

