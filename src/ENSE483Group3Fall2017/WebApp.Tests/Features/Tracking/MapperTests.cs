using AutoMapper;
using Ploeh.AutoFixture;
using SemanticComparison.Fluent;
using WebApp.Features.Tracking;
using Xunit;

namespace WebApp.Tests.Features.Tracking.MapperTests
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

    public class WhenMappingFromATrackingInfoToAnIndexModel
    {
        [Theory, AutoFakeItEasyData(typeof(Customization))]
        public void ItShoulMapTheExpectedValues(DAL.TrackingInfo trackingInfo, IMapper sut)
        {
            // Arrange
            var likeness = trackingInfo.AsSource()
                                        .OfLikeness<Index.Model>();

            // Act
            var result = sut.Map<DAL.TrackingInfo, Index.Model>(trackingInfo);

            // Assert
            likeness.ShouldEqual(result);
        }
    }

    public class WhenMappingFromATrackingInfoToADetailModel
    {
        [Theory, AutoFakeItEasyData(typeof(Customization))]
        public void ItShoulMapTheExpectedValues(DAL.TrackingInfo trackingInfo, IMapper sut)
        {
            // Arrange
            var likeness = trackingInfo.AsSource()
                                        .OfLikeness<Details.Model>();

            // Act
            var result = sut.Map<DAL.TrackingInfo, Details.Model>(trackingInfo);

            // Assert
            likeness.ShouldEqual(result);
        }
    }
}
