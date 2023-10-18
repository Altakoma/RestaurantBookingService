﻿using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Services;
using CatalogService.Application.ServicesConfigurations;
using CatalogService.Domain.Interfaces.Services;
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

            services.AddMvc(options =>
            {
                options.SuppressAsyncSuffixInActionNames = false;
            });

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

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IFoodTypeService, FoodTypeService>();

            return services;
        }
    }
}
