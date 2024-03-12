using Microsoft.AspNetCore.Http;
using Moq;

namespace OrderService.Tests.Mocks.HttpContextAccessors
{
    public class HttpContextAccessorMock : Mock<IHttpContextAccessor>
    {
    }
}
