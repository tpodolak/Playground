using System.Collections.Generic;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Infrastructure;
using CountingKs.Models;

namespace CountingKs.Controllers
{
    public class MeasuresController : ApiController
    {
        private readonly ICountingKsRepository repo;
        private readonly IModelFactory modelFactory;

        public MeasuresController(ICountingKsRepository repo, IModelFactory modelFactory)
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