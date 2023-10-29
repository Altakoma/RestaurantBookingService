using CatalogService.Application.Interfaces.Repositories;
using Grpc.Core;

namespace CatalogService.Infrastructure.Grpc.Services
{
    public class GrpcEmployeeService : EmployeeService.EmployeeServiceBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public GrpcEmployeeService(IEmployeeRepository employeeRepository)
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
