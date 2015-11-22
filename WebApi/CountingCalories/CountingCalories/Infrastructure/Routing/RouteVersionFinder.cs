using System;
using System.Linq;
using System.Net.Http;

namespace CountingCalories.Infrastructure.Routing
{
    public class RouteVersionFinder : IRouteVersionFinder
    {
        private readonly IRouteVersionSelector[] routeSelectors;

        public RouteVersionFinder(params IRouteVersionSelector[] routeSelectors)
        {
            if (routeSelectors == null)
            {
                throw new ArgumentNullException(nameof(routeSelectors));
            }

            this.routeSelectors = routeSelectors;
        }

        public int GetVersionFromRequest(HttpRequestMessage request)
        {
            return routeSelectors.Select(val => val.GetVersion(request))
                                 .Where(val => val != null)
                                 .OrderBy(val => val.Order)
                                 .FirstOrDefault()?.Version ?? 0;
        }
    }
}