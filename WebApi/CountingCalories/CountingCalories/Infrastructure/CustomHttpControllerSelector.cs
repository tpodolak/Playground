using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace CountingCalories.Infrastructure
{
    public class CustomHttpControllerSelector : DefaultHttpControllerSelector
    {
        public CustomHttpControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controllers = GetControllerMapping();
            var routeData = request.GetRouteData();
            var controllerName = (string)routeData.Values["controller"];
            HttpControllerDescriptor defaultController;
            if (controllers.TryGetValue(controllerName, out defaultController))
            {
                var apiVersion = this.GetVersionFromQueryString(request);
                var versionedControllerName = string.Concat(controllerName, "V", apiVersion);

                HttpControllerDescriptor versionedController;
                if (controllers.TryGetValue(versionedControllerName, out versionedController))
                {
                    return versionedController;
                }

                return defaultController;
            }

            return null;
        }

        private string GetVersionFromQueryString(HttpRequestMessage request)
        {
            var query = HttpUtility.ParseQueryString(request.RequestUri.Query);
            var version = query["v"];
            if (version != null)
            {
                return version;
            }
            return "1";
        }
    }
}