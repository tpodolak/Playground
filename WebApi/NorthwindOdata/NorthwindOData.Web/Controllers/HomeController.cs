using System.Web.Mvc;

namespace NorthwindOData.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
