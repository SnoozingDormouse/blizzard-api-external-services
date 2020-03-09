using BlizzardData.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlizzardData.Data
{
    public interface IAchievementContext
    {
        DbSet<Achievement> Achievements { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Criteria> Criteria { get; set; }

        int SaveChanges();
    }
}