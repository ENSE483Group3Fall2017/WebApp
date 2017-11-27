using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApp.DAL;
using WebApp.Service;

namespace WebApp.Features.Pet
{
    public class Delete
    {
        public class Command : IRequest
        {
            public string BeaconId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            private readonly IDbContext _dbContext;

            public CommandValidator(IDbContext dbContext)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            }

            protected CommandValidator()
            {
                RuleFor(x => x.BeaconId)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotEmpty()
                    .CustomAsync(async (beaconId, context, cancellation) =>
                    {
                        var petExists = await _dbContext.Pets.AnyAsync(x => x.BeaconID == beaconId);
                        if (!petExists) context.AddFailure($"Pet with beacon id '{beaconId}' does not exist.");
                    });
            }
        }

        public class CommandHandler : IAsyncRequestHandler<Command>
        {
            private readonly IDbContext _dbContext;
            private readonly ILostPetsRegistry _registryService;

            public CommandHandler(IDbContext dbContext, ILostPetsRegistry registryService)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
                _registryService = registryService ?? throw new ArgumentNullException(nameof(dbContext));
            }

            public async Task Handle(Command message)
            {
                message = message ?? throw new ArgumentNullException(nameof(message));

                var pet = await _dbContext.Pets.FindAsync(message.BeaconId);
                _dbContext.Pets.Remove(pet);
                await _registryService.Remove(pet);
            }
        }
    }
}
