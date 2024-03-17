using Microsoft.AspNetCore.Http;
using Moq;

namespace BookingService.Tests.Mocks.HttpContextAccessors
{
    public class HttpContextAccessorMock : Mock<IHttpContextAccessor>
    {
    }
}
