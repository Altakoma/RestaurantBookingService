using CatalogService.Application;
using CatalogService.Application.Interfaces.GrpcServices;
using CatalogService.Infrastructure.Grpc.Services.Clients;

namespace CatalogService.Presentation.Configurations
{
    public static class GrpcClientsConfiguration
    {
        public static IServiceCollection AddGrpcClients(this IServiceCollection services,
            IConfiguration configuration)
        {
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
            };

            services.AddGrpcClient<EmployeeGrpcService.EmployeeGrpcServiceClient>(options =>
            {
                options.Address = new Uri(configuration["IdentityService"]!);
            })
            .ConfigurePrimaryHttpMessageHandler(() => httpHandler);

            services.AddScoped<IGrpcEmployeeClientService, GrpcEmployeeClientService>();

            return services;
        }
    }
}
