using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Data.Repositories
{
    public class FoodTypeRepository : IFoodTypeRepository
    {
        private readonly CatalogServiceDbContext _dbContext;

        public FoodTypeRepository(CatalogServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteAsync(FoodType item)
        {
            _dbContext.Remove(item);
            return await _dbContext.SaveChangesToDbAsync();
        }

        public async Task<ICollection<FoodType>> GetAllAsync()
        {
            var foodTypes = await _dbContext.FoodTypes
                .Select(u => u).ToListAsync();

            return foodTypes;
        }

        public async Task<FoodType?> GetByIdAsync(int id)
        {
            var foodType = await _dbContext.FoodTypes
                .FirstOrDefaultAsync(u => u.Id == id);

            return foodType;
        }

        public async Task<(FoodType, bool)> InsertAsync(FoodType item)
        {
            await _dbContext.AddAsync(item);
            bool isInserted = await _dbContext.SaveChangesToDbAsync();

            return (item, isInserted);
        }

        public async Task<bool> UpdateAsync(FoodType item)
        {
            _dbContext.Update(item);
            return await _dbContext.SaveChangesToDbAsync();
        }
    }
}
