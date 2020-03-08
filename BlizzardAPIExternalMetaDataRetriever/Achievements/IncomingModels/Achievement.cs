using BlizzardData.Domain.Entities;
using BlizzardData.Domain.NonEntities;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels
{
        public class Achievement
        {
            public int id { get; set; }
            public Category category { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public int points { get; set; }
            public bool is_account_wide { get; set; }
            public Criteria criteria { get; set; }
            public NextAchievement next_achievement { get; set; }
    }
}
