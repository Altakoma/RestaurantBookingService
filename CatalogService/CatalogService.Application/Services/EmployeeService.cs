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
                throw new NotFoundException(id.ToString(), typeof(Employee));
            }

            bool isDeleted = await _employeeRepository.DeleteAsync(employee);

            if (!isDeleted)
            {
                throw new DbOperationException(nameof(DeleteAsync), id.ToString(),
                    typeof(Employee));
            }
        }

        public async Task<ICollection<EmployeeDTO>> GetAllAsync()
        {
            ICollection<Employee> employees = await _employeeRepository.GetAllAsync();

            var employeeDTOs = _mapper.Map<ICollection<EmployeeDTO>>(employees);

            return employeeDTOs;
        }

        public async Task<EmployeeDTO> GetByIdAsync(int id)
        {
            Employee? employee = await _employeeRepository.GetByIdAsync(id);

            if (employee is null)
            {
                throw new NotFoundException(id.ToString(), typeof(Employee));
            }

            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);

            return employeeDTO;
        }

        public async Task<EmployeeDTO> InsertAsync(EmployeeDTO item)
        {
            var employee = _mapper.Map<Employee>(item);

            (employee, bool isInserted) = await _employeeRepository
                .InsertAsync(employee);

            if (!isInserted)
            {
                throw new DbOperationException(nameof(InsertAsync),
                    employee.Id.ToString(), typeof(Employee));
            }

            item = _mapper.Map<EmployeeDTO>(employee);

            return item;
        }

        public async Task<EmployeeDTO> UpdateAsync(int id, UpdateEmployeeDTO item)
        {
            var employee = _mapper.Map<Employee>(item);

            bool isUpdated = await _employeeRepository.UpdateAsync(employee);

            if (!isUpdated)
            {
                throw new DbOperationException(nameof(UpdateAsync), id.ToString(),
                    typeof(Employee));
            }

            employee = await _employeeRepository.GetByIdAsync(id);

            var employeeDTO = _mapper.Map<EmployeeDTO>(employee);

            return employeeDTO;
        }
    }
}
