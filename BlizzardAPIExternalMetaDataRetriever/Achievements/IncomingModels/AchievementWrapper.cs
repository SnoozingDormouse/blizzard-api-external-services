using System.Collections.Generic;
using BlizzardData.Domain.Entities;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels
{
    public class AchievementWrapper
    {
        public IEnumerable<Achievement> achievements { get; set; }
    }
}
