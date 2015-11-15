using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;
using CountingKs.Data.Entities;
using CountingKs.Models;

namespace CountingKs.Infrastructure
{
    public class ModelFactory : IModelFactory
    {
        private readonly UrlHelper urlHelper;
        public ModelFactory(UrlHelper urlHelper)
        {
            this.urlHelper = urlHelper;
        }

        public FoodModel Create(Food food)
        {
            var result = new FoodModel
            {
                Id = food.Id,
                Url = urlHelper.Link("Foods", new {foodid = food.Id}),
                Description = food.Description,
                Measures = food.Measures.Select(Create).ToList()
            };
            return result;
        }

        public IEnumerable<FoodModel> Create(IEnumerable<Food> foods)
        {
            if (foods == null)
                return Enumerable.Empty<FoodModel>();

            return foods.Select(this.Create);
        }

        public MeasureModel Create(Measure measure)
        {
            var result = new MeasureModel
            {
                Id = measure.Id,
                Url = urlHelper.Link("Measures", new { id = measure.Id, foodid = measure.Food.Id }),
                Calories = measure.Calories,
                Description = measure.Description,
            };
            return result;
        }

        public IEnumerable<MeasureModel> Create(IEnumerable<Measure> measures)
        {
            if (measures == null)
                return Enumerable.Empty<MeasureModel>();
            return measures.Select(this.Create);
        }
    }
}