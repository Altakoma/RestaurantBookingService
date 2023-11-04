using CatalogService.Application;
using CatalogService.Application.Interfaces.GrpcServices;
using CatalogService.Domain.Exceptions;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;

namespace CatalogService.Infrastructure.Grpc.Services.Clients
{
    public class GrpcEmployeeClientService :IGrpcEmployeeClientService
    {
        private const string EnvironmentServerAddressString = "IdentityService";
        private const string ConfigurationServerAddressString = "IdentityService";

        private readonly IConfiguration _configuration;

        public GrpcEmployeeClientService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IsUserExistingReply> UserExists(
            IsUserExistingRequest request, CancellationToken cancellationToken)
        {
            string? serverAddress =
                Environment.GetEnvironmentVariable(EnvironmentServerAddressString) ??
                _configuration[ConfigurationServerAddressString];

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

                var reply = await client.UserExistsAsync(request,
                    cancellationToken: cancellationToken);

                return reply;
            }
        }
    }
}
