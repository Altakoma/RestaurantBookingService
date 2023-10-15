using CatalogService.Infrastructure.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure.Data
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
                var _dbContext = scope.ServiceProvider.GetRequiredService<CatalogServiceDbContext>();

                _dbContext.Database.Migrate();
            }
        }
    }
}
