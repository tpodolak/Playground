using System.Net.Http;

namespace CountingCalories.Infrastructure.Routing
{
    public interface IRouteVersionSelector
    {
        RouteVersionDescriptor GetVersion(HttpRequestMessage request);
    }
}