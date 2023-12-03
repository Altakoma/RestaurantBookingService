using FluentAssertions;
using IdentityService.API.Tests.Mocks.HttpContextAccessors;
using IdentityService.BusinessLogic.Services;
using IdentityService.BusinessLogic.Services.Interfaces;

namespace IdentityService.API.Tests.ServicesTests
{
    public class CookieServiceTests
    {
        private readonly HttpContextAccessorMock _httpContextAccessorMock;

        private readonly ICookieService _cookieService;

        public CookieServiceTests()
        {
            _httpContextAccessorMock = new();

            _cookieService = new CookieService(_httpContextAccessorMock.Object);
        }

        [Theory]
        [InlineData("testCookie", "cookieValue")]
        public void GetCookieValue_WhenItIsExisting_ReturnsCookieValue(string cookieName,
            string cookieValue)
        {
            //Arrange
            _httpContextAccessorMock.MockCookieFirstOrDefault(cookieName, cookieValue);

            //Act
            var result = _cookieService.GetCookieValue(cookieName);

            //Assert
            result.Should().BeEquivalentTo(cookieValue);

            _httpContextAccessorMock.Verify();
        }

        [Theory]
        [InlineData("testCookie", "cookieValue")]
        public void SetCookieValue_SuccessfullyExecuted(string cookieName,
            string cookieValue)
        {
            //Arrange
            _httpContextAccessorMock.MockCookieAppend(cookieName, cookieValue);

            //Act
            _cookieService.SetCookieValue(cookieName, cookieValue);

            //Assert
            _httpContextAccessorMock.Verify();
        }
    }
}
