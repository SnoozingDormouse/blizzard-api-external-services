using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BlizzardAPIExternalMetaDataRetriever.Services.Authorization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices
{
    public class BlizzardAPIService : IBlizzardAPIService
    {
        private readonly String _clientId;
        private readonly String _clientSecret;
        private readonly IHttpClientFactory _clientFactory;

        private AccessToken _accessToken;
        private readonly string _blizzardApiURL;
        private readonly string _tokenURL;

        public BlizzardAPIService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _clientId = configuration["Battlenet:ClientId"];
            _clientSecret = configuration["Battlenet:ClientSecret"];

            _blizzardApiURL = configuration["Battlenet:BlizzardApiURL"];
            _tokenURL = configuration["Battlenet:TokenURL"];
        }

        public BlizzardAPIService()
        {

        }

        private string GetNamespace(string blizzardQueryPath)
        {
            if (blizzardQueryPath.StartsWith("data", StringComparison.CurrentCultureIgnoreCase))
                return "static-eu";

            if (blizzardQueryPath.StartsWith("profile", StringComparison.CurrentCultureIgnoreCase))
                return "profile-eu";

            throw new NotSupportedException(String.Format("{0} is not supported", blizzardQueryPath));
        }

        public async Task<string> GetBlizzardAPIResponseAsJsonAsync(string blizzardQueryPath)
        {
            var nameSpace = GetNamespace(blizzardQueryPath);
            var queryURL = String.Format(_blizzardApiURL, blizzardQueryPath, nameSpace, "{0}");

            if (!ValidAccessToken())
            {
                _accessToken = await GetValidAccessTokenFromBlizzard();
            }

            var apiURL = String.Format(queryURL, _accessToken.Token);

            return await GetBlizzardAPIInfoAsync(apiURL);
        }

        internal async Task<string> GetBlizzardAPIInfoAsync(string apiURL)
        {
            String blizzardAPIResponse = null;

            try
            {
                using HttpClient httpClient = _clientFactory.CreateClient();
                HttpResponseMessage response = await httpClient.GetAsync(apiURL);
                if (response.IsSuccessStatusCode)
                {
                    blizzardAPIResponse = await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new HttpRequestException(response.ReasonPhrase);
                }
            }
            catch
            {
                throw;
            }

            return blizzardAPIResponse;
        }

        internal bool ValidAccessToken()
        {
            return _accessToken != null && _accessToken.Token != null && _accessToken.ExpiresAt != null && ((DateTime)_accessToken.ExpiresAt).AddMinutes(3) > DateTime.Now;
        }

        public async Task<AccessToken> GetValidAccessTokenFromBlizzard()
        {
            TokenResponse token;

            try
            {
                using HttpClient httpClient = _clientFactory.CreateClient();
                string tokenRequestParameters;
                var parameters = new Dictionary<string, string>()
                            {
                                { "client_id", _clientId },
                                { "client_secret", _clientSecret },
                                { "grant_type", "client_credentials" },
                            };

                tokenRequestParameters = new FormUrlEncodedContent(parameters).ReadAsStringAsync().Result;

                HttpContent content = new StringContent(tokenRequestParameters, Encoding.UTF8, "application/x-www-form-urlencoded");

                HttpResponseMessage response = await httpClient.PostAsync(_tokenURL, content);
                if (response.IsSuccessStatusCode)
                {
                    token = JsonConvert
                            .DeserializeObject<TokenResponse>(await response.Content.ReadAsStringAsync());

                    return new AccessToken(token);
                }
                else
                {
                    throw new HttpRequestException("The server could not be contacted");
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
