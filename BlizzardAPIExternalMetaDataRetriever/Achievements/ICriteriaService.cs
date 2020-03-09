using System.Threading.Tasks;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements
{
    public interface ICriteriaService
    {
        Task<string> UpdateAll();
    }
}
