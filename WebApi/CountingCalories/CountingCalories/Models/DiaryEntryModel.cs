
namespace CountingKs.Models
{
    public class DiaryEntryModel : ModelBase
    {
        public string FoodDescription { get; set; }
        public string MeasureDescription { get; set; }
        public string MeasureUrl { get; set; }
        public double Quantity { get; set; }
    }
}