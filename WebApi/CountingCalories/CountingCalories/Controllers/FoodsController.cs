using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Data.Entities;
using CountingKs.Infrastructure;
using CountingKs.Models;
namespace CountingKs.Controllers
{
    public class FoodsController : ApiController
    {
        private readonly ICountingKsRepository countingKsRepo;
        private readonly IModelFactory modelFactory;

        public FoodsController(ICountingKsRepository countingKsRepo,IModelFactory modelFactory)
        {
            this.countingKsRepo = countingKsRepo;
            this.modelFactory = modelFactory;
        }

        public IEnumerable<FoodModel> Get(bool includeMeasures = true)
        {
            IQueryable<Food> query;
            query = includeMeasures ? countingKsRepo.GetAllFoodsWithMeasures() : countingKsRepo.GetAllFoods();
            query = query.OrderBy(val => val.Description)
                         .Take(25);

            return modelFactory.Create(query);
        }

        public FoodModel Get(int foodid)
        {
            var food = countingKsRepo.GetFood(foodid);
            return modelFactory.Create(food);
        }
    }
}