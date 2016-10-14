using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ValidationIssue.ViewModels;

namespace ValidationIssue.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var payments = new PaymentsViewModel(Enumerable.Range(0, 5).Select(val => new PaymentViewModel()));
            return View(payments);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Pay(PaymentsViewModel payments)
        {
            var result = ModelState.IsValid;
            if (result && payments.Count > 2 )
                throw new InvalidOperationException("Validation was not called");

            return RedirectToAction("Index");
        }
    }
}