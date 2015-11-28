using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using Microsoft.Data.Edm;
using NorthwindOData.Entities;

namespace NorthwindOData.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapODataServiceRoute("Northwind", "odata", GetImplicitEDM());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static IEdmModel GetImplicitEDM()
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            var customers = builder.EntitySet<Customer>("Customers");
            builder.EntitySet<Order>("Orders");
            builder.EntitySet<OrderDetail>("OrderDetails");
            var customer = customers.EntityType;
            var action = customer.Action("AddLoyaltyPoints");
            action.Parameter<string>("category");
            action.Parameter<int>("points");
            action.Returns<int>();
            return builder.GetEdmModel();
        }
    }
}
