using System.Collections.Generic;
using System.Web.Http;
using CountingCalories.Data;
using CountingCalories.Infrastructure;
using CountingCalories.Models;

namespace CountingCalories.Controllers
{
    public class MeasuresV2Controller : ApiController
    {
        private readonly ICountingCaloriesRepository repo;
        private readonly IModelFactory modelFactory;

        public MeasuresV2Controller(ICountingCaloriesRepository repo, IModelFactory modelFactory)
        {
            this.repo = repo;
            this.modelFactory = modelFactory;
        }

        public IEnumerable<MeasureV2Model> Get(int foodid)
        {
            var results = repo.GetMeasuresForFood(foodid);
            return modelFactory.CreateMeasure(results);
        }

        public MeasureV2Model Get(int foodid, int id)
        {
            var result = repo.GetMeasure(id);
            if (result.Food.Id == foodid)
                return modelFactory.CreateMeasure(result);

            return null;
        }
    }
}