using IdentityService.API.Controllers;
using IdentityService.API.Tests.Mocks.Producers;
using IdentityService.API.Tests.Mocks.Services;
using IdentityService.BusinessLogic.DTOs.Base.Messages;
using IdentityService.BusinessLogic.KafkaMessageBroker.Interfaces.Producers;
using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.DataAccess.DatabaseContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Testcontainers.MsSql;
using Testcontainers.Redis;

namespace IdentityService.API.IntegrationTests
{
    public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>,
        IAsyncLifetime
    {
        public CookieServiceMock CookieServiceMock { get; private set; }
        public UserMessageProducerMock UserMessageProducerMock{ get; private set; }

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
                var dbDescriptor = services.SingleOrDefault(
                    service => service.ServiceType == typeof(DbContextOptions<IdentityDbContext>));

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
                var userMessageProducerDescriptor = services.SingleOrDefault(
                    service => service.ServiceType == typeof(IUserMessageProducer));

                if (userMessageProducerDescriptor is not null)
                {
                    services.Remove(userMessageProducerDescriptor);
                }

                var cookieServiceDescriptor = services.SingleOrDefault(
                    service => service.ServiceType == typeof(ICookieService));

                if (cookieServiceDescriptor is not null)
                {
                    services.Remove(cookieServiceDescriptor);
                }

                CookieServiceMock = new CookieServiceMock();

                UserMessageProducerMock = new UserMessageProducerMock();

                services.AddScoped(serviceProvider => UserMessageProducerMock.Object)
                        .AddSingleton(CookieServiceMock.Object);

                services.AddDbContext<IdentityDbContext>(options =>
                {
                    options.UseSqlServer(_dbContainer.GetConnectionString());
                });

                services.AddScoped<AuthController>()
                        .AddScoped<UserController>()
                        .AddScoped<UserRoleController>();
            });
        }

        public new async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();

            await _redisContainer.StopAsync();
        }
    }
}
