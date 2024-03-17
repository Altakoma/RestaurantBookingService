using BookingService.Application.Interfaces.HubServices;
using BookingService.Application.Interfaces.Kafka.Consumers;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Interfaces.Repositories.Base;
using BookingService.Application.Interfaces.Services;
using BookingService.Application.Services;
using BookingService.Application.Services.Background;
using BookingService.Application.ServicesConfigurations;
using BookingService.Application.TokenParsers;
using BookingService.Application.TokenParsers.Interfaces;
using BookingService.Domain.Entities;
using BookingService.Infrastructure.Data;
using BookingService.Infrastructure.Data.Repositories;
using BookingService.Infrastructure.KafkaMessageBroker.Consumers;
using BookingService.Infrastructure.SignalR.Services;
using Serilog;

namespace BookingService.Presentation.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            LoggingConfiguration.ConfigureLogging();

            //builder.Host.UseSerilog();

            services.AddControllers();

            //builder.Services.AddGrpc();

            services.AddHttpContextAccessor();

            services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddDatabaseContext(builder);

            //services.ConfigureKafkaOptions(builder.Configuration);

            services.AddEndpointsApiExplorer();

            services.AddSwagger();

            services.AddSingleton<Seed>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", corsPolicyBuilder =>
                corsPolicyBuilder.SetIsOriginAllowed(origin =>
                    new Uri(origin).Host == (builder.Configuration["CorsPolicyHost"]))
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });

            services.AddMapper();

            //services.AddSignalR();

            services.AddFluentValidation();

            services.AddJwtTokenAuthConfiguration(builder);

            services.AddAuthorization();

            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IRepository<Restaurant>, RestaurantRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IBookingService, Application.Services.BookingService>();

            services.AddScoped<IBookingHubService, BookingHubService>();

            //services.AddGrpcClients(builder.Configuration);

            services.AddSingleton<ITokenParser, JwtTokenParser>();

            //services.AddSingleton<IClientMessageConsumer, ClientMessageConsumer>();
            //services.AddSingleton<IRestaurantMessageConsumer, RestaurantMessageConsumer>();

            //services.AddHostedService<ClientConsumingMessagesHandlingService>();
            //services.AddHostedService<RestaurantConsumingMessagesHandlingService>();

            return services;
        }
    }
}
