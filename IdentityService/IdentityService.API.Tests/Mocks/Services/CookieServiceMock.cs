using IdentityService.BusinessLogic.Services;
using IdentityService.BusinessLogic.Services.Interfaces;
using Moq;

namespace IdentityService.API.Tests.Mocks.Services
{
    public class CookieServiceMock : Mock<ICookieService>
    {
        public CookieServiceMock MockGetCookieValue(string key, string value)
        {
            Setup(cookieService => cookieService.GetCookieValue(key))
            .Returns(value)
            .Verifiable();

            return this;
        }
        public CookieServiceMock MockSetCookieValue(string key, string value)
        {
            Setup(cookieService => cookieService.SetCookieValue(key, value))
            .Verifiable();

            return this;
        }
    }
}
