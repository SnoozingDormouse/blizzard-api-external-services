using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.Shared.Models
{
    public class TypeName
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}