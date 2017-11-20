using AutoMapper;

namespace WebApp.Features.Tracking
{
    public class MappingProfile : Profile
    {
        protected MappingProfile()
        {
            CreateMap<DAL.TrackingInfo, Index.Model>();
            CreateMap<DAL.TrackingInfo, Details.Model>();
        }
    }
}
