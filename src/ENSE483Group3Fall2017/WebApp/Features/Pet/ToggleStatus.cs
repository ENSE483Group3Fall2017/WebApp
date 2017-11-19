using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;
using WebApp.DAL;

namespace WebApp.Features.Pet
{
    public class ToggleStatus
    {
        public class Command : IRequest
        {
            public string BeaconId { get; set; }
        }

        public class BeaconValidtor : AbstractValidator<Command>
        {
            private readonly IDbContext _dbContext;
            
            public BeaconValidtor(IDbContext dbContext)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
                RuleFor(x => x.BeaconId).NotEmpty();
            }
        }

        public class CommandHandler : IAsyncRequestHandler<Command>
        {
            private readonly IDbContext _dbContext;

            public CommandHandler(IDbContext dbContext)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            }

            public async Task Handle(Command message)
            {
                message = message ?? throw new ArgumentNullException(nameof(message));
                var pet = await _dbContext.Pets.FindAsync(message.BeaconId);
                pet.Status = ToogleStatus(pet.Status);
            }

            private DAL.PetStatus ToogleStatus(DAL.PetStatus currentStatus)
            {
                switch (currentStatus)
                {
                    case DAL.PetStatus.Owned:
                        return DAL.PetStatus.Lost;
                    case DAL.PetStatus.Lost:
                        return DAL.PetStatus.Owned;
                    default:
                        throw new InvalidOperationException($"Undefined pet status {currentStatus}.");
                }
            }
        }
    }
}
