using System.Web.Mvc;

namespace Infrastructure.Web.Views
{
    using Extensions;

    public class StaticPageModel
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string AdditionalContent { get; set; }
    }

    public class Static : WebViewPage<StaticPageModel>
    {
        public override void Execute()
        {
            this.LiteralLine(@"
        <div id=""loading"" style=""visibility:hidden; opacity:0; margin-top: 50px;"">
        <div class=""fade in"" style=""  z-index: 99999; overflow: visible; color: #bbb; width: 100%; text-align: center;"">                    
            <span class=""h2 fade in"" style=""z-index: 99999; overflow: visible; color: #bbb;  display: block;"">          
                ", Model.Title, @"
            </span>",
                Model.Subtitle, @"
        </div>", Model.AdditionalContent, @"
      </div>
", SpaApp.GetScriptInline(
                    SpaApp.MinifyJavaScript(string.Concat(@"(
            function () {
                setTimeout(function () {
                    var e = document.getElementById('loading');
                    if (e) {
                        e.setAttribute('style', 'visibility:visible; opacity:1; transition: opacity 2s ease-out; margin-top: 50px;');
                        document.title = '", Model.Title, @"';
                    }
                }, 1000);
            })()"))));
        }
    }
}

