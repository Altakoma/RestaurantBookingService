using AutoMapper;
using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Application.Services.Base;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
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

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            ReadEmployeeDTO? readEmployeeDTO =
                await _employeeRepository.GetByIdAsync<ReadEmployeeDTO>(id, cancellationToken);

            if (readEmployeeDTO is null)
            {
                throw new NotFoundException(nameof(Employee),
                    id.ToString(), typeof(Employee));
            }

            await _employeeRepository.DeleteAsync(id, cancellationToken);

            bool isDeleted = await _employeeRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteAsync),
                    id.ToString(), typeof(Employee));
            }
        }

        public async Task<ICollection<T>> GetAllByRestaurantIdAsync<T>(
            int id, CancellationToken cancellationToken)
        {
            ICollection<T> readEmployeeDTOs = await _employeeRepository
                .GetAllByRestaurantIdAsync<T>(id, cancellationToken);

            return readEmployeeDTOs;
        }

        public async Task<T> GetByIdAsync<T>(int id, CancellationToken cancellationToken)
        {
            T? readEmployeeDTO =
                await _employeeRepository.GetByIdAsync<T>(id, cancellationToken);

            if (readEmployeeDTO is null)
            {
                throw new NotFoundException(nameof(Employee),
                    id.ToString(), typeof(Employee));
            }

            return readEmployeeDTO;
        }

        public async Task<T> UpdateAsync<U, T>(int id, U item,
            CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(item);
            employee.Id = id;

            _employeeRepository.Update(employee);

            bool isUpdated = await _employeeRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateAsync),
                    id.ToString(), typeof(Employee));
            }

            T? readEmployeeDTO = await _employeeRepository
                .GetByIdAsync<T>(id, cancellationToken);

            if (readEmployeeDTO is null)
            {
                throw new NotFoundException(nameof(Employee),
                    id.ToString(), typeof(Employee));
            }

            return readEmployeeDTO;
        }
    }
}
