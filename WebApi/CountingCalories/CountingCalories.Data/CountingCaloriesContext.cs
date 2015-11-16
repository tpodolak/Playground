using System.Data.Entity;
using CountingCalories.Data.Entities;

namespace CountingCalories.Data
{
  public class CountingCaloriesContext : DbContext
  {
    public CountingCaloriesContext()
      : base("DefaultConnection")
    {
      this.Configuration.LazyLoadingEnabled = false;
      this.Configuration.ProxyCreationEnabled = false;
    }

    static CountingCaloriesContext()
    {
      Database.SetInitializer(new MigrateDatabaseToLatestVersion<CountingCaloriesContext, CountingCaloriesMigrationConfiguration>());
    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      CountingCaloriesMapping.Configure(modelBuilder);
    }

    public DbSet<ApiUser> ApiUsers { get; set; }
    public DbSet<AuthToken> AuthTokens { get; set; }
    public DbSet<Food> Foods { get; set; }
    public DbSet<Measure> Measures { get; set; }
    public DbSet<Diary> Diaries { get; set; }
    public DbSet<DiaryEntry> DiaryEntries { get; set; }
  }
}
