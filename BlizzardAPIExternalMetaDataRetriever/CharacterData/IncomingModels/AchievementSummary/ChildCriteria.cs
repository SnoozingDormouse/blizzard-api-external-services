using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BlizzardAPIExternalMetaDataRetriever.CharacterData.Model.AchievementSummary
{
    public class ChildCriteria
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("amount")]
        public UInt64 Amount { get; set; }

        [JsonProperty("is_completed")]
        public bool IsCompleted { get; set; }

        [JsonProperty("child_criteria")]
        public IEnumerable<ChildCriteria> SubCriteria { get; set; }
    }
}
