using CatalogService.Application;
using CatalogService.Application.Interfaces.GrpcServices;

namespace CatalogService.Infrastructure.Grpc.Services.Clients
{
    public class GrpcEmployeeClientService : IGrpcEmployeeClientService
    {
        private readonly EmployeeGrpcService.EmployeeGrpcServiceClient _employeeGrpcServiceClient;

        public GrpcEmployeeClientService(
            EmployeeGrpcService.EmployeeGrpcServiceClient employeeGrpcServiceClient)
        {
            _employeeGrpcServiceClient = employeeGrpcServiceClient;
        }

        public async Task<IsUserExistingReply> IsUserExisting(
            IsUserExistingRequest request, CancellationToken cancellationToken)
        {
            var reply = await _employeeGrpcServiceClient.IsUserExistingAsync(request,
                    cancellationToken: cancellationToken);

            return reply;
        }
    }
}
