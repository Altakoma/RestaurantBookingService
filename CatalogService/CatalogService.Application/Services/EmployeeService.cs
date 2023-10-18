using AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Services.Base;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces.Services;

namespace CatalogService.Application.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository,
            IMapper mapper) : base(employeeRepository, mapper)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ICollection<T>> GetAllByRestaurantIdAsync<T>(
            int id, CancellationToken cancellationToken)
        {
            ICollection<T> readEmployeeDTOs = await _employeeRepository
                .GetAllByRestaurantIdAsync<T>(id, cancellationToken);

            return readEmployeeDTOs;
        }
    }
}
