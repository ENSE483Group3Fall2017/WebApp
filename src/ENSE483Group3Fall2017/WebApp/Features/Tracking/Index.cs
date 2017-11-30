using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.DAL;

namespace WebApp.Features.Tracking
{
    public class Index
    {
        public class Query : IRequest<IEnumerable<Model>>
        {
            public string Id { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Id)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .NotNull()
                    .NotEmpty();
            }
        }

        public class Model
        {
            public Guid Id { get; set; }

            public DateTime FrameStartTime { get; set; }

            public DateTime FrameEndTime { get; set; }

            public int AverageProximity { get; set; }

            public string GeoReversedAddress { get; set; }
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
                message = message ?? throw new ArgumentNullException(nameof(message));

                var records = await _dbContext.TrackingInfos
                                                .AsNoTracking()
                                                .Where(x => x.BeaconID == message.Id)
                                                .OrderByDescending(x => x.FrameStartTime)
                                                .Take(50)
                                                .ToArrayAsync();

                return records.Select(x => _mapper.Map<DAL.TrackingInfo, Model>(x)).ToArray();
            }
        }
    }
}