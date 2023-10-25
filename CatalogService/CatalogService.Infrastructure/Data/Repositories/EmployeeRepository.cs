using AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using CatalogService.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Data.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(CatalogServiceDbContext catalogServiceDbContext,
            IMapper mapper) : base(catalogServiceDbContext, mapper)
        {
        }

        public async Task<ICollection<U>> GetAllByRestaurantIdAsync<U>(int id,
            CancellationToken cancellationToken)
        {
            ICollection<U> readEmployeeDTOs = await _mapper.ProjectTo<U>(
                _catalogServiceDbContext.Employees
                .Where(employee => employee.RestaurantId == id))
                .ToListAsync(cancellationToken);

            return readEmployeeDTOs;
        }

        public async Task<bool> Exists(int id, CancellationToken cancellationToken)
        {
            bool isEmployeeExist = await _catalogServiceDbContext
                .Employees.AnyAsync(employee => employee.Id == id, cancellationToken);

            return isEmployeeExist;
        }
    }
}
