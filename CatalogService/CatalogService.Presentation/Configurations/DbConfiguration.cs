using CatalogService.Infrastructure.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Presentation.Configurations
{
    public static class DbConfiguration
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            services.AddDbContext<CatalogServiceDbContext>(options =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                }
                else
                {
                    options.UseSqlServer(Environment.GetEnvironmentVariable("DefaultConnection"));
                }
            });

            return services;
        }
    }
}
