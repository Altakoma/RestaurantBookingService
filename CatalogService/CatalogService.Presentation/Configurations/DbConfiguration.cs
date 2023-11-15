using CatalogService.Infrastructure.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Presentation.Configurations
{
    public static class DbConfiguration
    {
        public const string DbConnectionString = "SqlConnection";

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            services.AddDbContext<CatalogServiceDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration
                                            .GetConnectionString(DbConnectionString));
            });

            return services;
        }
    }
}
