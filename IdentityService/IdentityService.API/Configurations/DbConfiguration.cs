using IdentityService.DataAccess.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.API.Configurations
{
    public static class DbConfiguration
    {
        public const string DbDevelopmentConnectionString = "ConnectionStrings:DefaultConnection";
        public const string DbEnvironmentConnectionString = "DefaultConnection";

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            services.AddDbContext<IdentityDbContext>(options =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    options.UseSqlServer(builder.Configuration[DbDevelopmentConnectionString]);
                }
                else
                {
                    options.UseSqlServer(Environment.GetEnvironmentVariable(DbEnvironmentConnectionString));
                }
            });

            return services;
        }
    }
}
