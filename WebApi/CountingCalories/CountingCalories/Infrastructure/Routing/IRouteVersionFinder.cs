using System.Net.Http;

namespace CountingCalories.Infrastructure.Routing
{
    public interface IRouteVersionFinder
    {
        int GetVersionFromRequest(HttpRequestMessage request);
    }
}