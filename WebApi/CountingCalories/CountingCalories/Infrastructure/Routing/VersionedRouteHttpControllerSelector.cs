using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace CountingCalories.Infrastructure.Routing
{
    public class VersionedRouteHttpControllerSelector : DefaultHttpControllerSelector
    {
        private readonly IRouteVersionFinder versionFinder;
        public VersionedRouteHttpControllerSelector(HttpConfiguration configuration, IRouteVersionFinder versionFinder) : base(configuration)
        {
            this.versionFinder = versionFinder;
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controllers = GetControllerMapping();
            var routeData = request.GetRouteData();
            var controllerName = (string)routeData.Values["controller"];
            HttpControllerDescriptor defaultController;

            // no controller name, wrong route or attribute based routing, leave versioning for VersionedRouteAttribute
            // or use default behavior
            // note this selector doesn't supprot overriding global routes with route attributes
            if (string.IsNullOrWhiteSpace(controllerName))
            {
                return base.SelectController(request);
            }

            if (controllers.TryGetValue(controllerName, out defaultController))
            {

                var apiVersion = GetApiVersion(request);
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

        private int GetApiVersion(HttpRequestMessage request)
        {
            return versionFinder.GetVersionFromRequest(request);
        }
    }
}