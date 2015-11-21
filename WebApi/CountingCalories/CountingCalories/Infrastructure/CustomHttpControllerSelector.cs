using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace CountingCalories.Infrastructure
{
    public class CustomHttpControllerSelector : DefaultHttpControllerSelector
    {
        const string DefaultApiVersion = "1";
        public CustomHttpControllerSelector(HttpConfiguration configuration) : base(configuration)
        {
        }

        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            var controllers = GetControllerMapping();
            var routeData = request.GetRouteData();
            var controllerName = (string)routeData.Values["controller"];
            HttpControllerDescriptor defaultController;

            // skip versioning for attribute routing
            if (string.IsNullOrWhiteSpace(controllerName))
            {
                return base.SelectController(request);
            }

            if (controllers.TryGetValue(controllerName, out defaultController))
            {
                // var apiVersion = GetVersionFromQueryString(request);
                // var apiVersion = GetVersionFromHeader(request);
                // var apiVersion = GetVersionFromAcceptHeaderVersion(request);
                var apiVersion = GetVersionFromMediaType(request);
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
            return DefaultApiVersion;
        }

        private string GetVersionFromHeader(HttpRequestMessage request)
        {
            const string versionHeader = "X-Api-Version";

            var headerApiVersion = request.Headers.GetValues(versionHeader).FirstOrDefault();

            return string.IsNullOrWhiteSpace(headerApiVersion) ? DefaultApiVersion : headerApiVersion;
        }

        private string GetVersionFromAcceptHeaderVersion(HttpRequestMessage request)
        {
            // this supports only one media type, for prod we should also take care of others like application/xml etc
            var headerApiVersion = request.Headers.Accept.Where(val => val.MediaType == "application/json")
                .SelectMany(val => val.Parameters)
                .FirstOrDefault(val => val.Name.Equals("version", StringComparison.InvariantCultureIgnoreCase));
            return headerApiVersion != null ? headerApiVersion.Value : DefaultApiVersion;
        }

        private string GetVersionFromMediaType(HttpRequestMessage request)
        {
            var accept = request.Headers.Accept;
            var ex = new Regex(@"application\/vnd\.coutingcalories\.([a-z]+)\.v([0-9]+)\+json", RegexOptions.IgnoreCase);

            foreach (var match in accept.Select(mime => ex.Match(mime.MediaType)))
                return match.Groups[2].Value;

            return "1";
        }
    }
}