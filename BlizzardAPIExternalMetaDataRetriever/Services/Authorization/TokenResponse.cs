using System;

namespace BlizzardAPIExternalMetaDataRetriever.Services.Authorization
{
    public class TokenResponse
    {
        public string access_token {get; set; }
        public string token_type { get; set; }
        public Int64 expires_in { get; set; }
        public string scope { get; set; }
    }
}
