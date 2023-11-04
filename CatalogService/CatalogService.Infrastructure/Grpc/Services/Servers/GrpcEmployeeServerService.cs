using CatalogService.Application;
using CatalogService.Application.Interfaces.Repositories;
using Grpc.Core;

namespace CatalogService.Infrastructure.Grpc.Services.Servers
{
    public class GrpcServerEmployeeService : EmployeeGrpcService.EmployeeGrpcServiceBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GrpcServerEmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async override Task<IsWorkingAtRestaurantReply> EmployeeWorksAtRestaurant(
            IsWorkingAtRestaurantRequest request, ServerCallContext context)
        {
            bool isEmployeeWorkingAtRestaurant = await _employeeRepository
                .WorksAtRestaurantAsync(request.EmployeeId, request.RestaurantId,
                                        context.CancellationToken);

            var isWorkingAtRestaurantReply = new IsWorkingAtRestaurantReply
            {
                IsEmployeeWorkingAtRestaurant = isEmployeeWorkingAtRestaurant,
            };

            return isWorkingAtRestaurantReply;
        }
    }
}
