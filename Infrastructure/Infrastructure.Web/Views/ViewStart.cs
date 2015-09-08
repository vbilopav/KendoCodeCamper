using System.Web.Mvc;

namespace Infrastructure.Web.Views
{
    public class ViewStart : ViewStartPage
    {
        public override void Execute()
        {
            Layout = "~/Views/Shared/_Layout.cshtml";
        }
    }
}

