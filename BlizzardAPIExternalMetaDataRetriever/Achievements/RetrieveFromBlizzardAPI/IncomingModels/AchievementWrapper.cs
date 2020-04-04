using System.Collections.Generic;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels
{
    public class AchievementWrapper
    {
        public IEnumerable<Achievement> achievements { get; set; }
        public Character character { get; set; }
    }
}
