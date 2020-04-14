using BlizzardAPIExternalMetaDataRetriever.Shared.Models;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.CharacterData.Model.CharacterSummary
{
    public class ActiveTitle
    {
        [JsonProperty("key")]
        public HttpLink Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("display_string")]
        public string DisplayString { get; set; }
    }
}