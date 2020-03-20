using BlizzardData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlizzardData.Data
{
    public interface IAchievementContext
    {
        DbSet<Achievement> Achievements { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Criteria> Criteria { get; set; }
        DbSet<Character> Characters { get; set; }
        DbSet<CharacterCriteria> CharacterCriterias { get; set; }
        DbSet<Goal> Goals { get; set; }
        DbSet<GoalCriteria> GoalCriterias { get; set; }

        int SaveChanges();
    }
}