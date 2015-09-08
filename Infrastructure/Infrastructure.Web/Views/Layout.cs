using System.Web.Mvc;
using System.Web.Optimization;
using System.Globalization;

namespace Infrastructure.Web.Views
{
    using Extensions;

    public class Layout : WebViewPage
    {
        public override void Execute()
        {
            if (Infrastructure.App.Config.IsDebugConfiguration)
                this.LiteralLine("<!-- DEBUG -->");
            this.LiteralLine(@"<!DOCTYPE html>
<html lang=""", CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, @""">
    <head>
        <meta charset=""utf-8"">
        <meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"">
        <title></title>
        <meta name=""description"" content=""", SpaApp.Config.Description, @""">
        <meta name=""viewport"" content=""width=device-width, initial-scale=1"">");
            Write(Styles.Render("~/Content/css"));
            this.Literal(@"</head>
        <body>
            <div id=""applicationHost"">");
            Write(RenderBody());
            this.Literal(@"
            </div>
        </body>
</html>");
        }
    }
}
