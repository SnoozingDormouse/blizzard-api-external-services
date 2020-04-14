using BlizzardAPIExternalMetaDataRetriever.CharacterData.Model.CharacterSummary;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using BlizzardData.Data;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using entities = BlizzardData.Domain.Entities;

namespace BlizzardAPIExternalMetaDataRetriever.CharacterData.Services
{
    public class CharacterSummaryService
    {
        private readonly IDataContext _dataContext;
        private readonly IBlizzardAPIService _blizzardAPIService;
        private readonly string _characterSummaryPath;

        public CharacterSummaryService(
            IDataContext dataContext,
            IBlizzardAPIService blizzardAPIService)
        {
            _characterSummaryPath = "profile/wow/character/{0}/{1}";
            _dataContext = dataContext;
            _blizzardAPIService = blizzardAPIService;
        }

        internal async Task<entities::Character> GetAndPersistCharacter(string realm, string name)
        {
            var character = await GetCharacter(realm, name);
            return
                TransformAndStore(character);
        }

        internal async Task<CharacterProfileSummary> GetCharacter(string realm, string name)
        {
            var url = String.Format(_characterSummaryPath, realm, name);
            var response =
                await _blizzardAPIService
                    .GetBlizzardAPIResponseAsJsonAsync(url);

            return
                JsonConvert.DeserializeObject<CharacterProfileSummary>(response);
        }

        private entities::Character TransformAndStore(CharacterProfileSummary character)
        {
            var currentCharacter = _dataContext.Characters.Where(c => c.BlizzardId == character.BlizzardId).SingleOrDefault();
            var playerFaction = (entities::PlayerFaction)Enum.Parse(typeof(entities::PlayerFaction), character.Faction.Name);

            var entityCharacter = new entities::Character
            {
                Name = character.Name,
                Realm = character.Realm.Name,
                Faction = playerFaction,
                CharacterClass = character.CharacterClass.Name,
                Level = character.Level,
                BlizzardId = character.BlizzardId,
                LastUpdatedDateTime = DateTime.Now
            };

            if (currentCharacter == null)
            {
                _dataContext.Characters.Add(entityCharacter);
            }
            else
            {
                currentCharacter = entityCharacter;
            }

            _dataContext.SaveChanges();

            return entityCharacter;
        }
    }
}
