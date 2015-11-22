using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace CountingCalories.Infrastructure.Routing
{
    public class VersionConstraint : IHttpRouteConstraint
    {
        private readonly int version;

        public VersionConstraint(int version)
        {
            this.version = version;
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            // TODO figure out better way than service location;
            var versionFinder = (IRouteVersionFinder)request.GetDependencyScope().GetService(typeof(IRouteVersionFinder));
            var currentVersion = versionFinder.GetVersionFromRequest(request);
            return version == currentVersion;
        }
    }
}