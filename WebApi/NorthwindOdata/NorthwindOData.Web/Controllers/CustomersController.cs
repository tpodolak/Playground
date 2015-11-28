using System.Linq;
using System.Web.Http.OData;
using NorthwindOData.Data;
using NorthwindOData.Entities;

namespace NorthwindOData.Web.Controllers
{
    public class CustomersController : EntitySetController<Customer, string>
    {
        NorthwindDbContext _Context = new NorthwindDbContext();

        public CustomersController()
        {
            _Context.Configuration.LazyLoadingEnabled = false;
        }

        [EnableQuery]
        public override IQueryable<Customer> Get()
        {
            return _Context.Customers;
        }

        protected override Customer GetEntityByKey(string key)
        {
            return _Context.Customers.FirstOrDefault(val => val.CustomerID == key);
        }

        [EnableQuery]
        public IQueryable<Order> GetOrdersFromCustomer([FromODataUri] string key)
        {
            return _Context.Orders.Where(val => val.CustomerID == key);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _Context.Dispose();
        }

    }
}
