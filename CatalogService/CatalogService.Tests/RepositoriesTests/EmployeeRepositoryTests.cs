using AutoMapper;
using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.MappingProfiles;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Infrastructure.Data.Repositories;
using CatalogService.Tests.Fakers;
using CatalogService.Tests.RepositoriesTests.Base;
using Moq;

namespace CatalogService.Tests.RepositoriesTests
{
    public class EmployeeRepositoryTests : BaseRepositoryTests<Employee>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeRepositoryTests() : base()
        {
            _employeeRepository = new EmployeeRepository(_catalogServiceDbContextMock.Object,
                _mapperMock.Object);

            _mapper = new Mapper(new MapperConfiguration(
                configure => configure.AddProfile(new EmployeeProfile())));

            _repository = _employeeRepository;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsReadEmployeeDTOs()
        {
            //Arrange
            var employees = new List<Employee> { EmployeeDataFaker.GetFakedEmployee() };
            IQueryable<Employee> employeeQuery = employees.AsQueryable();

            var employeeReadDTOs = _mapper.Map<List<ReadEmployeeDTO>>(employees);

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(employeeQuery, employeeReadDTOs);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsEmployees()
        {
            //Arrange
            var employees = new List<Employee> { EmployeeDataFaker.GetFakedEmployee() };
            IQueryable<Employee> employeeQuery = employees.AsQueryable();

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(employeeQuery, employees);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsEmployee()
        {
            //Arrange
            var employee = EmployeeDataFaker.GetFakedEmployee();
            IQueryable<Employee> employeeQuery = new List<Employee> { employee }.AsQueryable();

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(employee.Id, employeeQuery, employee);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsReadEmployeeDTO()
        {
            //Arrange
            var employee = EmployeeDataFaker.GetFakedEmployee();
            IQueryable<Employee> employeeQuery = new List<Employee> { employee }.AsQueryable();

            var readEmployeeDTO = _mapper.Map<ReadEmployeeDTO>(employee);

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(employee.Id, employeeQuery, readEmployeeDTO);
        }

        [Fact]
        public async Task InsertEmployeeAsync_SuccessfullyExecuted()
        {
            //Arrange
            var employee = EmployeeDataFaker.GetFakedEmployee();

            //Act
            //Assert
            await base.InsertAsync_SuccessfullyExecuted(employee);
        }

        [Fact]
        public void UpdateEmployee_SuccessfullyExecuted()
        {
            //Arrange
            var employee = EmployeeDataFaker.GetFakedEmployee();

            //Act
            //Assert
            base.UpdateEntity_SuccessfullyExecuted(employee);
        }

        [Fact]
        public void DeleteEmployee_SuccessfullyExecuted()
        {
            //Arrange
            var employee = EmployeeDataFaker.GetFakedEmployee();

            //Act
            //Assert
            base.DeleteEntity_SuccessfullyExecuted(employee);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_WhenEmployeeIsExisting_SuccessfullyExecuted()
        {
            //Arrange
            var employee = EmployeeDataFaker.GetFakedEmployee();
            ICollection<Employee> employees = new List<Employee> { employee };

            IQueryable<Employee> query = employees.AsQueryable();

            _catalogServiceDbContextMock.MockDataSet(query);

            //Act
            await _employeeRepository.DeleteAsync(employee.Id, It.IsAny<CancellationToken>());

            //Assert
            _catalogServiceDbContextMock.Verify();
        }

        [Fact]
        public async Task DeleteEmployeeAsync_WhenEmployeeIsNotExisting_ThrowsNotFoundException()
        {
            //Arrange
            var employee = EmployeeDataFaker.GetFakedEmployee();
            int id = employee.Id;
            employee.Id = 0;

            IQueryable<Employee> query = new List<Employee> { employee }.AsQueryable();

            _catalogServiceDbContextMock.MockDataSet(query);

            //Act
            var result = _employeeRepository.DeleteAsync(id, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _catalogServiceDbContextMock.Verify();
        }
    }
}
