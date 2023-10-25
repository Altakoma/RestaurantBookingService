using BookingService.Infrastructure.Data;
using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Application.Interfaces.Repositories.Write;
using OrderService.Application.ServicesConfigurations;
using OrderService.Infrastructure.Repositories.Read;
using OrderService.Infrastructure.Repositories.Write;

namespace OrderService.Presentation.Configurations
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

            services.AddMapper();

            services.AddMediatR();

            services.AddFluentValidation();

            services.AddJwtTokenAuthConfiguration(builder);

            services.AddAuthorization();

            services.AddScoped<IReadClientRepository, ReadClientRepository>();
            services.AddScoped<IReadMenuRepository, ReadMenuRepository>();
            services.AddScoped<IReadOrderRepository, ReadOrderRepository>();
            services.AddScoped<IReadTableRepository, ReadTableRepository>();

            services.AddScoped<IWriteClientRepository, WriteClientRepository>();
            services.AddScoped<IWriteMenuRepository, WriteMenuRepository>();
            services.AddScoped<IWriteOrderRepository, WriteOrderRepository>();
            services.AddScoped<IWriteTableRepository, WriteTableRepository>();

            services.AddSingleton<Seed>();

            return services;
        }
    }
}
