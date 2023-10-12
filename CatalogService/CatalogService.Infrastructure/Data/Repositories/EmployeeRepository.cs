using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CatalogServiceDbContext _dbContext;

        public EmployeeRepository(CatalogServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteAsync(Employee item)
        {
            _dbContext.Remove(item);
            return await _dbContext.SaveChangesToDbAsync();
        }

        public async Task<ICollection<Employee>> GetAllAsync()
        {
            var employees = await _dbContext.Employees.Include(e => e.Restaurant)
                .Select(u => u).ToListAsync();

            return employees;
        }

        public async Task<ICollection<Employee>> GetAllByRestaurantIdAsync(int id)
        {
            var employees = await _dbContext.Employees.Include(e => e.Restaurant)
                .Where(e => e.RestaurantId == id).ToListAsync();

            return employees;
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            var employee = await _dbContext.Employees.Include(e => e.Restaurant)
                .FirstOrDefaultAsync(u => u.Id == id);

            return employee;
        }

        public async Task<(Employee, bool)> InsertAsync(Employee item)
        {
            await _dbContext.AddAsync(item);
            bool isInserted = await _dbContext.SaveChangesToDbAsync();

            return (item, isInserted);
        }

        public async Task<bool> UpdateAsync(Employee item)
        {
            _dbContext.Update(item);
            return await _dbContext.SaveChangesToDbAsync();
        }
    }
}
