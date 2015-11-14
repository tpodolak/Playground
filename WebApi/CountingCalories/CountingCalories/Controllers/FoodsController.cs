using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Data.Entities;

namespace CountingKs.Controllers
{
    public class FoodsController : ApiController
    {
        private readonly ICountingKsRepository countingKsRepo;

        public FoodsController(ICountingKsRepository countingKsRepo)
        {
            this.countingKsRepo = countingKsRepo;
        }

        public IEnumerable<Food> Get()
        {
            var results = this.countingKsRepo.GetAllFoods()
                .OrderBy(val => val.Description)
                .Take(25)
                .ToList();

            return results;
        }
    }
}