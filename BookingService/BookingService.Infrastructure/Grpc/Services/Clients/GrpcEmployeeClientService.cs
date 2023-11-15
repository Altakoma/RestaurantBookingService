using BookingService.Application;
using BookingService.Application.Interfaces.GrpcServices;
using BookingService.Domain.Exceptions;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;

namespace BookingService.Infrastructure.Grpc.Services.Clients
{
    public class GrpcClientEmployeeService : IGrpcClientEmployeeService
    {
        private const string ConfigurationServerAddressString = "CatalogService";

        private readonly IConfiguration _configuration;

        public GrpcClientEmployeeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IsWorkingAtRestaurantReply> EmployeeWorksAtRestaurant(
            IsWorkingAtRestaurantRequest request, CancellationToken cancellationToken)
        {
            string? serverAddress = _configuration[ConfigurationServerAddressString];

            if (serverAddress is null)
            {
                throw new BadConfigurationProvidedException(
                    ExceptionMessages.BadConfigurationProvidedMessage);
            }

            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
            };

            var channelOptions = new GrpcChannelOptions
            {
                HttpHandler = httpHandler,
            };

            using (var channel = GrpcChannel.ForAddress(serverAddress, channelOptions))
            {
                var client = new EmployeeGrpcService.EmployeeGrpcServiceClient(channel);

                var reply = await client.EmployeeWorksAtRestaurantAsync(request,
                    cancellationToken: cancellationToken);

                return reply;
            }
        }
    }
}
