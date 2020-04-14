using System.Threading.Tasks;
using BlizzardAPIExternalMetaDataRetriever.Achievements;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using BlizzardData.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BlizzardAPIExternalMetaDataRetriever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private readonly IAchievementService _achievementService;
        private readonly IBlizzardAPIService _blizzardAPIService;

        public UpdateController(
            IAchievementService achievementService,
            IBlizzardAPIService blizzardAPIService)
        {
            _achievementService = achievementService;
            _blizzardAPIService = blizzardAPIService;
        }

        [HttpGet]
        [Route("Achievements")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string> UpdateAchievements()
        {
            // invoke pull from external services
            return await _achievementService.UpdateAll();
        }

        [HttpGet]
        [Route("Achievement/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string> UpdateAchievement(int id)
        {
            // invoke pull from external services
            return await _achievementService.Update(id);
        }

        [HttpGet]
        [Route("GetAccessToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<AccessToken> GetAccessToken()
        {
            return await _blizzardAPIService.GetValidAccessTokenFromBlizzard();
        }
    }
}