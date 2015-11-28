using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.OData;
using System.Web.Http.OData.Extensions;
using System.Web.Http.OData.Results;
using Microsoft.Data.OData;
using NorthwindOData.Data;
using NorthwindOData.Entities;

namespace NorthwindOData.Web.Controllers
{
    public class CustomersController : EntitySetController<Customer, string>
    {
        readonly NorthwindDbContext context = new NorthwindDbContext();

        public CustomersController()
        {
            context.Configuration.LazyLoadingEnabled = false;
        }

        [EnableQuery]
        public override IQueryable<Customer> Get()
        {
            return context.Customers;
        }

        protected override Customer GetEntityByKey(string key)
        {
            return context.Customers.FirstOrDefault(val => val.CustomerID == key);
        }

        [EnableQuery]
        public IQueryable<Order> GetOrdersFromCustomer([FromODataUri] string key)
        {
            return context.Orders.Where(val => val.CustomerID == key);
        }

        protected override Customer UpdateEntity(string key, Customer update)
        {
            EnsureEntityExists(key);

            update.CustomerID = key;
            context.Customers.Attach(update);
            context.Entry(update).State = EntityState.Modified;
            context.SaveChanges();
            return update;
        }

        protected override Customer PatchEntity(string key, Delta<Customer> patch)
        {
            EnsureEntityExists(key);
            var customer = context.Customers.FirstOrDefault(val => val.CustomerID == key);
            if (customer == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            patch.Patch(customer);
            context.SaveChanges();
            return customer;
        }

        protected override Customer CreateEntity(Customer entity)
        {
            context.Customers.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public override void Delete([FromODataUri] string key)
        {
            EnsureEntityExists(key);
            var customer = context.Customers.FirstOrDefault(val => val.CustomerID == key);
            context.Customers.Remove(customer);
            context.SaveChanges();
        }

        protected override string GetKey(Customer entity)
        {
            return entity.CustomerID;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            context.Dispose();
        }

        private void EnsureEntityExists(string key)
        {
            if (!context.Customers.Any(val => val.CustomerID == key))
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, new ODataError
                {
                    ErrorCode = "EntityNotFound",
                    Message = $"Customer key {key} not found"
                }));
            }
        }
    }
}
