using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Services;
using CatalogService.Application.ServicesConfigurations;
using CatalogService.Application.TokenParsers;
using CatalogService.Application.TokenParsers.Interfaces;
using CatalogService.Domain.Interfaces.Services;
using CatalogService.Infrastructure.Data;
using CatalogService.Infrastructure.Data.Repositories;
using Google.Protobuf.WellKnownTypes;

namespace CatalogService.Presentation.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            services.AddHttpContextAccessor();

            services.AddControllers();

            builder.Services.AddGrpc();

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

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IFoodTypeRepository, FoodTypeRepository>();

            services.AddScoped<IBaseEmployeeService, EmployeeService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IBaseRestaurantService, RestaurantService>();
            services.AddScoped<IBaseFoodTypeService, FoodTypeService>();

            services.AddSingleton<ITokenParser, JwtTokenParser>();

            return services;
        }
    }
}
