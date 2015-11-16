using System.Collections.Generic;
using System.Web.Http;
using CountingCalories.Data;
using CountingCalories.Infrastructure;
using CountingCalories.Models;

namespace CountingCalories.Controllers
{
    public class MeasuresController : ApiController
    {
        private readonly ICountingCaloriesRepository repo;
        private readonly IModelFactory modelFactory;

        public MeasuresController(ICountingCaloriesRepository repo, IModelFactory modelFactory)
        {
            this.repo = repo;
            this.modelFactory = modelFactory;
        }

        public IEnumerable<MeasureModel> Get(int foodid)
        {
            var results = repo.GetMeasuresForFood(foodid);
            return modelFactory.Create(results);
        }

        public MeasureModel Get(int foodid, int id)
        {
            var result = repo.GetMeasure(id);
            if (result.Food.Id == foodid)
                return modelFactory.Create(result);

            return null;
        }
    }
}