using AutoMapper;
using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.Extensions;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Application.Services.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using FluentValidation;

namespace CatalogService.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<InsertEmployeeDTO> _insertEmployeeValidator;
        private readonly IValidator<UpdateEmployeeDTO> _updateEmployeeValidator;

        public EmployeeService(IEmployeeRepository employeeRepository,
            IMapper mapper,
            IValidator<InsertEmployeeDTO> insertEmployeeValidator,
            IValidator<UpdateEmployeeDTO> updateEmployeeValidator)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _insertEmployeeValidator = insertEmployeeValidator;
            _updateEmployeeValidator = updateEmployeeValidator;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            ReadEmployeeDTO? readEmployeeDTO =
                await _employeeRepository.GetByIdAsync(id, cancellationToken);

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

        public async Task<ICollection<ReadEmployeeDTO>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadEmployeeDTO> readEmployeeDTOs =
                await _employeeRepository.GetAllAsync(cancellationToken);

            return readEmployeeDTOs;
        }

        public async Task<ICollection<ReadEmployeeDTO>> GetAllByRestaurantIdAsync(
            int id, CancellationToken cancellationToken)
        {
            ICollection<ReadEmployeeDTO> readEmployeeDTOs = await _employeeRepository
                .GetAllByRestaurantIdAsync(id, cancellationToken);

            return readEmployeeDTOs;
        }

        public async Task<ReadEmployeeDTO> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            ReadEmployeeDTO? readEmployeeDTO =
                await _employeeRepository.GetByIdAsync(id, cancellationToken);

            if (readEmployeeDTO is null)
            {
                throw new NotFoundException(nameof(Employee),
                    id.ToString(), typeof(Employee));
            }

            return readEmployeeDTO;
        }

        public async Task<ReadEmployeeDTO> InsertAsync(InsertEmployeeDTO item,
            CancellationToken cancellationToken)
        {
            await _insertEmployeeValidator
                  .ValidateAndThrowArgumentExceptionAsync(item, cancellationToken);

            var employee = _mapper.Map<Employee>(item);

            employee = await _employeeRepository
                             .InsertAsync(employee, cancellationToken);

            bool isInserted = await _employeeRepository
                                    .SaveChangesToDbAsync(cancellationToken);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertAsync),
                    employee.Id.ToString(), typeof(Employee));
            }

            var readEmployeeDTO = _mapper.Map<ReadEmployeeDTO>(employee);

            return readEmployeeDTO;
        }

        public async Task<ReadEmployeeDTO> UpdateAsync(int id,
            UpdateEmployeeDTO item, CancellationToken cancellationToken)
        {
            await _updateEmployeeValidator
                  .ValidateAndThrowArgumentExceptionAsync(item, cancellationToken);

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

            ReadEmployeeDTO? readEmployeeDTO =
                await _employeeRepository.GetByIdAsync(id, cancellationToken);

            if (readEmployeeDTO is null)
            {
                throw new NotFoundException(nameof(Employee),
                    id.ToString(), typeof(Employee));
            }

            return readEmployeeDTO;
        }
    }
}
