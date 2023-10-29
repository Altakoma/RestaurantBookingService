using BookingService.Application.Interfaces.GrpcServices;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Services;
using BookingService.Application.ServicesConfigurations;
using BookingService.Application.TokenParsers;
using BookingService.Application.TokenParsers.Interfaces;
using BookingService.Domain.Interfaces.Services;
using BookingService.Infrastructure.Data;
using BookingService.Infrastructure.Data.Repositories;
using BookingService.Infrastructure.Grpc.Services.Clients;

namespace BookingService.Presentation.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            services.AddControllers();

            services.AddHttpContextAccessor();

            services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddDatabaseContext(builder);

            services.AddEndpointsApiExplorer();

            services.AddSwagger();

            services.AddSingleton<Seed>();

            services.AddMapper();

            services.AddFluentValidation();

            services.AddJwtTokenAuthConfiguration(builder);

            services.AddAuthorization();

            services.AddScoped<ITableRepository, TableRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IGrpcEmployeeClientService, EmployeeClientService>();

            services.AddSingleton<ITokenParser, JwtTokenParser>();

            return services;
        }
    }
}
