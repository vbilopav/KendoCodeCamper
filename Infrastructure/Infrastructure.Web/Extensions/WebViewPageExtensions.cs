using System.Text;
using System.Web.Mvc;

namespace Infrastructure.Web.Extensions
{
    public static class WebViewPageExtensions
    {
        private static string Concat(object[] @params)
        {
            var sb = new StringBuilder(@params.Length);
            foreach (object p in @params) sb.Append(p);
            return sb.ToString();
        }

        public static void Literal(this WebViewPage page, params object[] @params)
        {
            page.Output.Write(Concat(@params));
        }

        public static void LiteralLine(this WebViewPage page, params object[] @params)
        {           
            page.Output.WriteLine(Concat(@params));
        }

        public static void LiteralLines(this WebViewPage page, params object[] @params)
        {
            page.Output.WriteLine(string.Join("\t\n", @params));
        }       
    }
}
