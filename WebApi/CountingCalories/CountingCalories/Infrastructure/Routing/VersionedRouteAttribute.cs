using System.Collections.Generic;
using System.Web.Http.Routing;

namespace CountingCalories.Infrastructure.Routing
{
    /// <summary>
    /// Variation over VersionedRouteAttribute found in ASP.NET Web API 2 Recipes http://www.apress.com/9781430259800
    /// </summary>
    public class VersionedRouteAttribute : RouteFactoryAttribute
    {
        public int Version { get; set; }
        public VersionedRouteAttribute(string template) : base(template)
        {
            Order = -1;
        }

        public override IDictionary<string, object> Constraints => new Dictionary<string, object> { [""] = new VersionConstraint(Version) };
    }
}