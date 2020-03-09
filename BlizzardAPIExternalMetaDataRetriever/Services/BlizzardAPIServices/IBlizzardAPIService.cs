using System.Threading.Tasks;

namespace BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices
{
    public interface IBlizzardAPIService
    {
        Task<string> GetBlizzardGameDataAPIResponseAsJsonAsync(string apiPath);
        Task<string> GetBlizzardDefaultProfileAPIResponseAsJsonAsync(string apiPath);
        Task<string> GetBlizzardProfileAPIResponseAsJsonAsync(string apiPath, string realm, string character);
    }
}
