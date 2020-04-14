using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.Shared.Models
{
    public class KeyNameId
    {
        [JsonProperty("key")]
        public HttpLink Key { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
