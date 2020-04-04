using BlizzardAPIExternalMetaDataRetriever.Achievements;
using BlizzardData.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlizzardAPIExternalMetaDataRetriever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private readonly IAchievementService _achievementService;
        private readonly RefreshGameDataCriteria _refreshCriteria;

        public UpdateController(
            AchievementContext achievementContext,
            IAchievementService achievementService,
            ICriteriaService criteriaService)
        {
            _achievementService = achievementService;
            _refreshCriteria = new RefreshGameDataCriteria(achievementContext, criteriaService);
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
        [Route("Criteria")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string> UpdateCriteria()
        {
            // invoke pull from external services
            return await _refreshCriteria.UpdateAll();
        }
    }
}