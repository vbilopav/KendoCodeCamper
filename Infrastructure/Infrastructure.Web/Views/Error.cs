using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Infrastructure.Web.Views
{
    using I = Infrastructure;

    public class Error : WebViewPage<HandleErrorInfo>
    {
        public override void Execute()
        {          
            Layout = "~/Views/Shared/_Layout.cshtml";
            string subtitle = @"<span class=""fa fa-meh-o"" style=""font-size: 9em;""></span>";
            if (Model.Exception.Data["_id"] != null)
            {
                subtitle = string.Concat(subtitle,                     
                    "<span style=\"display: block;\">",
                    string.Format(Infrastructure._resx.Core.ErrorMessage, Model.Exception.Data["_id"]),
                    "</span>");                      
            }
            string content = null;
            if (I.App.Config.IsDebugConfiguration)
            {
                content = string.Concat(
                @"<br /><br /><div style=""margin-left: 10px;  text-decoration: underline;"">Exception: </div>",
                @"<div style=""color: rgb(203, 7, 51); margin-left: 10px;"">",
                Model.Exception.ToString(),
                "</div>");
            }
            Write(Html.Partial("_Static", new StaticPageModel 
            {
                Title = Infrastructure._resx.Core.GenericError,
                Subtitle = subtitle,
                AdditionalContent = content
            })); 
        }
    }
}

