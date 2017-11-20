using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Optional;
using System;
using System.Threading.Tasks;
using WebApp.DAL;

namespace WebApp.Features.Tracking
{
    public class Index
    {
        public class Query : IRequest<Option<Model>>
        {
            public Guid TrackingId { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.TrackingId).NotEqual(default(Guid));
            }
        }

        public class Model
        {
            public DateTime FrameStartTime { get; set; }

            public DateTime FrameEndTime { get; set; }

            public int MaxProximityInFrame { get; set; }

            public int MinProximityInFrame { get; set; }

            public string GeoReversedAddress { get; set; }

            public string GpsCoordinates { get; set; }
        }

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
                var record = await _dbContext.TrackingInfos
                                                .AsNoTracking()
                                                .FirstOrDefaultAsync(x => x.ID == message.TrackingId);

                if (record == null)
                    return Option.None<Model>();

                return _mapper.Map<DAL.TrackingInfo, Model>(record).Some();
            }
        }

    }
}