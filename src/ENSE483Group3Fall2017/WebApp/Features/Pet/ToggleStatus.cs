using FluentValidation;
using MediatR;
using System;
using System.Threading.Tasks;
using WebApp.DAL;
using WebApp.Service;

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
            private readonly ILostPetsRegistry _registryService;

            public CommandHandler(IDbContext dbContext, ILostPetsRegistry registryService)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
                _registryService = registryService ?? throw new ArgumentNullException(nameof(registryService));
            }

            public async Task Handle(Command message)
            {
                message = message ?? throw new ArgumentNullException(nameof(message));
                var pet = await _dbContext.Pets.FindAsync(message.BeaconId);
                await ToogleStatus(pet);
            }

            private Task ToogleStatus(DAL.Pet pet)
            {
                var currentStatus = pet.Status;

                switch (currentStatus)
                {
                    case DAL.PetStatus.Owned:
                        pet.Status = DAL.PetStatus.Lost;
                        return _registryService.Add(pet);
                    case DAL.PetStatus.Lost:
                        pet.Status = DAL.PetStatus.Owned;
                        return _registryService.Remove(pet);
                    default:
                        throw new InvalidOperationException($"Undefined pet status {currentStatus}.");
                }
            }
        }
    }
}
