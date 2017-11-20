using AutoMapper;
using Ploeh.AutoFixture;
using SemanticComparison.Fluent;
using WebApp.Features.Pet;
using Xunit;
using MappingProfile = WebApp.Features.Pet.MappingProfile;

namespace WebApp.Tests.Features.Pet.MapperTests
{
    class Customization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            var mapperCfg = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            var mapper = mapperCfg.CreateMapper();
            fixture.Inject(mapper);
        }
    }

    public class WhenMappingACreateCommandToAPetInstance
    {
        [Theory, AutoFakeItEasyData(typeof(Customization))]
        public void ItShouldMapTheExpectedValues(Create.Command command, IMapper sut)
        {
            // Arrange
            var likeness = command.AsSource()
                                    .OfLikeness<DAL.Pet>()
                                    .With(x => x.BeaconID).EqualsWhen((s, d) => s.Beacon.ToString() == d.BeaconID)
                                    .With(x => x.Status).EqualsWhen((s, d) => d.Status == DAL.PetStatus.Owned)
                                    .With(x => x.Kind).EqualsWhen((s, d) => (int)s.Kind == (int)d.Kind);

            // Act
            var result = sut.Map<Create.Command, DAL.Pet>(command);

            // Assert
            likeness.ShouldEqual(result);
        }
    }

    public class WhenMappingAPetInstanceToIndexModel
    {
        [Theory, AutoFakeItEasyData(typeof(Customization))]
        public void ItShhouldMapTheExpectedValues(DAL.Pet pet, IMapper sut)
        {
            // Arrange
            var likeness = pet.AsSource()
                                .OfLikeness<Index.Model>()
                                .With(x => x.Kind).EqualsWhen((s, d) => s.Kind.ToString() == d.Kind)
                                .With(x => x.Status).EqualsWhen((s, d) => s.Status.ToString() == d.Status);


            // Act
            var result = sut.Map<DAL.Pet, Index.Model>(pet);

            // Assert
            likeness.ShouldEqual(result); 
        }
    }

    public class WhenMappinggAPetInstanceToADetailModel
    {
        [Theory, AutoFakeItEasyData(typeof(Customization))]
        public void ItShouldMapTheExpectedValues(DAL.Pet pet, IMapper sut)
        {
            // Arrange
            var likeness = pet.AsSource()
                                .OfLikeness<Details.Model>()
                                .With(x => x.Kind).EqualsWhen((s, d) => s.Kind.ToString() == d.Kind)
                                .With(x => x.Status).EqualsWhen((s, d) => s.Status.ToString() == d.Status);

            // Act
            var result = sut.Map<DAL.Pet, Details.Model>(pet);

            // Assert
            likeness.ShouldEqual(result);
        }
    }
}