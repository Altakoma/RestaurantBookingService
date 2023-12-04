using IdentityService.DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.API.Configurations
{
    public static class DbConfiguration
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration
                                            .GetConnectionString("SqlConnection"));
            });

            return services;
        }
    }
}
