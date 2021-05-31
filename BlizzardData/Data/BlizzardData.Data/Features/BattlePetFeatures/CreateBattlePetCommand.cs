using BlizzardData.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BlizzardData.Data.Features.BattlePetFeatures
{
    public class CreateBattlePetCommand : IRequest<int>
    {
        public BattlePet BattlePet{ get; set; }
    }

    public class CreateBattlePetCommandHandler : IRequestHandler<CreateBattlePetCommand, int>
    {
        private readonly IDataContext _context;

        public CreateBattlePetCommandHandler(IDataContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateBattlePetCommand command, CancellationToken cancellationToken)
        {
            var battlePet = command.BattlePet with { };
            
            _context.BattlePets.Add(battlePet);

            await _context.SaveChangesAsync();

            return battlePet.Id;
        }
    }
}
