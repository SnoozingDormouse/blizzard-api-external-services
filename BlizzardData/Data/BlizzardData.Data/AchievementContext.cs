using BlizzardData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlizzardData.Data
{
    public class AchievementContext : DbContext
    {
        public AchievementContext(DbContextOptions<AchievementContext> options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Achievement>()
                .Property(a => a.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Category>()
                .Property(c => c.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Criteria>()
                .Property(c => c.Id)
                .ValueGeneratedNever();
        }

        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Criteria> Criteria { get; set; }
    }
}
