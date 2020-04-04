using System.Collections.Generic;
using System.Threading.Tasks;
using entities = BlizzardData.Domain.Entities;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements
{
    public interface ICriteriaService
    {
        Task<IEnumerable<entities::CharacterCriteria>> GetCharacterCriteriaFromBlizzardAPIAsync(string realm, string name);
        Task<IEnumerable<entities::Criteria>> GetCriteriaFromBlizzardAPIAsync();
    }
}
