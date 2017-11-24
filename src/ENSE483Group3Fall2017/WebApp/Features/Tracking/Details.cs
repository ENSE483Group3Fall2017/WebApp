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
    public class Details
    {
        public class Query : IRequest<IEnumerable<Model>>
        {
            public string BeaconId { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            protected QueryValidator()
            {
                RuleFor(x => x.BeaconId)
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

            public int MaxProximityInFrame { get; set; }

            public int MinProximityInFrame { get; set; }

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
                var records = await _dbContext.TrackingInfos
                                                .AsNoTracking()
                                                .OrderByDescending(x => x.FrameStartTime)
                                                .Take(50)
                                                .ToArrayAsync();

                return records.Select(x => _mapper.Map<DAL.TrackingInfo, Model>(x)).ToArray();
            }
        }
    }
}