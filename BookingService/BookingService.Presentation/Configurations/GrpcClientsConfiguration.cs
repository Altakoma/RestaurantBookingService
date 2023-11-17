using BookingService.Application;
using BookingService.Application.Interfaces.GrpcServices;
using BookingService.Infrastructure.Grpc.Services.Clients;

namespace BookingService.Presentation.Configurations
{
    public static class GrpcClientsConfiguration
    {
        private const string ConfigurationServerAddressString = "CatalogService";

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
                options.Address = new Uri(configuration[ConfigurationServerAddressString]!);
            })
            .ConfigurePrimaryHttpMessageHandler(() => httpHandler);

            services.AddScoped<IGrpcClientEmployeeService, GrpcClientEmployeeService>();

            return services;
        }
    }
}
