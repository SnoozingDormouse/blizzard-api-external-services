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
    public class GetPetIndexService : IRequestHandler<GetPetIndexRequest, IEnumerable<int>>
    {
        private readonly IBlizzardAPIService _blizzardAPIService;
        private readonly string _indexApiPath;

        public GetPetIndexService(IBlizzardAPIService blizzardAPIService)
        {
            _indexApiPath = "data/wow/pet/index";
            _blizzardAPIService = blizzardAPIService;
        }

        public async Task<IEnumerable<int>> Handle(GetPetIndexRequest request, CancellationToken cancellationToken)
        {
            var response = await _blizzardAPIService.GetBlizzardAPIResponseAsJsonAsync(_indexApiPath);
            var petIds = JsonConvert.DeserializeObject<PetIndex>(response).Pets.Select(p => p.Id);

            return petIds;
        }
    }

    public class GetPetIndexRequest : IRequest<IEnumerable<int>>
    {

    }
} 
