using System.Collections.Generic;
using System.Web.Http;
using CountingKs.Data;
using CountingKs.Data.Entities;
using CountingKs.Infrastructure;
using CountingKs.Models;

namespace CountingKs.Controllers
{
    public class MeasuresController : ApiController
    {
        private readonly ICountingKsRepository repo;

        public MeasuresController(ICountingKsRepository repo)
        {
            this.repo = repo;
        }

        public IEnumerable<MeasureModel> Get(int foodid)
        {
            var results = repo.GetMeasuresForFood(foodid);
            return results.Map<Measure, MeasureModel>();
        }

        public MeasureModel Get(int foodid, int id)
        {
            var result = repo.GetMeasure(id);
            if (result.Food.Id == foodid)
                return result.Map<Measure, MeasureModel>();

            return null;
        }
    }
}