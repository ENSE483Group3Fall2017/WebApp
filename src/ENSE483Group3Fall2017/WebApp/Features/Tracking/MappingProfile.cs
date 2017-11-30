using AutoMapper;

namespace WebApp.Features.Tracking
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DAL.TrackingInfo, Index.Model>()
                .ForMember(dst => dst.AverageProximity, opt => opt.MapFrom(src => (src.MinProximityInFrame + src.MinProximityInFrame) / 2));
            
        }
    }
}
