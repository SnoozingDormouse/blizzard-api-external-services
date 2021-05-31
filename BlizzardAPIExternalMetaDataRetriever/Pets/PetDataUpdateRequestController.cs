using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlizzardAPIExternalMetaDataRetriever.Pets
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetDataUpdateRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PetDataUpdateRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<string>> UpdateAllPets()
        {
            return Ok(await _mediator.Send(new PetDataUpdateRequest()));
        }
    }
}