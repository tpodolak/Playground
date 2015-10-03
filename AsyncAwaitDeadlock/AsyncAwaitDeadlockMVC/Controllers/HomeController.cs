using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AsyncAwaitDeadlock.Controllers
{
    public class HomeController : Controller
    {
        private static readonly TimeSpan LongRunningOperationTime = TimeSpan.FromSeconds(3);

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Deadlock()
        {
            LongRunningOperationDefaultAwait().Wait();
            return View();
        }

        public async Task<ActionResult> Blocking()
        {
            LongRunningOperationConfigureAwait().Wait();
            return View();
        }

        public async Task<ActionResult> Proper()
        {
            await LongRunningOperationDefaultAwait();
            return View();
        }

        private async Task LongRunningOperationDefaultAwait()
        {
            await Task.Delay(LongRunningOperationTime);
        }

        private async Task LongRunningOperationConfigureAwait()
        {
            await Task.Delay(LongRunningOperationTime).ConfigureAwait(false);
        }
    }
}