using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using NorthwindOData.Data;
using NorthwindOData.Entities;

namespace NorthwindOData.Web.Controllers
{
    public class CustomersController : ApiController
    {
        NorthwindDbContext _Context = new NorthwindDbContext();

        public CustomersController()
        {
            _Context.Configuration.LazyLoadingEnabled = false;
        }

        // GET /api/Customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _Context.Customers.ToArray();
        }

        // GET /api/Customers/ALFKI
        public Customer GetCustomer(string id)
        {
            Customer customer = _Context.Customers.FirstOrDefault(c => c.CustomerID == id);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return customer;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _Context.Dispose();
        }

    }
}
