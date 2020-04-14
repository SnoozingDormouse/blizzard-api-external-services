using System.Collections.Generic;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels
{
    public class AchievementsIndex
    {
        [JsonProperty("achievements")]
        public IEnumerable<Achievement> Achievements { get; set; }
    }
}
