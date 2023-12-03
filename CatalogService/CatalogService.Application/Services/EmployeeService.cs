using AutoMapper;
using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.Interfaces.GrpcServices;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Redis.Interfaces;
using CatalogService.Application.Services.Base;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;

namespace CatalogService.Application.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IGrpcEmployeeClientService _grpcEmployeeClientService;

        private readonly IEmployeeCacheAccessor _employeeCacheAccessor;

        public EmployeeService(IEmployeeRepository employeeRepository,
            IGrpcEmployeeClientService grpcEmployeeClientService,
            IEmployeeCacheAccessor employeeCacheAccessor,
            IMapper mapper)
            : base(employeeRepository, mapper)
        {
            _employeeRepository = employeeRepository;
            _grpcEmployeeClientService = grpcEmployeeClientService;
            _employeeCacheAccessor = employeeCacheAccessor;
        }

        public override async Task<T> GetByIdAsync<T>(int id,
            CancellationToken cancellationToken)
        {
            T itemDTO = await _employeeCacheAccessor
                .GetByResourceIdAsync<T>(id.ToString(), cancellationToken);

            return itemDTO;
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

            IsUserExistingReply reply = await _grpcEmployeeClientService
                .IsUserExisting(request, cancellationToken);

            if (!reply.IsUserExisting)
            {
                throw new NotFoundException(nameof(Employee),
                    insertEmployeeDTO.Id.ToString(), typeof(Employee));
            }

            var employee = _mapper.Map<Employee>(insertEmployeeDTO);

            employee.Name = reply.UserName;

            T readEmployeeDTO = await InsertAsync<Employee, T>(
                                   employee, cancellationToken);

            return readEmployeeDTO;
        }

        public override async Task<int> DeleteAsync(int id,
            CancellationToken cancellationToken)
        {
            await _employeeCacheAccessor.DeleteResourceByIdAsync(id.ToString(),
                                                                 cancellationToken);

            return await base.DeleteAsync(id, cancellationToken);
        }

        public override async Task<T> UpdateAsync<U, T>(int id, U updateItemDTO,
            CancellationToken cancellationToken)
        {
            await _employeeCacheAccessor.DeleteResourceByIdAsync(id.ToString(), 
                                                                 cancellationToken);

            return await base.UpdateAsync<U, T>(id, updateItemDTO, cancellationToken);
        }
    }
}
