using BlizzardAPIExternalMetaDataRetriever.Achievements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlizzardAPIExternalMetaDataRetriever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private IAchievementService _achievementService;

        public AchievementController(IAchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public string UpdateAchievements()
        {
            // invoke pull from external services
            return _achievementService.UpdateAllAchievements();
        }
    }
}