using System.Collections.Generic;
using CountingKs.Data.Entities;
using CountingKs.Models;

namespace CountingKs.Infrastructure
{
    public interface IModelFactory
    {
        FoodModel Create(Food food);
        IEnumerable<FoodModel> Create(IEnumerable<Food> foods);
        MeasureModel Create(Measure measure);
        IEnumerable<MeasureModel> Create(IEnumerable<Measure> measures);
    }
}