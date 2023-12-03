using AutoMapper;
using IdentityService.BusinessLogic.MappingProfiles;

namespace IdentityService.API.Tests.MappingsTests
{
    public class AutomapperProfilesTests
    {
        [Fact]
        public void UserProfile_ShouldHaveValidConfiguration()
        {
            var mapperConfiguration = new MapperConfiguration(
                configure => configure.AddProfile(new UserProfile()));

            mapperConfiguration.AssertConfigurationIsValid();
        }

        [Fact]
        public void UserRoleProfile_ShouldHaveValidConfiguration()
        {
            var mapperConfiguration = new MapperConfiguration(
                configure => configure.AddProfile(new UserRoleProfile()));

            mapperConfiguration.AssertConfigurationIsValid();
        }
    }
}
