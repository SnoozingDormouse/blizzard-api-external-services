using BlizzardAPIExternalMetaDataRetriever.CharacterData.Model.AchievementSummary;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using BlizzardData.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using entities = BlizzardData.Domain.Entities;

namespace BlizzardAPIExternalMetaDataRetriever.CharacterData.Services
{
    public class CharacterAchievementSummaryService
    {
        private readonly IDataContext _dataContext;
        private readonly IBlizzardAPIService _blizzardAPIService;
        private readonly string _characterAchievementSummaryPath;

        public CharacterAchievementSummaryService(
            IDataContext dataContext,
            IBlizzardAPIService blizzardAPIService)
        {
            _characterAchievementSummaryPath = "profile/wow/character/{0}/{1}/achievements";
            _dataContext = dataContext;
            _blizzardAPIService = blizzardAPIService;
        }

        internal async Task GetAndPersistCharacterAchievements(string realm, string name)
        {
            var characterAchievements = await GetCharacterAchievements(realm, name);
            TransformAndStore(characterAchievements);
        }

        internal async Task<CharacterAchievementsSummary> GetCharacterAchievements(string realm, string name)
        {
            var url = String.Format(_characterAchievementSummaryPath, realm.ToLower(), name.ToLower());
            var response =
                await _blizzardAPIService
                    .GetBlizzardAPIResponseAsJsonAsync(url);

            return
                JsonConvert.DeserializeObject<CharacterAchievementsSummary>(response);
        }

        private void TransformAndStore(CharacterAchievementsSummary characterAchievements)
        {
            TransformAndStoreAchievements(characterAchievements);
            TransformAndStoreCriteria(characterAchievements);
        }

        private void TransformAndStoreAchievements(CharacterAchievementsSummary characterAchievements)
        {
            var character = characterAchievements.Character;

            var characterAchievementsToAddOrUpdate =
                characterAchievements
                .Achievements
                .Select(c =>
                    new entities::CharacterAchievement
                    {
                        CharacterId = character.Id,
                        AchievementId = c.Achievement.Id,
                        CompletedTimestamp = c.TimeStamp
                    });

            var entities = _dataContext
                    .CharacterAchievements
                    .Where(c => c.CharacterId == character.Id);

            _dataContext.CharacterAchievements.RemoveRange(entities);
            _dataContext.CharacterAchievements.AddRange(characterAchievementsToAddOrUpdate);

            _dataContext.SaveChanges();
        }

        private void TransformAndStoreCriteria(CharacterAchievementsSummary characterAchievements)
        {
            var character = characterAchievements.Character;

            var entities = _dataContext
                    .CharacterCriterias
                    .Where(c => c.CharacterId == character.Id);

            _dataContext.CharacterCriterias.RemoveRange(entities);

            var criteriaToAdd = new List<entities::CharacterCriteria>();

            var criteria =
                characterAchievements
                .Achievements
                .Where(a => a.Criteria != null)
                .Select(c => c.Criteria);

            _dataContext.CharacterCriterias.AddRange(
                    criteria.Select(c =>
                        new entities::CharacterCriteria
                        {
                            CharacterId = character.Id,
                            CriteriaId = c.Id,
                            IsCompleted = c.IsCompleted
                        }));

            var childCriteria =
                criteria
                .Where(cr => cr.ChildCriteria != null)
                .SelectMany(c => c.ChildCriteria);

            _dataContext.CharacterCriterias.AddRange(
                    childCriteria.Select(c =>
                        new entities::CharacterCriteria
                        {
                            CharacterId = character.Id,
                            CriteriaId = c.Id,
                            Amount = c.Amount,
                            IsCompleted = c.IsCompleted
                        }));

            var childOfChildrenCriteria =
                 childCriteria
                 .Where(cr => cr.SubCriteria != null)
                 .SelectMany(c => c.SubCriteria);

            _dataContext.CharacterCriterias.AddRange(
                    childOfChildrenCriteria.Select(c => 
                        new entities::CharacterCriteria
                        {
                            CharacterId = character.Id,
                            CriteriaId = c.Id,
                            Amount = c.Amount,
                            IsCompleted = c.IsCompleted
                        }));

            _dataContext.SaveChanges();
        }
    }
}
