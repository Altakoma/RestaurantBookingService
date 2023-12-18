using BookingService.Application.Interfaces.GrpcServices;
using BookingService.Application.Interfaces.HubServices;
using BookingService.Application.TokenParsers.Interfaces;
using BookingService.Infrastructure.Data.ApplicationDbContext;
using BookingService.Presentation;
using BookingService.Presentation.Controllers;
using BookingService.Tests.Mocks.Grpc;
using BookingService.Tests.Mocks.HubServices;
using BookingService.Tests.Mocks.TokenParsers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace BookingService.IntegrationTests
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>,
        IAsyncLifetime
    {
        public TokenParserMock TokenParserMock { get; private set; }
        public BookingHubServiceMock BookingHubServiceMock { get; private set; }
        public GrpcClientEmployeeServiceMock GrpcClientEmployeeServiceMock { get; private set; }

        private readonly PostgreSqlContainer _dbContainer;

        public IntegrationTestWebAppFactory()
        {
            _dbContainer = new PostgreSqlBuilder()
                                .WithImage("postgres:12.16-bullseye")
                                .Build();
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");

            builder.ConfigureTestServices(services =>
            {
                var dbDescriptor = services.SingleOrDefault(service =>
                    service.ServiceType == typeof(DbContextOptions<BookingServiceDbContext>));

                if (dbDescriptor is not null)
                {
                    services.Remove(dbDescriptor);
                }

                services.AddDbContext<BookingServiceDbContext>(options =>
                {
                    options.UseNpgsql(_dbContainer.GetConnectionString());
                });

                var tokenParserDescriptor = services.SingleOrDefault(
                    descriptor => descriptor.ServiceType == typeof(ITokenParser));

                if (tokenParserDescriptor is not null)
                {
                    services.Remove(tokenParserDescriptor);
                }

                TokenParserMock = new TokenParserMock();

                services.AddSingleton(TokenParserMock.Object);

                var grpcClientEmployeeServiceDescriptor = services.SingleOrDefault(
                    descriptor => descriptor.ServiceType == typeof(IGrpcClientEmployeeService));

                if (grpcClientEmployeeServiceDescriptor is not null)
                {
                    services.Remove(grpcClientEmployeeServiceDescriptor);
                }

                var bookingHubServiceDescriptor = services.SingleOrDefault(
                    descriptor => descriptor.ServiceType == typeof(IBookingHubService));

                if (bookingHubServiceDescriptor is not null)
                {
                    services.Remove(bookingHubServiceDescriptor);
                }

                BookingHubServiceMock = new BookingHubServiceMock();

                services.AddScoped(serviceProvider => BookingHubServiceMock.Object);

                GrpcClientEmployeeServiceMock = new GrpcClientEmployeeServiceMock();

                services.AddScoped(serviceProvider => GrpcClientEmployeeServiceMock.Object);

                services.AddScoped<BookingController>()
                             .AddScoped<RestaurantController>()
                             .AddScoped<TableController>();
            });
        }

        public new async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}
