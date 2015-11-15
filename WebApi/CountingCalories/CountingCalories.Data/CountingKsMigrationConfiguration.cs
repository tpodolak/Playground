using System.Data.Entity.Migrations;

namespace CountingKs.Data
{
  public class CountingKsMigrationConfiguration : DbMigrationsConfiguration<CountingKsContext>
  {
    public CountingKsMigrationConfiguration()
    {
      this.AutomaticMigrationsEnabled = true;
      this.AutomaticMigrationDataLossAllowed = true;
    }

#if DEBUG
    protected override void Seed(CountingKsContext context)
    {
      // Seed the database if necessary
      new CountingKsSeeder(context).Seed();
    }
#endif
  }
}
