using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;

namespace Infrastructure.Web.Views
{
    [PageVirtualPathAttribute("~/Views/Home/Loading.cshtml")]
    public class Loading : WebViewPage
    {
        public override void Execute()
        {
            Layout = "~/Views/Shared/_Layout.cshtml";
            Write(Html.Partial("_Static", new StaticPageModel 
            {
                Title = Infrastructure._resx.Core.Loading,
                Subtitle = "<span class=\"fa fa-cog fa-spin\" style=\"font-size: 10em;\"><span/>"
            }));                    
        }
    }
}

