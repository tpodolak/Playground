using System.Net.Http;
using System.Web;

namespace CountingCalories.Infrastructure.Routing
{
    public class QueryStringRouteVersionSelector : IRouteVersionSelector
    {
        public RouteVersionDescriptor GetVersion(HttpRequestMessage request)
        {
            var query = HttpUtility.ParseQueryString(request.RequestUri.Query);
            var rawVersion = query["v"];
            int version;
            if (int.TryParse(rawVersion, out version))
            {
                return new RouteVersionDescriptor
                {
                    Order = 1,
                    Version = version
                };
            }

            return null;
        }
    }
}