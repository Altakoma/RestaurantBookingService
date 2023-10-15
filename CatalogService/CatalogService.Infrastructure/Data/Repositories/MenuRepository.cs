using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Data.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly CatalogServiceDbContext _dbContext;

        public MenuRepository(CatalogServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteAsync(Menu item)
        {
            _dbContext.Remove(item);
            return await _dbContext.SaveChangesToDbAsync();
        }

        public async Task<ICollection<Menu>> GetAllAsync()
        {
            var menu = await _dbContext.Menu.Include(m => m.FoodType)
                .Include(m => m.Restaurant)
                .Select(u => u).ToListAsync();

            return menu;
        }

        public async Task<Menu?> GetByIdAsync(int id)
        {
            var menu = await _dbContext.Menu.Include(m => m.FoodType)
                .Include(m => m.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);

            return menu;
        }

        public async Task<(Menu, bool)> InsertAsync(Menu item)
        {
            await _dbContext.AddAsync(item);
            bool isInserted = await _dbContext.SaveChangesToDbAsync();

            return (item, isInserted);
        }

        public async Task<bool> UpdateAsync(Menu item)
        {
            _dbContext.Update(item);
            return await _dbContext.SaveChangesToDbAsync();
        }

        public async Task<ICollection<Menu>> GetAllByRestaurantIdAsync(int id)
        {
            ICollection<Menu> menu = await _dbContext.Menu.Include(m => m.FoodType)
                .Where(m => m.RestaurantId == id).ToListAsync();

            return menu;
        }
    }
}
