namespace BlizzardAPIExternalMetaDataRetriever.Services.Authorization
{
    public class TokenRequest
    {
        public string clientid {get; set; }
        public string clientsecret {get; set; }
        public string grant_type {get; set; }
    }
}
