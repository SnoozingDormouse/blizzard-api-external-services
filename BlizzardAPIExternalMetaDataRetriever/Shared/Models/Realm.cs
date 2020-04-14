using System;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.Shared.Models
{
    public class Realm
    {
        [JsonProperty("key")]
        public HttpLink Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public UInt64 Id { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }
    }
}
