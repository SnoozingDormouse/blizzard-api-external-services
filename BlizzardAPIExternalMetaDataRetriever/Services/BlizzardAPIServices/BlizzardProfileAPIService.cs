using System.Net.Http;

namespace BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices
{
    public class BlizzardProfileAPIService : BlizzardAPIService
    {
        private static readonly string queryURL = "https://eu.api.blizzard.com/{0}?namespace=profile-eu&locale=en_GB&access_token={1}";

        public BlizzardProfileAPIService(IHttpClientFactory clientFactory, string clientId, string clientSecret)
            : base(clientFactory, clientId, clientSecret, queryURL)
        {
        }
    }
}
