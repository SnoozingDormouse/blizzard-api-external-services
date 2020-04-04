using BlizzardData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlizzardData.Data
{
    public class AchievementContext : DbContext, IAchievementContext
    {
        public AchievementContext(DbContextOptions<AchievementContext> options)
            : base(options)
        { }

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

            modelBuilder.Entity<CharacterCriteria>()
                .HasKey(c => new { c.CharacterId, c.CriteriaId });

            modelBuilder.Entity <GoalCriteria>()
                .HasKey(c => new { c.GoalId, c.CriteriaId });
        }

        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Criteria> Criteria { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterCriteria> CharacterCriterias { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<GoalCriteria> GoalCriterias { get; set; }
    }
}
