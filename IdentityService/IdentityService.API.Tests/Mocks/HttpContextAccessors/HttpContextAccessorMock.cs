using Microsoft.AspNetCore.Http;
using Moq;

namespace IdentityService.API.Tests.Mocks.HttpContextAccessors
{
    public class HttpContextAccessorMock : Mock<IHttpContextAccessor>
    {
        public HttpContextAccessorMock MockCookieFirstOrDefault(string cookieName,
            string cookieValue)
        {
            var httpContext = new DefaultHttpContext();

            httpContext.Request.Headers["Cookie"] = $"{cookieName}={cookieValue}";

            var cookieCollection = httpContext.Request.Cookies;

            Setup(httpContextAccessorMock =>
                httpContextAccessorMock.HttpContext!.Request.Cookies)
            .Returns(cookieCollection)
            .Verifiable();

            return this;
        }

        public HttpContextAccessorMock MockCookieAppend(string cookieName,
            string cookieValue)
        {
            Setup(httpContextAccessorMock =>
                httpContextAccessorMock.HttpContext!.Response
                .Cookies.Append(cookieName, cookieValue, It.IsAny<CookieOptions>()))
            .Verifiable();

            return this;
        }
    }
}
