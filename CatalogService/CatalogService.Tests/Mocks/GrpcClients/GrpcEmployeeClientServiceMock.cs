using CatalogService.Application;
using CatalogService.Application.Interfaces.GrpcServices;
using Moq;

namespace CatalogService.Tests.Mocks.GrpcClients
{
    public class GrpcEmployeeClientServiceMock : Mock<IGrpcEmployeeClientService>
    {
        public GrpcEmployeeClientServiceMock MockIsUserExisting(IsUserExistingRequest request,
            IsUserExistingReply reply)
        {
            Setup(grpcClient => grpcClient.IsUserExisting(request
                , It.IsAny<CancellationToken>()))
            .ReturnsAsync(reply)
            .Verifiable();

            return this;
        }
    }
}
