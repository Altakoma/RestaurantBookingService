using AutoMapper;
using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Application.Services.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;

namespace CatalogService.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository,
            IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            Employee? employee = await _employeeRepository.GetByIdAsync(id);

            if (employee is null)
            {
                throw new NotFoundException(nameof(Employee),
                    id.ToString(), typeof(Employee));
            }

            bool isDeleted = await _employeeRepository.DeleteAsync(employee);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteAsync), id.ToString(),
                    typeof(Employee));
            }
        }

        public async Task<ICollection<ReadEmployeeDTO>> GetAllAsync()
        {
            ICollection<Employee> employees =
                await _employeeRepository.GetAllAsync();

            var readEmployeeDTOs = _mapper
                .Map<ICollection<ReadEmployeeDTO>>(employees);

            return readEmployeeDTOs;
        }

        public async Task<ICollection<ReadEmployeeDTO>> GetAllByRestaurantIdAsync(int id)
        {
            ICollection<Employee> employees =
                await _employeeRepository.GetAllByRestaurantIdAsync(id);

            var readEmployeeDTOs =
                _mapper.Map<ICollection<ReadEmployeeDTO>>(employees);

            return readEmployeeDTOs;
        }

        public async Task<ReadEmployeeDTO> GetByIdAsync(int id)
        {
            Employee? employee = await _employeeRepository.GetByIdAsync(id);

            if (employee is null)
            {
                throw new NotFoundException(nameof(Employee),
                    id.ToString(), typeof(Employee));
            }

            var readEmployeeDTO = _mapper.Map<ReadEmployeeDTO>(employee);

            return readEmployeeDTO;
        }

        public async Task<ReadEmployeeDTO> InsertAsync(InsertEmployeeDTO item)
        {
            var employee = _mapper.Map<Employee>(item);

            (employee, bool isInserted) = await _employeeRepository
                .InsertAsync(employee);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertAsync),
                    employee.Id.ToString(), typeof(Employee));
            }

            var readEmployeeDTO = _mapper.Map<ReadEmployeeDTO>(employee);

            return readEmployeeDTO;
        }

        public async Task<ReadEmployeeDTO> UpdateAsync(int id,
            UpdateEmployeeDTO item)
        {
            var employee = _mapper.Map<Employee>(item);
            employee.Id = id;

            bool isUpdated = await _employeeRepository.UpdateAsync(employee);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateAsync),
                    id.ToString(), typeof(Employee));
            }

            employee = await _employeeRepository.GetByIdAsync(id);

            var employeeDTO = _mapper.Map<ReadEmployeeDTO>(employee);

            return employeeDTO;
        }
    }
}
