using System.Threading.Tasks;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements
{
    public interface IAchievementService
    {
        Task<string> UpdateAll();
    }
}
