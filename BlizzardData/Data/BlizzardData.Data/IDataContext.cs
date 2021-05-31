using BlizzardData.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlizzardData.Data
{
    public interface IDataContext
    {
        DbSet<Achievement> Achievements { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Criteria> Criteria { get; set; }
        DbSet<AchievementCriteria> AchievementCriterias { get; set; }
        DbSet<CriteriaCriteria> CriteriaCriterias { get; set; }
        DbSet<Goal> Goals { get; set; }
        DbSet<GoalCriteria> GoalCriterias { get; set; }
        DbSet<BattlePet> BattlePets { get; set; }
        DbSet<BattlePetAbility> BattlePetAbilities { get; set; }
        DbSet<BattlePetBattlePetAbility> BattlePetBattlePetAbilities { get; set; }


        // Reputation
        DbSet<ReputationFaction> ReputationFactions { get; set; }
        DbSet<CriteriaReputation> CriteriaReputations { get; set; }

        // Character Data
        DbSet<Character> Characters { get; set; }
        DbSet<CharacterAchievement> CharacterAchievements { get; set; }
        DbSet<CharacterCriteria> CharacterCriterias { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}