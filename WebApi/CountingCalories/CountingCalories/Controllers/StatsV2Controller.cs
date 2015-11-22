using System.Linq;
using System.Net.Http;
using System.Web.Http;
using CountingCalories.Data;
using CountingCalories.Infrastructure.Routing;

namespace CountingCalories.Controllers
{
    public class StatsV2Controller : ApiController
    {
        private readonly ICountingCaloriesRepository repo;

        public StatsV2Controller(ICountingCaloriesRepository repo)
        {
            this.repo = repo;
        }

        [VersionedRoute("~/api/stat/{id:int}", Version = 2)]
        public HttpResponseMessage Get(int id)
        {
            var result = new
            {
                Id = id,
                NumFoods = repo.GetAllFoods().Count(),
                NumUsers = repo.GetApiUsers().Count(),
                Version = 2
            };

            return Request.CreateResponse(result);
        }
    }
}