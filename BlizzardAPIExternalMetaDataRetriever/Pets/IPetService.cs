using System.Threading.Tasks;

namespace BlizzardAPIExternalMetaDataRetriever.Pets
{
    public interface IPetService
    {
        Task<string> Update(int id);
        Task<string> UpdateAll();
    }
}
