using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using OrderService.Application;
using OrderService.Application.Interfaces.GrpcServices;
using OrderService.Domain.Exceptions;

namespace OrderService.Infrastructure.Grpc.Services.Clients
{
    public class GrpcClientBookingService : IGrpcClientBookingService
    {
        public const string EnvironmentServerAddressString = "BookingService";
        public const string ConfigurationServerAddressString = "BookingService";

        private readonly IConfiguration _configuration;

        public GrpcClientBookingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IsClientBookedTableReply> IsClientBookedTable(
            IsClientBookedTableRequest request, CancellationToken cancellationToken)
        {
            string? serverAddress =
                Environment.GetEnvironmentVariable(EnvironmentServerAddressString) ??
                _configuration[ConfigurationServerAddressString];

            if (serverAddress is null)
            {
                throw new BadConfigurationProvidedException(
                    ExceptionMessages.BadConfigurationProvidedMessage);
            }

            using (var channel = GrpcChannel.ForAddress(serverAddress))
            {
                var client = new BookingGrpcService.BookingGrpcServiceClient(channel);
                var reply = await client.IsClientBookedTableAsync(request,
                    cancellationToken: cancellationToken);

                return reply;
            }
        }
    }
}
