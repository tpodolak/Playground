using System;
using System.Web.Http;
using System.Web.Http.OData;
using NorthwindOData.Entities;

namespace NorthwindOData.Web.Controllers
{
    public class OrdersController : EntitySetController<Order, int>
    {
        public override void CreateLink([FromODataUri] int key, string navigationProperty, [FromBody] Uri link)
        {

        }
    }
}