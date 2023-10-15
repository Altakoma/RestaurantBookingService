using AutoMapper;
using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using CatalogService.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Data.Repositories
{
    public class EmployeeRepository : WriteRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(CatalogServiceDbContext dbContext,
            IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            Employee? employee = await _dbContext.Employees
                .FirstOrDefaultAsync(employee => employee.Id == id,
                                     cancellationToken);

            if (employee is null)
            {
                throw new NotFoundException(nameof(Employee), id.ToString(),
                    typeof(Employee));
            }

            Delete(employee);
        }

        public async Task<ICollection<ReadEmployeeDTO>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var readEmployeeDTOs = await _mapper.ProjectTo<ReadEmployeeDTO>(
                _dbContext.Employees.Select(employee => employee))
                .ToListAsync(cancellationToken);

            return readEmployeeDTOs;
        }

        public async Task<ICollection<ReadEmployeeDTO>> GetAllByRestaurantIdAsync(int id,
            CancellationToken cancellationToken)
        {
            var readEmployeeDTOs = await _mapper.ProjectTo<ReadEmployeeDTO>(
                _dbContext.Employees
                .Where(employee => employee.RestaurantId == id))
                .ToListAsync(cancellationToken);

            return readEmployeeDTOs;
        }

        public async Task<ReadEmployeeDTO?> GetByIdAsync(int id, 
            CancellationToken cancellationToken)
        {
            var readEmployeeDTO = await _mapper.ProjectTo<ReadEmployeeDTO>(
                _dbContext.Employees.Where(employee => employee.Id == id))
                .SingleOrDefaultAsync(cancellationToken);

            return readEmployeeDTO;
        }
    }
}
