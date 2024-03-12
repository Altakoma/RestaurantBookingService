using MediatR;
using OrderService.Application.BackgroundServices;
using OrderService.Application.Interfaces.Kafka.Consumers;
using OrderService.Application.Interfaces.Repositories.NoSql;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Application.ServicesConfigurations;
using OrderService.Application.TokenParsers;
using OrderService.Application.TokenParsers.Interfaces;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.KafkaMessageBroker.Consumers;
using OrderService.Infrastructure.Repositories.NoSql;
using OrderService.Infrastructure.Repositories.Sql;
using OrderService.Presentation.Behaviors;
using Serilog;

namespace OrderService.Presentation.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureServices(
            this IServiceCollection services, WebApplicationBuilder builder)
        {
            LoggingConfiguration.ConfigureLogging();

            builder.Host.UseSerilog();

            services.AddControllers();

            services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddHttpContextAccessor();

            services.AddDatabaseContext(builder);

            services.ConfigureKafkaOptions(builder.Configuration);

            services.AddHangfire(builder);

            services.AddEndpointsApiExplorer();

            services.AddSwagger();

            services.AddMapper();

            services.AddMediatR();

            services.AddFluentValidation();

            services.AddJwtTokenAuthConfiguration(builder);

            services.AddAuthorization();

            services.AddTransient(
                typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

            services.AddScoped<INoSqlOrderRepository, NoSqlOrderRepository>();

            services.AddScoped<ISqlOrderRepository, SqlOrderRepository>();

            services.AddSingleton<ITokenParser, JwtTokenParser>();

            services.AddGrpcClients(builder.Configuration);

            services.AddSingleton<Seed>();

            services.AddSingleton<IClientMessageConsumer, ClientMessageConsumer>();
            services.AddSingleton<IMenuMessageConsumer, MenuMessageConsumer>();

            services.AddHostedService<ClientConsumingMessagesHandlingService>();
            services.AddHostedService<MenuConsumingMessagesHandlingService>();

            return services;
        }
    }
}
