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
        private readonly string _referenceCharacter;
        private readonly string _referenceRealm;

        private readonly String _clientId;
        private readonly String _clientSecret;
        private readonly IHttpClientFactory _clientFactory;

        private AccessToken _accessToken;
        private string _gameDataQueryURL;
        private string _profileQueryURL;
        private string _tokenURL;

        public BlizzardAPIService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _clientId = configuration["Battlenet:ClientId"];
            _clientSecret = configuration["Battlenet:ClientSecret"];
            _referenceRealm = configuration["Battlenet:ReferenceRealm"];
            _referenceCharacter = configuration["Battlenet:ReferenceCharacter"];

            _gameDataQueryURL = configuration["Battlenet:GameDataQueryURL"];
            _profileQueryURL = configuration["Battlenet:ProfileQueryURL"];
            _tokenURL = configuration["Battlenet:TokenURL"];
        }

        public string GetBlizzardGameDataAPIResponseAsJson(string apiPath)
        {
            var url = String.Format(_gameDataQueryURL, apiPath, "{1}");
            return GetBlizzardAPIResponseAsJson(url);
        }

        public string GetBlizzardDefaultProfileAPIResponseAsJson(string apiPath)
        {
            return GetBlizzardProfileAPIResponseAsJson(apiPath, _referenceRealm, _referenceCharacter);
        }

        public string GetBlizzardProfileAPIResponseAsJson(string apiPath, string realm, string character)
        {
            var url = String.Format(_profileQueryURL, realm, character, apiPath, "{1}");
            return GetBlizzardAPIResponseAsJson(url);
        }

        private string GetBlizzardAPIResponseAsJson(string queryURL)
        {
            if (!ValidAccessToken())
            {
                _accessToken = GetValidAccessTokenFromBlizzard().Result;
            }

            var apiURL = String.Format(queryURL, _accessToken.Token);

            return GetBlizzardAPIInfoAsync(apiURL).Result;
        }

        async Task<string> GetBlizzardAPIInfoAsync(string apiURL)
        {
            String blizzardAPIResponse = null;

            try
            {
                using (HttpClient httpClient = _clientFactory.CreateClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiURL);
                    if (response.IsSuccessStatusCode)
                    {
                        blizzardAPIResponse = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        throw new HttpRequestException("The server could not be contacted");
                    }
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

        private async Task<AccessToken> GetValidAccessTokenFromBlizzard()
        {
            TokenResponse token = new TokenResponse();

            try
            {
                using (HttpClient httpClient = _clientFactory.CreateClient())
                {
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
            }
            catch
            {
                throw;
            }
        }
    }
}
