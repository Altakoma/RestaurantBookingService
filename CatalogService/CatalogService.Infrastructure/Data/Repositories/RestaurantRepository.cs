using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Data.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly CatalogServiceDbContext _dbContext;

        public RestaurantRepository(CatalogServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteAsync(Restaurant item)
        {
            _dbContext.Remove(item);
            return await _dbContext.SaveChangesToDbAsync();
        }

        public async Task<ICollection<Restaurant>> GetAllAsync()
        {
            var restaurants = await _dbContext.Restaurants
                .Select(u => u).ToListAsync();

            return restaurants;
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restaurant = await _dbContext.Restaurants
                .FirstOrDefaultAsync(u => u.Id == id);

            return restaurant;
        }

        public async Task<(Restaurant, bool)> InsertAsync(Restaurant item)
        {
            await _dbContext.AddAsync(item);
            bool isInserted = await _dbContext.SaveChangesToDbAsync();

            return (item, isInserted);
        }

        public async Task<bool> UpdateAsync(Restaurant item)
        {
            _dbContext.Update(item);
            return await _dbContext.SaveChangesToDbAsync();
        }
    }
}
