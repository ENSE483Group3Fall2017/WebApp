using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Optional;
using System;
using System.Threading.Tasks;
using WebApp.DAL;

namespace WebApp.Features.Pet
{
    public class Details
    {
        public class Query : IRequest<Option<Model>>
        {
            public string Id { get; set; }
        }

        public class Model
        {
            public string BeaconId { get; set; }

            public string Name { get; set; }

            public string Kind { get; set; }

            public string Status { get; set; }
        }
            public string Description { get; set; }

        public class QueryHandler : IAsyncRequestHandler<Query, Option<Model>>
        {
            private readonly IDbContext _dbContext;
            private readonly IMapper _mapper;

            public QueryHandler(IDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<Option<Model>> Handle(Query message)
            {
                var pet = await _dbContext.Pets.AsNoTracking().FirstOrDefaultAsync(x => x.BeaconID == message.Id);
                if (pet == null) return Option.None<Model>();

                return _mapper.Map<DAL.Pet, Model>(pet).Some();
            }
        }
    }
}
