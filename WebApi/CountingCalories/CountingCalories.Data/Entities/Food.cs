using System.Collections.Generic;

namespace CountingCalories.Data.Entities
{
  public class Food
  {
    public Food()
    {
      Measures = new List<Measure>();
    }

    public int Id { get; set; }
    public string Description { get; set; }

    public virtual ICollection<Measure> Measures { get; set; }
  }
}
