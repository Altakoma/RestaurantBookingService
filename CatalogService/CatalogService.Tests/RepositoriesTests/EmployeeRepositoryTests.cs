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

        public EmployeeRepositoryTests() : base(typeof(EmployeeRepository))
        {
            _employeeRepository = new EmployeeRepository(_catalogServiceDbContextMock.Object,
                _mapperMock.Object);

            _mapper = new Mapper(new MapperConfiguration(
                configure => configure.AddProfile(new EmployeeProfile())));
        }

        [Fact]
        public async Task GetAllAsync_ReturnsReadEmployeeDTOs()
        {
            //Arrange
            var employess = new List<Employee> { EmployeeDataFaker.GetFakedEmployee() };
            IQueryable<Employee> menuQuery = employess.AsQueryable();

            var menuReadDTOs = _mapper.Map<List<ReadEmployeeDTO>>(employess);

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(menuQuery, menuReadDTOs);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsEmployees()
        {
            //Arrange
            var employess = new List<Employee> { EmployeeDataFaker.GetFakedEmployee() };
            IQueryable<Employee> menuQuery = employess.AsQueryable();

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(menuQuery, employess);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsEmployee()
        {
            //Arrange
            var menu = EmployeeDataFaker.GetFakedEmployee();
            IQueryable<Employee> menuQuery = new List<Employee> { menu }.AsQueryable();

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(menu.Id, menuQuery, menu);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsReadEmployeeDTO()
        {
            //Arrange
            var menu = EmployeeDataFaker.GetFakedEmployee();
            IQueryable<Employee> menuQuery = new List<Employee> { menu }.AsQueryable();

            var readEmployeeDTO = _mapper.Map<ReadEmployeeDTO>(menu);

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(menu.Id, menuQuery, readEmployeeDTO);
        }

        [Fact]
        public async Task InsertEmployeeAsync_SuccessfullyExecuted()
        {
            //Arrange
            var menu = EmployeeDataFaker.GetFakedEmployee();

            //Act
            //Assert
            await base.InsertAsync_SuccessfullyExecuted(menu);
        }

        [Fact]
        public void UpdateEmployee_SuccessfullyExecuted()
        {
            //Arrange
            var menu = EmployeeDataFaker.GetFakedEmployee();

            //Act
            //Assert
            base.UpdateEntity_SuccessfullyExecuted(menu);
        }

        [Fact]
        public void DeleteEmployee_SuccessfullyExecuted()
        {
            //Arrange
            var menu = EmployeeDataFaker.GetFakedEmployee();

            //Act
            //Assert
            base.DeleteEntity_SuccessfullyExecuted(menu);
        }

        [Fact]
        public async Task DeleteEmployeeAsync_WhenEmployeeIsExisting_SuccessfullyExecuted()
        {
            //Arrange
            var menu = EmployeeDataFaker.GetFakedEmployee();
            ICollection<Employee> employess = new List<Employee> { menu };

            IQueryable<Employee> query = employess.AsQueryable();

            _catalogServiceDbContextMock.MockDataSet(query);

            //Act
            await _employeeRepository.DeleteAsync(menu.Id, It.IsAny<CancellationToken>());

            //Assert
            _catalogServiceDbContextMock.Verify();
        }

        [Fact]
        public async Task DeleteEmployeeAsync_WhenEmployeeIsNotExisting_ThrowsNotFoundException()
        {
            //Arrange
            var menu = EmployeeDataFaker.GetFakedEmployee();
            int id = menu.Id;
            menu.Id = 0;

            IQueryable<Employee> query = new List<Employee> { menu }.AsQueryable();

            _catalogServiceDbContextMock.MockDataSet(query);

            //Act
            var result = _employeeRepository.DeleteAsync(id, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _catalogServiceDbContextMock.Verify();
        }
    }
}
