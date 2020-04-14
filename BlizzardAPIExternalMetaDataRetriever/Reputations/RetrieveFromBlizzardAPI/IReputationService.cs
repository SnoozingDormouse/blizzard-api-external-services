using System.Threading.Tasks;

namespace BlizzardAPIExternalMetaDataRetriever.Reputations.RetrieveFromBlizzardAPI
{
    public interface IReputationService
    {
        Task<string> Update(int id);
        Task<string> UpdateAll();
    }
}
