using Moq;
using OrderService.Application;
using OrderService.Application.Interfaces.GrpcServices;

namespace OrderService.Tests.Mocks.GrpcClientServices
{
    public class GrpcClientBookingServiceMock : Mock<IGrpcClientBookingService>
    {
        public GrpcClientBookingServiceMock MockIsClientBookedTable(IsClientBookedTableRequest request,
            IsClientBookedTableReply reply)
        {
            Setup(grpcClientService => grpcClientService.IsClientBookedTable(
                It.Is<IsClientBookedTableRequest>(currentRequest =>
                currentRequest.ClientId == request.ClientId &&
                currentRequest.BookingId == request.BookingId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(reply)
            .Verifiable();

            return this;
        }
    }
}
