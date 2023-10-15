using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Application.Services;
using CatalogService.Application.Services.Interfaces;
using CatalogService.Application.ServicesConfigurations;
using CatalogService.Infrastructure.Data;
using CatalogService.Infrastructure.Data.Repositories;

namespace CatalogService.Presentation.Configurations
{
    public static class ServicesConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            services.AddControllers();

            services.AddDatabaseContext(builder);

            services.AddEndpointsApiExplorer();

            services.AddSwagger();

            services.AddSingleton<Seed>();

            services.AddMapper();

            services.AddJwtTokenAuthConfiguration(builder);

            services.AddAuthorization();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IFoodTypeRepository, FoodTypeRepository>();

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IFoodTypeService, FoodTypeService>();

            return services;
        }
    }
}
