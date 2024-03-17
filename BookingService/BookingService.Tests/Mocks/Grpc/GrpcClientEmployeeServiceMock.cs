using BookingService.Application;
using BookingService.Application.Interfaces.GrpcServices;
using Moq;

namespace BookingService.Tests.Mocks.Grpc
{
    public class GrpcClientEmployeeServiceMock : Mock<IGrpcClientEmployeeService>
    {
        public GrpcClientEmployeeServiceMock MockIsEmployeeWorkingAtRestaurant(
            IsWorkingAtRestaurantRequest request, IsWorkingAtRestaurantReply reply)
        {
            Setup(grpcClient =>
                grpcClient.IsEmployeeWorkingAtRestaurant(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(reply)
            .Verifiable();

            return this;
        }
    }
}
