using BlizzardAPIExternalMetaDataRetriever.Achievements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlizzardAPIExternalMetaDataRetriever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private IAchievementService _achievementService;
        private ICriteriaService _criteriaService;

        public UpdateController(
            IAchievementService achievementService,
            ICriteriaService criteriaService)
        {
            _achievementService = achievementService;
            _criteriaService = criteriaService;
        }

        [HttpGet]
        [Route("Achievements")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public string UpdateAchievements()
        {
            // invoke pull from external services
            return _achievementService.UpdateAll();
        }

        [HttpGet]
        [Route("Criteria")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public string UpdateCriteria()
        {
            // invoke pull from external services
            return _criteriaService.UpdateAll();
        }
    }
}