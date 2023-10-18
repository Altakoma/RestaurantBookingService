using BookingService.Infrastructure.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
                var dbContext = scope.ServiceProvider.GetRequiredService<BookingServiceDbContext>();

                dbContext.Database.Migrate();
            }
        }
    }
}
