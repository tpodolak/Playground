using System;
using System.Linq;
using System.Net.Http;

namespace CountingCalories.Infrastructure.Routing
{
    public class AcceptHeaderRouteVersionSelector : IRouteVersionSelector
    {
        public RouteVersionDescriptor GetVersion(HttpRequestMessage request)
        {
            // this supports only one media type, for prod we should also take care of others like application/xml etc
            var headerApiVersion = request.Headers.Accept.Where(val => val.MediaType == "application/json")
                .SelectMany(val => val.Parameters)
                .FirstOrDefault(val => val.Name.Equals("version", StringComparison.InvariantCultureIgnoreCase));

            var rawVersion = headerApiVersion?.Value;
            int version;

            if (!int.TryParse(rawVersion, out version)) return null;

            if (!string.IsNullOrWhiteSpace(rawVersion))
            {
                return new RouteVersionDescriptor
                {
                    Order = 2,
                    Version = version
                };
            }
            return null;
        }
    }
}