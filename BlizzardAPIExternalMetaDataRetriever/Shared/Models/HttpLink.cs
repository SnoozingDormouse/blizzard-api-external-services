using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.Shared.Models
{
    public class HttpLink
    {
        [JsonProperty("href")]
        public string HRef { get; set; }
    }
}