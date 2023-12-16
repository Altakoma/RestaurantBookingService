using AutoMapper;
using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.DTOs.FoodType;
using CatalogService.Application.Interfaces.GrpcServices;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using CatalogService.Presentation.Controllers;
using CatalogService.Tests.Fakers;
using CatalogService.Tests.Mocks.GrpcClients;
using CatalogService.Tests.Mocks.TokenParsers;
using Docker.DotNet.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace CatalogService.IntegrationTests
{
    public class FoodTypeControllerTests : IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly IServiceScope _serviceScope;
        private readonly FoodTypeController _foodTypeController;

        private readonly CancellationTokenSource _cancellationTokenSource;

        private readonly CatalogServiceDbContext _catalogDbContext;
        private readonly TokenParserMock _tokenParserMock;

        public FoodTypeControllerTests(IntegrationTestWebAppFactory factory)
        {
            _serviceScope = factory.Services.CreateScope();

            _tokenParserMock = factory.TokenParserMock;

            _foodTypeController = _serviceScope.ServiceProvider
                .GetRequiredService<FoodTypeController>();

            _catalogDbContext = _serviceScope.ServiceProvider
                .GetRequiredService<CatalogServiceDbContext>();

            _cancellationTokenSource = new CancellationTokenSource();
        }

        [Fact]
        public async Task GetAllFoodTypesAsync_ReturnsReadFoodTypeDTO()
        {
            //Arrange
            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _foodTypeController.GetAllFoodTypesAsync(cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult?.Value.Should().BeOfType<List<ReadFoodTypeDTO>>();
        }

        [Fact]
        public async Task GetFoodTypeAsync_ReturnsReadFoodTypeDTO()
        {
            //Arrange
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodTypeForInsert();

            _catalogDbContext.FoodTypes.Add(foodType);
            _catalogDbContext.SaveChanges();

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _foodTypeController.GetFoodTypeAsync(foodType.Id,
                cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult?.Value.Should().BeOfType<ReadFoodTypeDTO>();
            okResult?.Value.Should().BeEquivalentTo(foodType, options =>
                options.ExcludingNestedObjects().ExcludingMissingMembers());
        }

        [Fact]
        public async Task InsertFoodTypeAsync_ReturnsReadFoodTypeDTO()
        {
            //Arrange
            FoodTypeDTO foodTypeDTO = FoodTypeDataFaker.GetFakedFoodTypeDTO();

            int employeeId = GetFakeEmployeeId();

            _tokenParserMock.MockParseSubjectId(employeeId);

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _foodTypeController.InsertFoodTypeAsync(foodTypeDTO,
                cancellationToken);

            var createdAtActionResult = result as CreatedAtActionResult;

            FoodType? resultFoodType = _catalogDbContext.FoodTypes
                .OrderBy(foodType => foodType.Id).LastOrDefault();

            //Assert
            createdAtActionResult.Should().NotBeNull();

            foodTypeDTO.Should().BeEquivalentTo(resultFoodType, options =>
                options.ExcludingMissingMembers().ExcludingNestedObjects());
        }

        [Fact]
        public async Task UpdateFoodTypeAsync_ReturnsReadFoodTypeDTO()
        {
            //Arrange
            int employeeId = GetFakeEmployeeId();

            _tokenParserMock.MockParseSubjectId(employeeId);

            FoodType foodType = FoodTypeDataFaker.GetFakedFoodTypeForInsert();

            _catalogDbContext.FoodTypes.Add(foodType);
            _catalogDbContext.SaveChanges();

            _catalogDbContext.ChangeTracker.Clear();

            FoodTypeDTO updatingFoodType = FoodTypeDataFaker.GetFakedFoodTypeDTO();

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _foodTypeController.UpdateFoodTypeAsync(foodType.Id,
                updatingFoodType, cancellationToken);

            var okResult = result as OkObjectResult;

            FoodType? resultFoodType = _catalogDbContext.FoodTypes
                .OrderBy(foodType => foodType.Id).LastOrDefault();

            //Assert
            okResult.Should().NotBeNull();

            okResult?.Value.Should().BeOfType<ReadFoodTypeDTO>();
            okResult?.Value.Should().BeEquivalentTo(resultFoodType, options =>
                options.ExcludingMissingMembers().ExcludingNestedObjects());
            foodType.Should().NotBeEquivalentTo(resultFoodType, options =>
                options.ExcludingMissingMembers().ExcludingNestedObjects());
        }

        [Fact]
        public async Task DeleteFoodTypeAsync_ReturnsNoContentResult()
        {
            //Arrange
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodTypeForInsert();

            _catalogDbContext.FoodTypes.Add(foodType);
            _catalogDbContext.SaveChanges();

            int employeeId = GetFakeEmployeeId();

            _catalogDbContext.ChangeTracker.Clear();

            _tokenParserMock.MockParseSubjectId(employeeId);

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _foodTypeController.DeleteFoodTypeAsync(foodType.Id,
                cancellationToken);

            var noContentResult = result as NoContentResult;

            FoodType? resultFoodType = _catalogDbContext.FoodTypes
                .FirstOrDefault(currentFoodType => foodType.Id == currentFoodType.Id);

            //Assert
            noContentResult.Should().NotBeNull();

            resultFoodType.Should().BeNull();
        }

        private int GetFakeEmployeeId()
        {
            Employee employee = EmployeeDataFaker.GetFakedEmployeeForInsert();

            bool isExisting = _catalogDbContext.Employees.Any(currentEmployee =>
                                    currentEmployee.Id == employee.Id);

            if (!isExisting)
            {
                _catalogDbContext.Employees.Add(employee);
                _catalogDbContext.SaveChanges();
            }

            return employee.Id;
        }
    }
}
