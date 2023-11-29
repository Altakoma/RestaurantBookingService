using AutoMapper;
using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.Interfaces.GrpcServices;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Services.Base;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;

namespace CatalogService.Application.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IGrpcEmployeeClientService _grpcEmployeeClientService;

        public EmployeeService(IEmployeeRepository employeeRepository,
            IMapper mapper, IGrpcEmployeeClientService grpcEmployeeClientService)
            : base(employeeRepository, mapper)
        {
            _employeeRepository = employeeRepository;
            _grpcEmployeeClientService = grpcEmployeeClientService;
        }

        public async Task<ICollection<T>> GetAllByRestaurantIdAsync<T>(
            int id, CancellationToken cancellationToken)
        {
            ICollection<T> readEmployeeDTOs = await _employeeRepository
                .GetAllByRestaurantIdAsync<T>(id, cancellationToken);

            return readEmployeeDTOs;
        }

        public async Task<T> InsertAsync<T>(InsertEmployeeDTO insertEmployeeDTO,
            CancellationToken cancellationToken)
        {
            var request = new IsUserExistingRequest
            {
                UserId = insertEmployeeDTO.Id,
            };

            var employee = _mapper.Map<Employee>(insertEmployeeDTO);

            IsUserExistingReply reply = await _grpcEmployeeClientService
                .IsUserExisting(request, cancellationToken);

            if (!reply.IsUserExisting)
            {
                throw new NotFoundException(nameof(Employee),
                    insertEmployeeDTO.Id.ToString(), typeof(Employee));
            }

            employee.Name = reply.UserName;

            T readEmployeeDTO = await InsertAsync<Employee, T>(
                                   employee, cancellationToken);

            return readEmployeeDTO;
        }
    }
}
