using BookingService.Application;
using BookingService.Application.Interfaces.GrpcServices;

namespace BookingService.Infrastructure.Grpc.Services.Clients
{
    public class GrpcClientEmployeeService : IGrpcClientEmployeeService
    {
        private readonly EmployeeGrpcService.EmployeeGrpcServiceClient _employeeGrpcServiceClient;

        public GrpcClientEmployeeService(
            EmployeeGrpcService.EmployeeGrpcServiceClient employeeGrpcServiceClient)
        {
            _employeeGrpcServiceClient = employeeGrpcServiceClient;
        }

        public async Task<IsWorkingAtRestaurantReply> IsEmployeeWorkingAtRestaurant(
            IsWorkingAtRestaurantRequest request, CancellationToken cancellationToken)
        {
            var reply = await _employeeGrpcServiceClient.IsEmployeeWorkingAtRestaurantAsync(request,
                    cancellationToken: cancellationToken);

            return reply;
        }
    }
}
