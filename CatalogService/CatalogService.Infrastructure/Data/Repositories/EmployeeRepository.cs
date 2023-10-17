using AutoMapper;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
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

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            Employee? employee = await _catalogServiceDbContext.Employees
                .FirstOrDefaultAsync(employee => employee.Id == id,
                                     cancellationToken);

            if (employee is null)
            {
                throw new NotFoundException(nameof(Employee), id.ToString(),
                    typeof(Employee));
            }

            Delete(employee);
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

        public async Task<U?> GetByIdAsync<U>(int id,
            CancellationToken cancellationToken)
        {
            U? readEmployeeDTO = await _mapper.ProjectTo<U>(
                _catalogServiceDbContext.Employees.Where(employee => employee.Id == id))
                .SingleOrDefaultAsync(cancellationToken);

            return readEmployeeDTO;
        }
    }
}
