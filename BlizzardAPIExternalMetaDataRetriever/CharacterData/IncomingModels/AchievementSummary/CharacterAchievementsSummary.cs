using System.Collections.Generic;
using BlizzardAPIExternalMetaDataRetriever.Shared.Models;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.CharacterData.Model.AchievementSummary
{
    public class CharacterAchievementsSummary
    {
        [JsonProperty("total_quantity")]
        public int TotalQuantity { get; set; }

        [JsonProperty("total_points")]
        public int TotalPoints { get; set; }

        [JsonProperty("achievements")]
        public IEnumerable<CharacterAchievement> Achievements { get; set; }

        [JsonProperty("category_progress")]
        public IEnumerable<CategoryProgress> CategoryProgress { get; set; }

        [JsonProperty("recent_events")]
        public IEnumerable<CharacterAchievement> RecentEvents { get; set; }

        [JsonProperty("character")]
        public Character Character { get; set; }

        [JsonProperty("statistics")]
        public HttpLink Statistics { get; set; }
    }
}
