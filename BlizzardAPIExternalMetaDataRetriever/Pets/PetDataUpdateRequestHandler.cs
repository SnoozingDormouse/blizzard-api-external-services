using AutoMapper;
using BlizzardData.Data.Features.BattlePetFeatures;
using BlizzardData.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BlizzardAPIExternalMetaDataRetriever.Pets
{
    public class PetDataUpdateRequestHandler : IRequestHandler<PetDataUpdateRequest, string>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PetDataUpdateRequestHandler(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        public async Task<string> Handle(PetDataUpdateRequest request, CancellationToken cancellationToken)
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();

            var getPets = GetAllPets(cancellationToken);
            var getPetAbilities = GetAllPetAbilities(cancellationToken);

            var pets = await getPets;
            var petAbilities = await getPetAbilities;

            foreach (var pet in pets)
            {
                await _mediator.Send(new CreateBattlePetCommand { BattlePet = _mapper.Map<BattlePet>(pet) });
            }

            stopwatch.Stop();

            var results = 
                string.Format("{0} pets and {1} pet abilities downloaded in {2:hh\\:mm\\:ss}", 
                    pets.Count(),
                    petAbilities.Count(), 
                    stopwatch.Elapsed);

            return results;
        }

        private async Task<IEnumerable<Pet>> GetAllPets(CancellationToken cancellationToken)
        {
            List<int> petIds = (await _mediator.Send(new GetPetIndexRequest(), cancellationToken)).ToList();

            using var semaphore = new SemaphoreSlim(80);
            Task<Pet>[] getPets = petIds.Select(async petId =>
            {
                await semaphore.WaitAsync(1000, cancellationToken);
                try
                {
                    return await _mediator.Send(new GetPetRequest { Id = petId }, cancellationToken);
                }
                finally
                {
                    semaphore.Release();
                }
            }).ToArray();

            return await Task.WhenAll(getPets);
        }

        private async Task<IEnumerable<PetAbility>> GetAllPetAbilities(CancellationToken cancellationToken)
        {

            List<int> petAbilityIds = (await _mediator.Send(new GetPetAbilitiesIndexRequest(), cancellationToken)).ToList();

            using var semaphore = new SemaphoreSlim(80);
            Task<PetAbility>[] getPetAbilities = petAbilityIds.Select(async petAbilityId =>
            {
                await semaphore.WaitAsync(1000, cancellationToken);
                try
                {
                    return await _mediator.Send(new GetPetAbilityRequest { Id = petAbilityId }, cancellationToken);
                }
                finally
                {
                    semaphore.Release();
                }
            }).ToArray();

            return await Task.WhenAll(getPetAbilities);
        }
    }
}
