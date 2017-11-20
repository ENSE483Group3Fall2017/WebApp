using AutoMapper;
using WebApp.DAL;

namespace WebApp.Features.Pet
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Pet.Create.Command, DAL.Pet>()
                .ForMember(dst => dst.BeaconID, opt => opt.MapFrom(src => src.Beacon.ToString()))
                .ForMember(dst => dst.Status, opt => opt.UseValue(PetStatus.Owned));

            CreateMap<DAL.Pet, Pet.Index.Model>();


            CreateMap<DAL.Pet, Pet.Details.Model>();
        }
    }
}
