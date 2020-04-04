using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using BlizzardData.Data;
using BlizzardData.Domain.Entities;
using Newtonsoft.Json;
using entities = BlizzardData.Domain.Entities;
using incoming = BlizzardAPIExternalMetaDataRetriever.Achievements.IncomingModels;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements
{
    public class CriteriaService : ICriteriaService
    {
        private readonly IBlizzardAPIService _blizzardAPIService;
        private readonly string _pathURL;

        public CriteriaService(
            IAchievementContext achievementContext,
            IBlizzardAPIService blizzardAPIService)
        {
            _blizzardAPIService = blizzardAPIService;
            _pathURL = "achievements";
        }

        public async Task<IEnumerable<CharacterCriteria>> GetCharacterCriteriaFromBlizzardAPIAsync(string realm, string name)
        {

            var response = await _blizzardAPIService.GetBlizzardProfileAPIResponseAsJsonAsync(_pathURL, realm, name);

            var characterAchievements = JsonConvert.DeserializeObject<AchievementWrapper>(response);
            int characterId = characterAchievements.character.id;

            var criteria = FlattenCriteria(characterAchievements.achievements);
            return criteria.Select(c =>
                new CharacterCriteria
                {
                    CharacterId = characterId,
                    CriteriaId = c.id,
                    Amount = c.amount,
                    IsComplete = c.is_completed
                });
        }

        public async Task<IEnumerable<entities.Criteria>> GetCriteriaFromBlizzardAPIAsync()
        {
            var achievements = await RetrieveDefaultProfileAchievementsWithCriteriaAsync();
            var criteria = FlattenCriteria(achievements);
            return criteria.Select(c => (entities::Criteria)c);
        }

        private async Task<IEnumerable<incoming::Achievement>> RetrieveDefaultProfileAchievementsWithCriteriaAsync()
        {
            var response = await _blizzardAPIService.GetBlizzardDefaultProfileAPIResponseAsJsonAsync(_pathURL);
            
            return
                JsonConvert.DeserializeObject<AchievementWrapper>(response)
                .achievements;
        }

        private List<FlattenedCriteria> FlattenCriteria(IEnumerable<incoming.Achievement> achievementsWithCriteria)
        {
            _criteriaList = new List<FlattenedCriteria>();

            foreach (var achievement in achievementsWithCriteria)
            {
                var achievementId = achievement.id;
                int? parentId = null;

                if (achievement.criteria != null)
                    AddToCriteriaList(achievementId, achievement.criteria, parentId);
            }

            return _criteriaList;
        }

        List<FlattenedCriteria> _criteriaList;

        private void AddToCriteriaList(int achievementId, incoming.Criteria criteria, int? parentId)
        {
            _criteriaList.Add(new incoming::FlattenedCriteria
            {
                achievementId = achievementId,
                parentId = parentId,
                id = criteria.id,
                amount = criteria.amount,
                is_completed = criteria.is_completed
            });

            if (criteria.child_criteria != null)
            {
                foreach (var childCriteria in criteria.child_criteria)
                {
                    AddToCriteriaList(achievementId, childCriteria, criteria.id);
                }
            }
        }
    }
} 
