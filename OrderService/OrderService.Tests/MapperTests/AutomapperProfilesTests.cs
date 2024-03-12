using AutoMapper;
using OrderService.Application.MappingProfiles;

namespace OrderService.Tests.MapperTests
{
    public class AutomapperProfilesTests
    {
        [Fact]
        public void ClientProfile_ShouldHaveValidConfiguration()
        {
            var mapperConfiguration = new MapperConfiguration(
                configure => configure.AddProfile(new ClientProfile()));

            mapperConfiguration.AssertConfigurationIsValid();
        }

        [Fact]
        public void OrderProfile_ShouldHaveValidConfiguration()
        {
            var profilesList = new List<Profile>
            {
                new OrderProfile(),
                new MenuProfile(),
                new ClientProfile(),
            };

            var mapperConfiguration = new MapperConfiguration(
                configure => configure.AddProfiles(profilesList));

            mapperConfiguration.AssertConfigurationIsValid();
        }

        [Fact]
        public void MenuProfile_ShouldHaveValidConfiguration()
        {
            var mapperConfiguration = new MapperConfiguration(
                configure => configure.AddProfile(new MenuProfile()));

            mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
