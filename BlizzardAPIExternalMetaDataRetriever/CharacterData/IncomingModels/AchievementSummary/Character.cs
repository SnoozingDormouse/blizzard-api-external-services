using System;
using BlizzardAPIExternalMetaDataRetriever.Shared.Models;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.CharacterData.Model.AchievementSummary
{
    public class Character
    {
        [JsonProperty("key")]
        public HttpLink Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public UInt64 Id { get; set; }

        [JsonProperty("realm")]
        public Realm Realm { get; set; }
    }
}
