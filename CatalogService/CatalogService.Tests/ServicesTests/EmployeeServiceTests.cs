using AutoMapper;
using CatalogService.Application;
using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.MappingProfiles;
using CatalogService.Application.Services;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Tests.Fakers;
using CatalogService.Tests.Mocks.CacheAccessors;
using CatalogService.Tests.Mocks.GrpcClients;
using CatalogService.Tests.Mocks.Repositories;
using CatalogService.Tests.Mocks.Repositories.Base;
using CatalogService.Tests.ServicesTests.Base;
using FluentAssertions;
using Moq;

namespace CatalogService.Tests.ServicesTests
{
    public class EmployeeServiceTests : BaseServiceTests<IEmployeeRepository, Employee>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeService _employeeService;

        private readonly EmployeeRepositoryMock _employeeRepositoryMock;
        private readonly GrpcEmployeeClientServiceMock _grpcClientMock;
        private readonly EmployeeCacheAccessorMock _cacheAccessorMock;

        public EmployeeServiceTests() : base()
        {
            _mapper = new Mapper(new MapperConfiguration(configure =>
                configure.AddProfile(new EmployeeProfile())));

            _employeeRepositoryMock = new();
            _cacheAccessorMock = new();
            _grpcClientMock = new();

            _employeeService = new EmployeeService(_employeeRepositoryMock.Object,
                _grpcClientMock.Object, _cacheAccessorMock.Object, _mapperMock.Object);

            _baseRepositoryMock = _employeeRepositoryMock;
            _baseService = _employeeService;
        }

        [Fact]
        public async Task GetEmployeeByIdAsync_WhenItIsExisting_ReturnsReadEmployeeDTO()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            var readEmployeeDTO = _mapper.Map<ReadEmployeeDTO>(employee);

            _cacheAccessorMock.MockGetByResourceIdAsync(employee.Id.ToString(), readEmployeeDTO);

            //Act
            var result = await _employeeService.GetByIdAsync<ReadEmployeeDTO>(employee.Id,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readEmployeeDTO);

            _cacheAccessorMock.Verify();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsReadEmployeeDTO()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            var employees = new List<Employee> { employee };
            var readEmployeeDTOs = _mapper.Map<List<ReadEmployeeDTO>>(employees);

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(readEmployeeDTOs);
        }

        [Fact]
        public async Task InsertEmployeeAsync_WhenItIsSaved_ReturnsReadEmployeeDTO()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            var readEmployeeDTO = _mapper.Map<ReadEmployeeDTO>(employee);
            var insertEmployeeDTO = _mapper.Map<InsertEmployeeDTO>(employee);

            var request = new IsUserExistingRequest { UserId = employee.Id };
            var reply = new IsUserExistingReply
            {
                IsUserExisting = true,
                UserName = employee.Name
            };

            var isInserted = true;

            _mapperMock.MockMap(insertEmployeeDTO, employee);
            _mapperMock.MockMap(employee, employee);

            _baseRepositoryMock.MockInsertAsync(employee)
                               .MockSaveChangesAsync(isInserted);

            _cacheAccessorMock.MockGetByResourceIdAsync(employee.Id.ToString(), readEmployeeDTO);

            _grpcClientMock.MockIsUserExisting(request, reply);

            //Act
            var result = await _employeeService.InsertAsync<ReadEmployeeDTO>(insertEmployeeDTO,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readEmployeeDTO);

            _mapperMock.Verify();
            _baseRepositoryMock.Verify();
            _cacheAccessorMock.Verify();

            _grpcClientMock.Verify(grpcClient => grpcClient.IsUserExisting(request,
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task InsertEmployeeAsync_WhenItIsNotSaved_ThrowsDbOperationException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            var readEmployeeDTO = _mapper.Map<ReadEmployeeDTO>(employee);
            var insertEmployeeDTO = _mapper.Map<InsertEmployeeDTO>(employee);

            var request = new IsUserExistingRequest { UserId = employee.Id };
            var reply = new IsUserExistingReply
            {
                IsUserExisting = true,
                UserName = employee.Name
            };

            var isInserted = false;

            _mapperMock.MockMap(insertEmployeeDTO, employee);
            _mapperMock.MockMap(employee, employee);

            _baseRepositoryMock.MockInsertAsync(employee)
                               .MockSaveChangesAsync(isInserted);

            _grpcClientMock.MockIsUserExisting(request, reply);

            //Act
            var result = _employeeService.InsertAsync<ReadEmployeeDTO>(insertEmployeeDTO,
                It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<DbOperationException>(() => result);

            _mapperMock.Verify();
            _baseRepositoryMock.Verify();

            _grpcClientMock.Verify(grpcClient => grpcClient.IsUserExisting(request,
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task InsertEmployeeAsync_WhenUserIsNotExisting_ThrowsNotFoundException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            var insertEmployeeDTO = _mapper.Map<InsertEmployeeDTO>(employee);

            var request = new IsUserExistingRequest { UserId = employee.Id };
            var reply = new IsUserExistingReply
            {
                IsUserExisting = false,
            };

            _grpcClientMock.MockIsUserExisting(request, reply);

            //Act
            var result = _employeeService.InsertAsync<ReadEmployeeDTO>(insertEmployeeDTO,
                It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _grpcClientMock.Verify(grpcClient => grpcClient.IsUserExisting(request,
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task DeleteEmployeeAsync_WhenItIsSaved_ReturnsId()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();

            _cacheAccessorMock.MockDeleteResourceByIdAsync(employee.Id.ToString());

            //Act
            await base.DeleteAsync_WhenItIsSaved_ReturnsId(employee);

            //Assert
            _cacheAccessorMock.Verify();
        }

        [Fact]
        public async Task DeleteEmployeeAsync_WhenItIsNotSaved_ThrowsDbOperationException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();

            _cacheAccessorMock.MockDeleteResourceByIdAsync(employee.Id.ToString());

            //Act
            await base.DeleteAsync_WhenItIsNotSaved_ThrowsDbOperationException(employee);

            //Assert
            _cacheAccessorMock.Verify();
        }

        [Fact]
        public async Task UpdateEmployee_WhenItIsSaved_ReturnsReadEmployeeDTO()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            var readEmployeeDTO = _mapper.Map<ReadEmployeeDTO>(employee);

            _cacheAccessorMock.MockDeleteResourceByIdAsync(employee.Id.ToString());

            //Act
            await base.Update_WhenItIsSaved_ReturnsEntity(employee, employee, readEmployeeDTO);

            //Assert
            _cacheAccessorMock.Verify();
        }

        [Fact]
        public async Task UpdateEmployee_WhenItIsNotSaved_ThrowsDbOperationException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            var readEmployeeDTO = _mapper.Map<ReadEmployeeDTO>(employee);

            _cacheAccessorMock.MockDeleteResourceByIdAsync(employee.Id.ToString());

            //Act
            await base.Update_WhenItIsNotSaved_ThrowsDbOperationException(employee, employee, readEmployeeDTO);

            //Assert
            _cacheAccessorMock.Verify();
        }

        [Fact]
        public async Task UpdateEmployee_WhenItIsNotFound_ThrowsNotFoundException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();

            _cacheAccessorMock.MockDeleteResourceByIdAsync(employee.Id.ToString());

            //Act
            await base.Update_WhenItIsNotFound_ThrowsNotFoundException<Employee, ReadEmployeeDTO>(
                employee, employee.Id);

            //Assert
            _cacheAccessorMock.Verify();
        }


        [Fact]
        public async Task GetAllEmployeedByRestaurantIdAsync_ReturnsReadEmployeeDTOs()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            var employees = new List<Employee> { employee };
            var readEmployeeDTOs = _mapper.Map<List<ReadEmployeeDTO>>(employees);

            _employeeRepositoryMock.MockGetAllByRestaurantIdAsync(employee.RestaurantId, readEmployeeDTOs);

            //Act
            var result = await _employeeService.GetAllByRestaurantIdAsync<ReadEmployeeDTO>(
                employee.RestaurantId, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readEmployeeDTOs);

            _employeeRepositoryMock.Verify();
        }
    }
}
