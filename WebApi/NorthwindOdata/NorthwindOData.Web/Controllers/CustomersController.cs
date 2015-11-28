using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Results;
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

        protected override Customer UpdateEntity(string key, Customer update)
        {
            update.CustomerID = key;
            _Context.Customers.Attach(update);
            _Context.Entry(update).State = EntityState.Modified;
            _Context.SaveChanges();
            return update;
        }

        protected override Customer PatchEntity(string key, Delta<Customer> patch)
        {
            var customer = _Context.Customers.FirstOrDefault(val => val.CustomerID == key);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            patch.Patch(customer);
            _Context.SaveChanges();
            return customer;
        }

        protected override Customer CreateEntity(Customer entity)
        {
            _Context.Customers.Add(entity);
            _Context.SaveChanges();
            return entity;
        }

        public override void Delete([FromODataUri] string key)
        {
            var customer = _Context.Customers.FirstOrDefault(val => val.CustomerID == key);
            _Context.Customers.Remove(customer);
            _Context.SaveChanges();
        }

        protected override string GetKey(Customer entity)
        {
            return entity.CustomerID;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _Context.Dispose();
        }

    }
}
