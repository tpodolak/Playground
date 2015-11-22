using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace CountingCalories.Infrastructure.Routing
{
    public class ApiVersionHeaderRouteVersionSelector : IRouteVersionSelector
    {
        public RouteVersionDescriptor GetVersion(HttpRequestMessage request)
        {
            const string versionHeader = "X-Api-Version";
            IEnumerable<string> headers;
            if (!request.Headers.TryGetValues(versionHeader, out headers)) return null;

            var rawVersion = request.Headers.GetValues(versionHeader).FirstOrDefault();
            int version;
            if (int.TryParse(rawVersion, out version))
            {
                if (!string.IsNullOrWhiteSpace(rawVersion))
                {
                    return new RouteVersionDescriptor
                    {
                        Order = 3,
                        Version = version
                    };
                }
            }
            return null;
        }
    }
}