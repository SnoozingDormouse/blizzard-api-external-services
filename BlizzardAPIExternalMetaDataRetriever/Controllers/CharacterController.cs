using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlizzardAPIExternalMetaDataRetriever.Controllers
{
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        // GET: api/Character/{Region}/{Realm}/{Character}
        [HttpGet("{region}/{realm}/{character}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<string> UpdateCharacter(string region, string realm, string character)
        {
            throw new NotImplementedException();
            // invoke pull from external services
            //return await _characterService.Update(region, realm, character);
        }
    }
}
