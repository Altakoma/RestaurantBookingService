﻿using BookingService.Application;
using BookingService.Application.Interfaces.GrpcServices;
using BookingService.Domain.Exceptions;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;

namespace BookingService.Infrastructure.Grpc.Services.Clients
{
    public class EmployeeClientService : IGrpcEmployeeClientService
    {
        public const string EnvironmentServerAddressString = "CatalogService";
        public const string ConfigurationServerAddressString = "CatalogService";

        private readonly IConfiguration _configuration;

        public EmployeeClientService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IsWorkingAtRestaurantReply> EmployeeWorksAtRestaurant(
            IsWorkingAtRestaurantRequest request, CancellationToken cancellationToken)
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
                var client = new EmployeeService.EmployeeServiceClient(channel);
                var reply = await client.EmployeeWorksAtRestaurantAsync(request,
                    cancellationToken: cancellationToken);

                return reply;
            }
        }
    }
}
