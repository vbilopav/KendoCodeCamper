using System.Web.Mvc;

namespace Infrastructure.Web.Controllers
{
    public class HomeController : Controller
    {
        
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Loading()
        {
            return View();
        }            
    }
}