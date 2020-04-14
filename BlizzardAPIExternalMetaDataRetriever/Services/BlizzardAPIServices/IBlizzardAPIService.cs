using System.Threading.Tasks;

namespace BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices
{
    public interface IBlizzardAPIService
    {
        Task<string> GetBlizzardAPIResponseAsJsonAsync(string apiPath);
        Task<AccessToken> GetValidAccessTokenFromBlizzard();
    }
}
