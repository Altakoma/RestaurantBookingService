using AutoMapper;
using CatalogService.Application.MappingProfiles;

namespace CatalogService.Tests.MappingsTests
{
    public class AutomapperProfilesTests
    {
        [Fact]
        public void EmployeeProfile_ShouldHaveValidConfiguration()
        {
            var mapperConfiguration = new MapperConfiguration(
                configure => configure.AddProfile(new EmployeeProfile()));

            mapperConfiguration.AssertConfigurationIsValid();
        }

        [Fact]
        public void FoodTypeProfile_ShouldHaveValidConfiguration()
        {
            var mapperConfiguration = new MapperConfiguration(
                configure => configure.AddProfile(new FoodTypeProfile()));

            mapperConfiguration.AssertConfigurationIsValid();
        }

        [Fact]
        public void MenuProfile_ShouldHaveValidConfiguration()
        {
            var profilesList = new List<Profile>
            {
                new MenuProfile(),
                new FoodTypeProfile(),
            };

            var mapperConfiguration = new MapperConfiguration(
                configure => configure.AddProfiles(profilesList));

            mapperConfiguration.AssertConfigurationIsValid();
        }

        [Fact]
        public void RestaurantProfile_ShouldHaveValidConfiguration()
        {
            var mapperConfiguration = new MapperConfiguration(
                configure => configure.AddProfile(new RestaurantProfile()));

            mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
