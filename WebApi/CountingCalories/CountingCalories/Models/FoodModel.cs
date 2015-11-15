using System.Collections.Generic;

namespace CountingKs.Models
{
    public class FoodModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<MeasureModel> Measures { get; set; }
    }
}