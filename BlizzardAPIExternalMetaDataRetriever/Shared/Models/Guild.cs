using System;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.Shared.Models
{
    public class Guild
    {
        [JsonProperty("key")]
        public HttpLink Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public UInt64 Id { get; set; }

        [JsonProperty("realm")]
        public Realm Realm { get; set; }

        [JsonProperty("faction")]
        public TypeName Faction { get; set; }
    }
}
