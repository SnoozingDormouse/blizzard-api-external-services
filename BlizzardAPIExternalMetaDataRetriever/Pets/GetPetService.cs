using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using MediatR;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.Pets
{
    public class GetPetService : IRequestHandler<GetPetRequest, Pet>
    {
        private readonly IBlizzardAPIService _blizzardAPIService;

        public GetPetService(IBlizzardAPIService blizzardAPIService)
        {
            _blizzardAPIService = blizzardAPIService;
        }

        public async Task<Pet> Handle(GetPetRequest request, CancellationToken cancellationToken)
        {
            var blizzardAPIPath = $"data/wow/pet/{request.Id}";
            var response = await _blizzardAPIService.GetBlizzardAPIResponseAsJsonAsync(blizzardAPIPath);

            var pet = JsonConvert.DeserializeObject<IncomingModels.Pet>(response);

            var abilities = pet.Abilities
                                .Select(a => new Pet.PetAbility
                                { 
                                    Id = a.Ability.Id,
                                    Name = a.Ability.Name,
                                    Slot = a.Slot,
                                    RequiredLevel = a.RequiredLevel
                                });

            return new Pet
            {
                Id = pet.Id,
                Name = pet.Name,
                TypeId = pet.BattlePetType.Id,
                TypeName = pet.BattlePetType.Name,
                Description = pet.Description,
                IsCapturable = pet.IsCapturable,
                IsTradable = pet.IsTradeable,
                IsBattlePet = pet.IsBattlePet,
                IsAllianceOnly = pet.IsAllianceOnly,
                IsHordeOnly = pet.IsHordeOnly,
                Source = pet.Source.Name,
                Abilities = abilities,
            };
        }
    }

    public class GetPetRequest : IRequest<Pet>
    {
        public int Id { get; set; }
    }
} 
