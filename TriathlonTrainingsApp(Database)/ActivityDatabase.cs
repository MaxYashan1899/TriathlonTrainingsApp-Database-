using Microsoft.EntityFrameworkCore;

namespace DatabaseTrainingsApp
{
    public class ActivityDatabase : DbContext
    {
        public DbSet<Triathlon> TriathlonTrainings { get; set; }
        public DbSet<Running> RunningTrainings { get; set; }
        public DbSet<Bicycle> BicycleTrainings { get; set; }
        public DbSet<Swimming> SwimmingTrainings { get; set; }

        public ActivityDatabase()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=triathlonappdb;Trusted_Connection=True;");
        }
    }
}
