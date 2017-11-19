using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using WebApp.DAL;

namespace WebApp.Features.Pet
{
    public class Create
    {
        public class Command : IRequest
        {
            [Required]
            public Beacon Beacon { get; set; }

            [Required(AllowEmptyStrings = false)]
            [StringLength(maximumLength: 50)]
            public string PetName { get; set; }

            [Required]
            public PetKind PetKind { get; set; }

            [StringLength(maximumLength: 150)]
            public string PetDesciption { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            private readonly IDbContext _dbContext;

            public CommandValidator(IDbContext dbContext)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            }

            public CommandValidator()
            {
                RuleFor(x => x.Beacon).Cascade(CascadeMode.StopOnFirstFailure)
                    .NotNull()
                    .CustomAsync(async (beaconID, context, cancellation) =>
                    {
                        var id = beaconID.ToString();
                        var petExists = await _dbContext.Pets.AnyAsync(x => x.BeaconID == id);
                        if (petExists) context.AddFailure($"Beacon id {beaconID} is already in use.");
                    });
                RuleFor(x => x.PetKind).NotEqual(default(PetKind));
                RuleFor(x => x.PetName).NotEmpty().MaximumLength(50);
                RuleFor(x => x.PetDesciption).MaximumLength(150);
            }
        }

        public class CommandHandler : IAsyncRequestHandler<Command>
        {
            private readonly IDbContext _dbContext;
            private readonly IMapper _mapper;

            public CommandHandler(IDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public Task Handle(Command message)
            {
                message = message ?? throw new ArgumentNullException(nameof(message));
                var pet = _mapper.Map<Command, DAL.Pet>(message);
                return _dbContext.Pets.AddAsync(pet);
            }
        }
    }
}