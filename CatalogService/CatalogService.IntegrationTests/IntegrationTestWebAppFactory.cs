using CatalogService.Application.Interfaces.GrpcServices;
using CatalogService.Application.Interfaces.Kafka.Producers;
using CatalogService.Application.TokenParsers.Interfaces;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using CatalogService.Presentation;
using CatalogService.Presentation.Controllers;
using CatalogService.Tests.Mocks.GrpcClients;
using CatalogService.Tests.Mocks.MessageProducers;
using CatalogService.Tests.Mocks.TokenParsers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Testcontainers.MsSql;
using Testcontainers.Redis;

namespace CatalogService.IntegrationTests
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>,
        IAsyncLifetime
    {
        public GrpcEmployeeClientServiceMock GrpcEmployeeClientServiceMock { get; private set; }
        public MenuMessageProducerMock MenuMessageProducerMock { get; private set; }
        public RestaurantMessageProducerMock RestaurantMessageProducerMock { get; private set; }
        public TokenParserMock TokenParserMock { get; private set; }

        private readonly MsSqlContainer _dbContainer;
        private readonly RedisContainer _redisContainer;

        public IntegrationTestWebAppFactory()
        {
            _dbContainer = new MsSqlBuilder()
                                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                                .Build();

            _redisContainer = new RedisBuilder()
                                    .WithImage("redis:7.2.3")
                                    .Build();
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            await _redisContainer.StartAsync();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production");

            builder.ConfigureTestServices(services =>
            {
                var dbDescriptor = services.SingleOrDefault(service =>
                    service.ServiceType == typeof(DbContextOptions<CatalogServiceDbContext>));

                if (dbDescriptor is not null)
                {
                    services.Remove(dbDescriptor);
                }

                var distributedCacheDescriptor = services.FirstOrDefault(descriptor =>
                    descriptor.ServiceType == typeof(IDistributedCache));

                if (distributedCacheDescriptor is not null)
                {
                    services.Remove(distributedCacheDescriptor);
                }

                services.AddStackExchangeRedisCache(redisOptions =>
                {
                    redisOptions.Configuration = _redisContainer.GetConnectionString();
                });

                var grpcEmployeeClientServiceDescriptor = services.SingleOrDefault(
                    descriptor => descriptor.ServiceType == typeof(IGrpcEmployeeClientService));

                if (grpcEmployeeClientServiceDescriptor is not null)
                {
                    services.Remove(grpcEmployeeClientServiceDescriptor);
                }

                GrpcEmployeeClientServiceMock = new GrpcEmployeeClientServiceMock();

                services.AddScoped(serviceProvider => GrpcEmployeeClientServiceMock.Object);

                var tokenParserDescriptor = services.SingleOrDefault(
                    descriptor => descriptor.ServiceType == typeof(ITokenParser));

                if (tokenParserDescriptor is not null)
                {
                    services.Remove(tokenParserDescriptor);
                }

                TokenParserMock = new TokenParserMock();

                services.AddSingleton(TokenParserMock.Object);

                var restaurantMessageProducerDescriptor = services.SingleOrDefault(
                    descriptor => descriptor.ServiceType == typeof(IRestaurantMessageProducer));

                if (restaurantMessageProducerDescriptor is not null)
                {
                    services.Remove(restaurantMessageProducerDescriptor);
                }

                RestaurantMessageProducerMock = new RestaurantMessageProducerMock();

                services.AddScoped(serviceProvider => RestaurantMessageProducerMock.Object);

                var menuMessageProducerDescriptor = services.SingleOrDefault(
                    descriptor => descriptor.ServiceType == typeof(IMenuMessageProducer));

                if (menuMessageProducerDescriptor is not null)
                {
                    services.Remove(menuMessageProducerDescriptor);
                }

                MenuMessageProducerMock = new MenuMessageProducerMock();

                services.AddScoped(serviceProvider => MenuMessageProducerMock.Object);

                services.AddDbContext<CatalogServiceDbContext>(options =>
                {
                    options.UseSqlServer(_dbContainer.GetConnectionString());
                });

                services.AddScoped<EmployeeController>()
                        .AddScoped<FoodTypeController>()
                        .AddScoped<MenuController>()
                        .AddScoped<RestaurantController>();
            });
        }

        public new async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();

            await _redisContainer.StopAsync();
        }
    }
}
