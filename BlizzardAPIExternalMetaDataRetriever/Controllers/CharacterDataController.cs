using System;
using System.Threading.Tasks;
using BlizzardAPIExternalMetaDataRetriever.CharacterData.Services;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using BlizzardData.Data;
using Microsoft.AspNetCore.Mvc;

namespace BlizzardAPIExternalMetaDataRetriever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterDataController : ControllerBase
    {
        private readonly CharacterSummaryService _characterSummaryService;
        private readonly CharacterAchievementSummaryService _characterAchievementSummaryService;

        public CharacterDataController(
            DataContext dataContext,
            IBlizzardAPIService blizzardAPIService)
        {
            _characterSummaryService = new CharacterSummaryService(dataContext, blizzardAPIService);
            _characterAchievementSummaryService = new CharacterAchievementSummaryService(dataContext, blizzardAPIService);
        }

        [HttpGet]
        [Route("CharacterSummary/{realm}/{name}")]
        public async Task<IActionResult> GetCharacterSummary(string realm, string name)
        {
            return Ok(
                await _characterSummaryService.GetCharacter(realm, name));
        }

        [HttpGet]
        [Route("Update/{realm}/{name}")]
        public async Task<IActionResult> UpdateCharacterData(string realm, string name)
        {
            try
            {
                var character =
                    await _characterSummaryService.GetAndPersistCharacter(realm, name);

                await _characterAchievementSummaryService.GetAndPersistCharacterAchievements(realm, name);

                return Ok(character);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
