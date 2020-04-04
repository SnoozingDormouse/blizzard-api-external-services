using incoming = BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels;
using entities = BlizzardData.Domain.Entities;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels
{
        public class Achievement
        {
#pragma warning disable IDE1006 // Naming Styles
            public int id { get; set; }
            public entities::Category category { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public int points { get; set; }
            public bool is_account_wide { get; set; }
            public incoming::Criteria criteria { get; set; }
            public NextAchievement next_achievement { get; set; }

#pragma warning restore IDE1006 // Naming Styles
    }
}
