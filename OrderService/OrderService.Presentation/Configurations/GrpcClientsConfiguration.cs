using OrderService.Application;
using OrderService.Application.Interfaces.GrpcServices;
using OrderService.Infrastructure.Grpc.Services.Clients;

namespace OrderService.Presentation.Configurations
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

            services.AddGrpcClient<BookingGrpcService.BookingGrpcServiceClient>(options =>
            {
                options.Address = new Uri(configuration["BookingService"]!);
            })
            .ConfigurePrimaryHttpMessageHandler(() => httpHandler);

            services.AddScoped<IGrpcClientBookingService, GrpcClientBookingService>();

            return services;
        }
    }
}
