using BlizzardData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BlizzardData.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
                throw new ArgumentNullException();

            modelBuilder.Entity<Achievement>()
                .Property(a => a.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Category>()
                .Property(c => c.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Criteria>()
                .Property(c => c.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Character>()
                .HasKey(c => new { c.BlizzardId });

            modelBuilder.Entity<AchievementCriteria>()
                .HasKey(c => new { c.AchievementId, c.CriteriaId });

            modelBuilder.Entity<CriteriaCriteria>()
                .HasKey(c => new { c.CriteriaId, c.ChildCriteriaId });

            modelBuilder.Entity<CharacterCriteria>()
                .HasKey(c => new { c.CharacterId, c.CriteriaId });

            modelBuilder.Entity<CharacterAchievement>()
                .HasKey(c => new { c.CharacterId, c.AchievementId });

            modelBuilder.Entity<GoalCriteria>()
                .HasKey(c => new { c.GoalId, c.CriteriaId });

            modelBuilder
               .Entity<ReputationFaction>()
               .Property(e => e.PlayerFaction)
               .HasConversion(
                   v => v.ToString(),
                   v => (PlayerFaction)Enum.Parse(typeof(PlayerFaction), v));

            modelBuilder.Entity<CriteriaReputation>()
                .HasKey(c => new { c.CriteriaId, c.ReputationId });

            modelBuilder.Entity<BattlePet>()
                .Property(p => p.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<BattlePetAbility>()
                .Property(a => a.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<BattlePetBattlePetAbility>()
                .HasKey(c => new { c.BattlePetId, c.BattlePetAbilityId });
        }

        // Game Data
        public virtual DbSet<Achievement> Achievements { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Criteria> Criteria { get; set; }
        public virtual DbSet<AchievementCriteria> AchievementCriterias { get; set; }
        public virtual DbSet<CriteriaCriteria> CriteriaCriterias { get; set; }

        public virtual DbSet<ReputationFaction> ReputationFactions { get; set; }
        public virtual DbSet<CriteriaReputation> CriteriaReputations { get; set; }

        public virtual DbSet<BattlePet> BattlePets { get; set; }
        public virtual DbSet<BattlePetAbility> BattlePetAbilities { get; set; }
        public virtual DbSet<BattlePetBattlePetAbility> BattlePetBattlePetAbilities { get; set; }


        // Manually created links
        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<GoalCriteria> GoalCriterias { get; set; }


        // Character Data
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<CharacterAchievement> CharacterAchievements { get; set; }
        public virtual DbSet<CharacterCriteria> CharacterCriterias { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
