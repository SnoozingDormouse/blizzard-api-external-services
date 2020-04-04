using BlizzardData.Data;
using BlizzardData.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlizzardAPIExternalMetaDataRetriever.Achievements
{
    public class RefreshCharacterCriteria
    {
        private readonly IAchievementContext _achievementContext;
        private readonly ICriteriaService _criteriaService;

        public RefreshCharacterCriteria(
            IAchievementContext achievementContext,
            ICriteriaService criteriaService)
        {
            _achievementContext = achievementContext;
            _criteriaService = criteriaService;
        }

        public async Task<string> UpdateCharacter(string realm, string name)
        {
            try
            {
                int saved = await RetrieveAndPersistCharacterCriteria(realm, name);

                return saved.ToString() + string.Format(" criteria saved to database for {0}-{1}", realm, name);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString() + "\r\n" +
                    ex.InnerException.ToString() + "\r\n" +
                    ex.StackTrace.ToString();
            }
        }

        private async Task<int> RetrieveAndPersistCharacterCriteria(string realm, string name)
        {
            IEnumerable<CharacterCriteria> criteria = await _criteriaService.GetCharacterCriteriaFromBlizzardAPIAsync(realm, name);
            var blizzardCharacterId = criteria.Select(c => c.CharacterId).FirstOrDefault();

            AddOrUpdateCharacterAccessInfo(realm, name, blizzardCharacterId);
            int saved = SaveCriteria(criteria);
            return saved;
        }

        private void AddOrUpdateCharacterAccessInfo(string realm, string name, int blizzardCharacterId)
        {
            var character = new Character
            {
                BlizzardId = blizzardCharacterId,
                Realm = realm,
                Name = name,
                LastUpdatedDateTime = DateTime.Now
            };

            var entity = _achievementContext.Characters.Where(c => c.BlizzardId == blizzardCharacterId).FirstOrDefault();

            if (entity == null)
            {
                _achievementContext.Characters.Add(character);
            }
            else
            {
                entity.Realm = character.Realm;
                entity.Name = character.Name;
                entity.LastUpdatedDateTime = character.LastUpdatedDateTime;
            }
        }

        private int SaveCriteria(IEnumerable<CharacterCriteria> criteria)
        {
            foreach (var c in criteria)
            {
                var entity = _achievementContext.CharacterCriterias.Where(cc => cc.CharacterId == c.CharacterId && cc.CriteriaId == c.CriteriaId).FirstOrDefault();
                if (entity == null)
                {
                    _achievementContext.CharacterCriterias.Add(c);
                }
                else
                {
                    entity.Amount = c.Amount;
                    entity.IsComplete = c.IsComplete;
                }
            }

            return _achievementContext.SaveChanges();
        }
    }
}
