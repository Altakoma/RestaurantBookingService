using Hangfire;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Interfaces.GrpcServices;
using OrderService.Application.TokenParsers.Interfaces;
using OrderService.Infrastructure.Data.ApplicationNoSqlDbSettings;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;
using OrderService.Presentation;
using OrderService.Presentation.Controllers;
using OrderService.Tests.Mocks.BackgroundJobClients;
using OrderService.Tests.Mocks.GrpcClientServices;
using OrderService.Tests.Mocks.TokenParsers;
using Testcontainers.MongoDb;
using Testcontainers.MsSql;

namespace OrderService.IntegrationTests
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>,
        IAsyncLifetime
    {
        private readonly MongoDbContainer _noSqlDbContainer;
        private readonly MsSqlContainer _sqlDbContainer;

        public BackgroundJobClientMock BackgroundJobClientMock { get; private set; }
        public TokenParserMock TokenParserMock { get; private set; }
        public GrpcClientBookingServiceMock GrpcClientBookingServiceMock { get; private set; }

        public IntegrationTestWebAppFactory()
        {
            _sqlDbContainer = new MsSqlBuilder()
                                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                                .Build();

            _noSqlDbContainer = new MongoDbBuilder()
                                .WithImage("mongo:latest")
                                .Build();
        }

        public async Task InitializeAsync()
        {
            await _sqlDbContainer.StartAsync();

            await _noSqlDbContainer.StartAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");

            builder.ConfigureTestServices(services =>
            {
                var dbDescriptor = services.SingleOrDefault(service =>
                    service.ServiceType == typeof(DbContextOptions<OrderServiceSqlDbContext>));

                if (dbDescriptor is not null)
                {
                    services.Remove(dbDescriptor);
                }

                services.AddDbContext<OrderServiceSqlDbContext>(options =>
                {
                    options.UseSqlServer(_sqlDbContainer.GetConnectionString());
                });

                var mongoDbSettingsDescriptor = services.SingleOrDefault(service =>
                    service.ServiceType == typeof(IMongoDbSettings));

                if (mongoDbSettingsDescriptor is not null)
                {
                    services.Remove(mongoDbSettingsDescriptor);
                }

                var mongoDbSettings = new MongoDbSettings
                {
                    DatabaseName = _noSqlDbContainer.Name.Replace("/", string.Empty),
                    ConnectionString = _noSqlDbContainer.GetConnectionString()
                };

                services.AddSingleton<IMongoDbSettings>(serviceProvider => mongoDbSettings);

                var backgroundJobClientMock = services.SingleOrDefault(
                    descriptor => descriptor.ServiceType == typeof(IBackgroundJobClient));

                if (backgroundJobClientMock is not null)
                {
                    services.Remove(backgroundJobClientMock);
                }

                BackgroundJobClientMock = new BackgroundJobClientMock();

                services.AddScoped(serviceProvider => BackgroundJobClientMock.Object);

                var grpcClientBookingServiceDescriptor = services.SingleOrDefault(
                    descriptor => descriptor.ServiceType == typeof(IGrpcClientBookingService));

                if (grpcClientBookingServiceDescriptor is not null)
                {
                    services.Remove(grpcClientBookingServiceDescriptor);
                }

                GrpcClientBookingServiceMock = new GrpcClientBookingServiceMock();

                services.AddScoped(serviceProvider => GrpcClientBookingServiceMock.Object);

                var tokenParserDescriptor = services.SingleOrDefault(
                    descriptor => descriptor.ServiceType == typeof(ITokenParser));

                if (tokenParserDescriptor is not null)
                {
                    services.Remove(tokenParserDescriptor);
                }

                TokenParserMock = new TokenParserMock();

                services.AddSingleton(TokenParserMock.Object);

                services.AddScoped<OrderController>();
            });
        }

        public new async Task DisposeAsync()
        {
            await _sqlDbContainer.StopAsync();

            await _noSqlDbContainer.StopAsync();
        }
    }
}
