using AutoMapper;
using CatalogService.Application;
using CatalogService.Application.DTOs.Employee;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using CatalogService.Presentation.Controllers;
using CatalogService.Tests.Fakers;
using CatalogService.Tests.Mocks.GrpcClients;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CatalogService.IntegrationTests
{
    public class EmployeeControllerTests : IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly IServiceScope _serviceScope;
        private readonly EmployeeController _employeeController;

        private readonly CancellationTokenSource _cancellationTokenSource;

        private readonly CatalogServiceDbContext _catalogDbContext;

        private readonly GrpcEmployeeClientServiceMock _grpcEmployeeClientServiceMock;

        private readonly IMapper _mapper;

        public EmployeeControllerTests(IntegrationTestWebAppFactory factory)
        {
            _serviceScope = factory.Services.CreateScope();

            _employeeController = _serviceScope.ServiceProvider
                .GetRequiredService<EmployeeController>();

            _catalogDbContext = _serviceScope.ServiceProvider
                .GetRequiredService<CatalogServiceDbContext>();

            _mapper = _serviceScope.ServiceProvider
                .GetRequiredService<IMapper>();

            _grpcEmployeeClientServiceMock = factory.GrpcEmployeeClientServiceMock;

            _cancellationTokenSource = new CancellationTokenSource();
        }

        [Fact]
        public async Task GetAllEmployeesAsync_ReturnsReadEmployeeDTOs()
        {
            //Arrange
            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _employeeController.GetAllEmployeesAsync(cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult?.Value.Should().BeOfType<List<ReadEmployeeDTO>>();
        }

        [Fact]
        public async Task GetEmployeesAsync_ReturnsReadEmployeeDTOs()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurantForInsert();

            _catalogDbContext.Restaurants.Add(restaurant);
            _catalogDbContext.SaveChanges();

            Employee employee = EmployeeDataFaker.GetFakedEmployeeForInsert(restaurant.Id);

            _catalogDbContext.Employees.Add(employee);
            _catalogDbContext.SaveChanges();

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _employeeController.GetEmployeeAsync(employee.Id,
                cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult?.Value.Should().BeEquivalentTo(employee, options =>
                options.ExcludingNestedObjects().ExcludingMissingMembers());
        }

        [Fact]
        public async Task InsertEmployeeAsync_ReturnsReadEmployeeDTOs()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurantForInsert();

            _catalogDbContext.Restaurants.Add(restaurant);
            _catalogDbContext.SaveChanges();

            Employee employee = EmployeeDataFaker.GetFakedEmployeeForInsert(restaurant.Id);

            var insertEmployeeDTO = _mapper.Map<InsertEmployeeDTO>(employee);

            var request = new IsUserExistingRequest
            {
                UserId = employee.Id,
            };

            var reply = new IsUserExistingReply
            {
                IsUserExisting = true,
                UserName = employee.Name,
            };

            _grpcEmployeeClientServiceMock.Setup(grpcClient =>
                grpcClient.IsUserExisting(request, It.IsAny<CancellationToken>()))
                .ReturnsAsync(reply);

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _employeeController.InsertEmployeeAsync(insertEmployeeDTO,
                cancellationToken);

            var createdAtActionResult = result as CreatedAtActionResult;

            //Assert
            createdAtActionResult.Should().NotBeNull();

            createdAtActionResult?.Value.Should().BeEquivalentTo(employee, options =>
                options.ExcludingNestedObjects().ExcludingMissingMembers());

            _grpcEmployeeClientServiceMock.Verify();
        }

        [Fact]
        public async Task DeleteEmployeeAsync_ReturnsNoContent()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurantForInsert();

            _catalogDbContext.Restaurants.Add(restaurant);
            _catalogDbContext.SaveChanges();

            Employee employee = EmployeeDataFaker.GetFakedEmployeeForInsert();

            var isExisting = _catalogDbContext.Employees.Any(currentEmployee =>
                currentEmployee.Id == employee.Id);

            if (!isExisting)
            {
                employee.RestaurantId = restaurant.Id;
                _catalogDbContext.Employees.Add(employee);
                _catalogDbContext.SaveChanges();
            }

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _employeeController.DeleteEmployeeAsync(employee.Id,
                cancellationToken);

            var noContentResult = result as NoContentResult;

            Employee? resultEmployee = _catalogDbContext.Employees
                .FirstOrDefault(currentEmployee => employee.Id == currentEmployee.Id);

            //Assert
            noContentResult.Should().NotBeNull();

            resultEmployee.Should().BeNull();
        }
    }
}
