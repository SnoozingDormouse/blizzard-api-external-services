using System;
using BlizzardAPIExternalMetaDataRetriever.Shared.Models;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.CharacterData.Model.AchievementSummary
{
    public class CharacterAchievement
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("achievement")]
        public KeyNameId Achievement { get; set; }

        [JsonProperty("criteria")]
        public Criteria Criteria { get; set; }

        [JsonProperty("completed_timestamp")]
        public UInt64 TimeStamp { get; set; }
    }
}
