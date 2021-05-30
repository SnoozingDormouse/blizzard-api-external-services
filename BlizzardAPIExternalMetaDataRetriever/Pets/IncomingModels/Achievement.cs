using BlizzardAPIExternalMetaDataRetriever.Shared.Models;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels
{
    public class Achievement
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("category")]
        public KeyNameId Category { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }

        [JsonProperty("is_account_wide")]
        public bool IsAccountWide { get; set; }

        [JsonProperty("criteria")]
        public Criteria Criteria { get; set; }

        [JsonProperty("reward_description")]
        public string RewardDescription { get; set; }

        [JsonProperty("media")]
        public Media Media { get; set; }

        [JsonProperty("display_order")]
        public int DisplayOrder { get; set; }
    }
}
