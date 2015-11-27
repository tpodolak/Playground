using System.Linq;
using System.Web.Http;
using NorthwindOData.Data;
using NorthwindOData.Entities;

namespace NorthwindOData.Web.Controllers
{
    public class ProductsController : ApiController
    {
        NorthwindDbContext _Context = new NorthwindDbContext();
        [HttpGet]
        //[Queryable]
        public IQueryable<Product> GimmeSomeProducts()
        {
            return _Context.Products;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _Context.Dispose();
        }
    }
}
