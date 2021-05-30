using BlizzardAPIExternalMetaDataRetriever.Shared.Models;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels
{
    public class Media
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("key")]
        public HttpLink Key { get; set; }
    }
}