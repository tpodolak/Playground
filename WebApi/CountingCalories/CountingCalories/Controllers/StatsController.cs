using System.Linq;
using System.Net.Http;
using System.Web.Http;
using CountingCalories.Data;
using CountingCalories.Infrastructure.Routing;

namespace CountingCalories.Controllers
{
    [RoutePrefix("api/stats")]
    public class StatsController : ApiController
    {
        private readonly ICountingCaloriesRepository repo;

        public StatsController(ICountingCaloriesRepository repo)
        {
            this.repo = repo;
        }

        [Route("")]
        public HttpResponseMessage Get()
        {
            var result = new
            {
                NumFoods = repo.GetAllFoods().Count(),
                NumUsers = repo.GetApiUsers().Count()
            };

            return Request.CreateResponse(result);
        }

        // tilde overrides default prefix
        [VersionedRoute("~/api/stat/{id:int}")]
        public HttpResponseMessage Get(int id)
        {
            var result = new
            {
                Id = id,
                NumFoods = repo.GetAllFoods().Count(),
                NumUsers = repo.GetApiUsers().Count()
            };

            return Request.CreateResponse(result);
        }

        // tilde overrides default prefix
        [VersionedRoute("~/api/stat/{name:alpha}")]
        public HttpResponseMessage Get(string name)
        {
            var result = new
            {
                Name = name,
                NumFoods = repo.GetAllFoods().Count(),
                NumUsers = repo.GetApiUsers().Count()
            };

            return Request.CreateResponse(result);
        }
    }
}