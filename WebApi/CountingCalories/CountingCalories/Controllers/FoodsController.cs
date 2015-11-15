using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Data.Entities;
using CountingKs.Models;
using CountingKs.Infrastructure;
namespace CountingKs.Controllers
{
    public class FoodsController : ApiController
    {
        private readonly ICountingKsRepository countingKsRepo;

        public FoodsController(ICountingKsRepository countingKsRepo)
        {
            this.countingKsRepo = countingKsRepo;
        }

        public IEnumerable<FoodModel> Get(bool includeMeasures = true)
        {
            IQueryable<Food> query;
            query = includeMeasures ? countingKsRepo.GetAllFoodsWithMeasures() : countingKsRepo.GetAllFoods();
            query = query.OrderBy(val => val.Description)
                         .Take(25);

            return query.Map<Food, FoodModel>();
        }

        public FoodModel Get(int foodid)
        {
            return countingKsRepo.GetFood(foodid).Map<Food, FoodModel>();
        }
    }
}