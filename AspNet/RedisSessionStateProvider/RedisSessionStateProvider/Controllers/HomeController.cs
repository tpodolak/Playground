using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using RedisSessionStateProvider.ViewModels;

namespace RedisSessionStateProvider.Controllers
{
	[SessionState(SessionStateBehavior.Required)]
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";
			TempData["Flight"] = new FlightViewModel
			{
				FlightNumber = Guid.NewGuid().ToString()
			};
			Session["MyCustomKey"] = new FlightViewModel
			{
				FlightNumber = Guid.NewGuid().ToString()
			};
			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = TempData["Flight"];

			return View();
		}
	}
}