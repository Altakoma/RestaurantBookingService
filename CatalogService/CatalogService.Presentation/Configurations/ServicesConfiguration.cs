using CatalogService.Application.Interfaces.GrpcServices;
using CatalogService.Application.Interfaces.Kafka.Producers;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Redis.Interfaces;
using CatalogService.Application.Services;
using CatalogService.Application.ServicesConfigurations;
using CatalogService.Application.TokenParsers;
using CatalogService.Application.TokenParsers.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces.Services;
using CatalogService.Infrastructure.Data;
using CatalogService.Infrastructure.Data.Repositories;
using CatalogService.Infrastructure.Grpc.Services.Clients;
using CatalogService.Infrastructure.KafkaMessageBroker.Producers;
using CatalogService.Infrastructure.Redis.CacheAccessors;

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

            services.ConfigureRedis(builder.Configuration);

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddDatabaseContext(builder);

            services.ConfigureKafkaOptions(builder.Configuration);

            services.AddEndpointsApiExplorer();

            services.AddSwagger();

            services.AddSingleton<Seed>();

            services.AddMapper();

            services.AddFluentValidation();

            services.AddJwtTokenAuthConfiguration(builder);

            services.AddAuthorization();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IRepository<Employee>, EmployeeRepository>();

            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IRepository<Menu>, MenuRepository>();

            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IRepository<Restaurant>, RestaurantRepository>();

            services.AddScoped<IFoodTypeRepository, FoodTypeRepository>();
            services.AddScoped<IRepository<FoodType>, FoodTypeRepository>();

            services.AddScoped<IMenuCacheAccessor, MenuCacheAccessor>();
            services.AddScoped<IFoodTypeCacheAccessor, FoodCacheTypeAccessor>();
            services.AddScoped<IEmployeeCacheAccessor, EmployeeCacheAccessor>();
            services.AddScoped<IRestaurantCacheAccessor, RestaurantCacheAccessor>();

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IBaseRestaurantService, RestaurantService>();
            services.AddScoped<IBaseFoodTypeService, FoodTypeService>();

            services.AddGrpcClients(builder.Configuration);

            services.AddSingleton<ITokenParser, JwtTokenParser>();
            services.AddSingleton<IMenuMessageProducer, MenuMessageProducer>();
            services.AddSingleton<IRestaurantMessageProducer, RestaurantMessageProducer>();

            return services;
        }
    }
}
