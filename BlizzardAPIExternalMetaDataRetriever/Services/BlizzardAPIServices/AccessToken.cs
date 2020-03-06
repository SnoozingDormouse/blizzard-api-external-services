using System;
using BlizzardAPIExternalMetaDataRetriever.Services.Authorization;

namespace BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices
{
    public class AccessToken
    {
        public string Token { get; private set; }
        public DateTime? ExpiresAt { get; private set; }

        public AccessToken(TokenResponse token)
        {
            AssignValues(token.access_token, token.expires_in);
        }

        public AccessToken(string accessToken, Int64 expiresIn)
        {
            AssignValues(accessToken, expiresIn);
        }

        private void AssignValues(string access_token, long expires_in)
        {
            Token = access_token;
            ExpiresAt = DateTime.Now.AddSeconds(expires_in);
        }
    }
}
