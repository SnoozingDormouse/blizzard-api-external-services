using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlizzardAPIExternalMetaDataRetriever.Achievements;
using BlizzardData.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlizzardAPIExternalMetaDataRetriever.Controllers
{
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly IAchievementService _achievementService;
        private readonly RefreshCharacterCriteria _refreshCriteria;

        public CharacterController(
            AchievementContext achievementContext,
            IAchievementService achievementService,
            ICriteriaService criteriaService)
        {
            _achievementService = achievementService;
            _refreshCriteria = new RefreshCharacterCriteria(achievementContext, criteriaService);
        }

        // GET: api/Character/{Region}/{Realm}/{Character}
        [HttpGet("{realm}/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string> UpdateCharacter(string realm, string name)
        {
            return await _refreshCriteria.UpdateCharacter(realm, name);

        }
    }
}
