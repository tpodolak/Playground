using System.Data.Entity.Migrations;

namespace CountingCalories.Data
{
  public class CountingCaloriesMigrationConfiguration : DbMigrationsConfiguration<CountingCaloriesContext>
  {
    public CountingCaloriesMigrationConfiguration()
    {
      this.AutomaticMigrationsEnabled = true;
      this.AutomaticMigrationDataLossAllowed = true;
    }

#if DEBUG
    protected override void Seed(CountingCaloriesContext context)
    {
      // Seed the database if necessary
      new CountingCaloriesSeeder(context).Seed();
    }
#endif
  }
}
