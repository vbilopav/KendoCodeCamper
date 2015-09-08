using System.Linq;
using System.Web.Optimization;
using System.Web.Mvc;
using System.Globalization;
using Infrastructure.Web.Data;

namespace Infrastructure.Web.Views
{
    using Extensions;
    using I = Infrastructure;

    public class References : WebViewPage
    {
        private void WriteInlineSrcAndBundleSrc()
        {
            Write(Html.GetGlobalScriptScriptInline());
            this.LiteralLine();
            Write(Scripts.Render(SpaApp.Config.ScriptBundle.Replace("{culture}", CultureInfo.CurrentUICulture.TwoLetterISOLanguageName)));
        }

        public override void Execute()
	    {
            if (I.App.Config.IsDebugConfiguration)
            {
                foreach (var @ref in SpaApp.Config.References.Where(@ref => !@ref.Value.Skip))
                {
                    if (@ref.Key != Library.RequireJs)
                        this.LiteralLine(
                            SpaApp.GetScript(Url.Content(string.Format(@ref.Value.LocalUrl, SpaApp.LibVersion(@ref.Key)))));                           
                    else
                    {
                        WriteInlineSrcAndBundleSrc();
                        this.Literal(
                            SpaApp.GetScript(Url.Content(string.Format(@ref.Value.LocalUrl, SpaApp.LibVersion(@ref.Key))),
                                "data-main", Url.Content(SpaApp.Config.MainJsModule)));
                    }
                }                
            }
            else
            {
                foreach (var @ref in SpaApp.Config.References.Where(@ref => !@ref.Value.Skip))
                {
                    if (@ref.Key != Library.RequireJs)
                    {
                        if (@ref.Value.CdnUrl != null)
                        {
                            this.LiteralLine(
                                SpaApp.GetScript(Url.Content(string.Format(@ref.Value.CdnUrl, SpaApp.LibVersion(@ref.Key)))));

                            if (@ref.Value.FallbackUrl != null && @ref.Value.FallbackExp != null)
                                this.LiteralLine(
                                    SpaApp.GetScriptInline(
                                        string.Concat(
                                            @ref.Value.FallbackExp,
                                            " || document.write('<script type=\"text/javascript\" src=\"",
                                            Url.Content(string.Format(@ref.Value.FallbackUrl, SpaApp.LibVersion(@ref.Key))),
                                            "\">\\x3C/script>')")));
                        }
                        else
                        {
                            if (@ref.Value.FallbackUrl != null)
                                this.LiteralLine(
                                    SpaApp.GetScript(Url.Content(string.Format(@ref.Value.FallbackUrl, SpaApp.LibVersion(@ref.Key)))));
                        }
                    }
                    else
                    {
                        WriteInlineSrcAndBundleSrc(); 
                        if (@ref.Value.CdnUrl != null)
                        {
                            this.LiteralLine(
                                SpaApp.GetScript(Url.Content(string.Format(@ref.Value.CdnUrl, SpaApp.LibVersion(@ref.Key))),
                                "data-main", Url.Content(SpaApp.Config.MainJsModule)));

                            if (@ref.Value.FallbackUrl != null && @ref.Value.FallbackExp != null)

                                this.Literal(
                                   SpaApp.GetScriptInline(
                                       string.Concat(
                                           @ref.Value.FallbackExp,
                                           " || document.write('<script type=\"text/javascript\" data-main=\"",
                                            Url.Content(SpaApp.Config.MainJsModule),
                                           "\" src=\"",
                                           Url.Content(string.Format(@ref.Value.FallbackUrl, SpaApp.LibVersion(@ref.Key))),
                                           "\">\\x3C/script>')")));                                                                                  
                        }
                        else
                        {
                            if (@ref.Value.FallbackUrl != null)
                                this.Literal(
                                    SpaApp.GetScript(Url.Content(string.Format(@ref.Value.FallbackUrl, SpaApp.LibVersion(@ref.Key))),
                                    "data-main", Url.Content(SpaApp.Config.MainJsModule)));
                        }
                        if (SpaApp.Config.InlineScripts)
                        {
                            Write(Html.MainJavaScriptBuildInlineContent());
                        }
                    }
                }                
            }
	    }
    }
}