using Newtonsoft.Json;
using System.Collections.Generic;

namespace BlizzardAPIExternalMetaDataRetriever.CharacterData.Model.AchievementSummary
{
    public class Criteria
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("is_completed")]
        public bool IsCompleted { get; set; }

        [JsonProperty("child_criteria")]
        public IEnumerable<ChildCriteria> ChildCriteria { get; set; }
    }
}
