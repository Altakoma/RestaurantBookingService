using Microsoft.AspNetCore.Http;
using Moq;

namespace CatalogService.Tests.Mocks.HttpContextAccessors
{
    public class HttpContextAccessorMock : Mock<IHttpContextAccessor>
    {
    }
}
