using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DAL;

namespace WebApp.Features.Pet
{
    public class Index
    {
        public class Query : IRequest<IEnumerable<Model>>
        {
        }

        public class Model
        {
            public string BeaconId { get; set; }

            public string Name { get; set; }

            public string Kind { get; set; }

            public string Status { get; set; }
        }

        public class QueryHandler : IAsyncRequestHandler<Query, IEnumerable<Model>>
        {
            private readonly IDbContext _dbContext;
            private readonly IMapper _mapper;

            public QueryHandler(IDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<IEnumerable<Model>> Handle(Query message)
            {
                var pets = await _dbContext.Pets
                                            .AsNoTracking()
                                            .ToArrayAsync();
                if (!pets.Any())
                    return Enumerable.Empty<Model>();

                return pets.Select(x => _mapper.Map<DAL.Pet, Model>(x)).ToArray();
            }
        }
    }
}
