using System.Linq;
using System.Web.Mvc;
using CountingCalories.Data;

namespace CountingCalories.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      var repo = new CountingCaloriesRepository(new CountingCaloriesContext());

      var results = repo.GetAllFoodsWithMeasures().Take(25).ToList();

      return View(results);
    }

    public ActionResult About()
    {
      ViewBag.Message = "Your app description page.";

      return View();
    }
  }
}
