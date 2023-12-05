using AutoMapper;
using BookingService.Application.MappingProfiles;

namespace BookingService.Tests.MappingsTests
{
    public class AutomapperProfilesTests
    {
        [Fact]
        public void BookingProfile_ShouldHaveValidConfiguration()
        {
            var mapperConfiguration = new MapperConfiguration(
                configure => configure.AddProfile(new BookingProfile()));

            mapperConfiguration.AssertConfigurationIsValid();
        }

        [Fact]
        public void RestaurantProfile_ShouldHaveValidConfiguration()
        {
            var profilesList = new List<Profile>
            {
                new RestaurantProfile(),
                new TableProfile(),
            };

            var mapperConfiguration = new MapperConfiguration(
                configure => configure.AddProfiles(profilesList));

            mapperConfiguration.AssertConfigurationIsValid();
        }

        [Fact]
        public void TableProfile_ShouldHaveValidConfiguration()
        {
            var mapperConfiguration = new MapperConfiguration(
                configure => configure.AddProfile(new TableProfile()));

            mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
