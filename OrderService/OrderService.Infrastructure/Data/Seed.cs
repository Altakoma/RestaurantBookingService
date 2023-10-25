using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;

namespace BookingService.Infrastructure.Data
{
    public class Seed
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Seed(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void SeedData()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<OrderServiceSqlDbContext>();

                dbContext.Database.Migrate();
            }
        }
    }
}
