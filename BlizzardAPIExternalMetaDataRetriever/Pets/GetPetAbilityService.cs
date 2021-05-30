using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using MediatR;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.Pets
{
    public class GetPetAbilityService : IRequestHandler<GetPetAbilityRequest, PetAbility>
    {
        private readonly IBlizzardAPIService _blizzardAPIService;

        public GetPetAbilityService(IBlizzardAPIService blizzardAPIService)
        {
            _blizzardAPIService = blizzardAPIService;
        }

        public async Task<PetAbility> Handle(GetPetAbilityRequest request, CancellationToken cancellationToken)
        {
            var blizzardAPIPath = $"data/wow/pet-ability/{request.Id}";
            var response = await _blizzardAPIService.GetBlizzardAPIResponseAsJsonAsync(blizzardAPIPath);

            var petAbility = JsonConvert.DeserializeObject<IncomingModels.PetAbility>(response);

            return new PetAbility
            {
                Id = petAbility.Id,
                Name = petAbility.Name,
                PetAbilityType = petAbility.BattlePetType.Name,
                PetAbilityTypeId = petAbility.BattlePetType.Id,
                Rounds = petAbility.Rounds,
            };
        }
    }

    public class GetPetAbilityRequest : IRequest<PetAbility>
    {
        public int Id { get; set; }
    }
} 
