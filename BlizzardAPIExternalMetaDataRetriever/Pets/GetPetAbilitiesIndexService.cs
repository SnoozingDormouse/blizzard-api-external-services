using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlizzardAPIExternalMetaDataRetriever.Pets.IncomingModels;
using BlizzardAPIExternalMetaDataRetriever.Services.BlizzardAPIServices;
using MediatR;
using Newtonsoft.Json;

namespace BlizzardAPIExternalMetaDataRetriever.Pets
{
    public class GetPetAbilitiesIndexService : IRequestHandler<GetPetAbilitiesIndexRequest, IEnumerable<int>>
    {
        private readonly IBlizzardAPIService _blizzardAPIService;
        private readonly string _indexApiPath;

        public GetPetAbilitiesIndexService(IBlizzardAPIService blizzardAPIService)
        {
            _indexApiPath = "data/wow/pet-ability/index";
            _blizzardAPIService = blizzardAPIService;
        }

        public async Task<IEnumerable<int>> Handle(GetPetAbilitiesIndexRequest request, CancellationToken cancellationToken)
        {
            var response = await _blizzardAPIService.GetBlizzardAPIResponseAsJsonAsync(_indexApiPath);
            var Ids = JsonConvert.DeserializeObject<PetAbilitiesIndex>(response).Abilities.Select(p => p.Id);

            return Ids;
        }
    }

    public class GetPetAbilitiesIndexRequest : IRequest<IEnumerable<int>>
    {

    }
} 
