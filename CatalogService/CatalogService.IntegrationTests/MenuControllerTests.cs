using CatalogService.Application.DTOs.Menu;
using CatalogService.Application.DTOs.Menu.Messages;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using CatalogService.Presentation.Controllers;
using CatalogService.Tests.Fakers;
using CatalogService.Tests.Mocks.MessageProducers;
using CatalogService.Tests.Mocks.TokenParsers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.IntegrationTests
{
    public class MenuControllerTests : IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly IServiceScope _serviceScope;
        private readonly MenuController _menuController;

        private readonly CancellationTokenSource _cancellationTokenSource;

        private readonly CatalogServiceDbContext _catalogDbContext;

        private readonly MenuMessageProducerMock _menuMessageProducerMock;
        private readonly TokenParserMock _tokenParserMock;

        public MenuControllerTests(IntegrationTestWebAppFactory factory)
        {
            _serviceScope = factory.Services.CreateScope();

            _menuController = _serviceScope.ServiceProvider
                .GetRequiredService<MenuController>();

            _catalogDbContext = _serviceScope.ServiceProvider
                .GetRequiredService<CatalogServiceDbContext>();

            _menuMessageProducerMock = factory.MenuMessageProducerMock;
            _tokenParserMock = factory.TokenParserMock;

            _cancellationTokenSource = new CancellationTokenSource();
        }

        [Fact]
        public async Task GetAllFoodAsync_ReturnsReadMenuDTOs()
        {
            //Arrange
            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _menuController.GetAllFoodAsync(cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult?.Value.Should().BeOfType<List<ReadMenuDTO>>();
        }

        [Fact]
        public async Task GetFoodAsync_ReturnsReadMenuDTO()
        {
            //Arrange
            Menu menu = MenuDataFaker.GetFakedMenuForInsert();

            _catalogDbContext.Menu.Add(menu);
            _catalogDbContext.SaveChanges();

            _catalogDbContext.ChangeTracker.Clear();

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _menuController.GetFoodAsync(menu.Id,
                cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult?.Value.Should().BeEquivalentTo(menu, options =>
                options.ExcludingNestedObjects().ExcludingMissingMembers());
        }

        [Fact]
        public async Task InsertFoodAsync_ReturnsReadMenuDTO()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurantForInsert();
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodTypeForInsert();

            _catalogDbContext.Restaurants.Add(restaurant);
            _catalogDbContext.FoodTypes.Add(foodType);
            _catalogDbContext.SaveChanges();

            int employeeId = GetFakeEmployeeId(restaurant.Id);

            InsertMenuDTO insertMenuDTO = MenuDataFaker
                .GetFakedInsertMenuDTO(restaurant.Id, foodType.Id);

            var cancellationToken = _cancellationTokenSource.Token;

            _tokenParserMock.MockParseSubjectId(employeeId);
            _menuMessageProducerMock.MockProduceMessageAsync<InsertMenuMessageDTO>();

            //Act
            var result = await _menuController.InsertFoodAsync(insertMenuDTO,
                cancellationToken);

            var createdAtActionResult = result as CreatedAtActionResult;

            //Assert
            createdAtActionResult.Should().NotBeNull();

            createdAtActionResult?.Value.Should().BeEquivalentTo(insertMenuDTO,
                options => options.ExcludingNestedObjects().ExcludingMissingMembers());

            _tokenParserMock.Verify();
            _menuMessageProducerMock.Verify();
        }

        [Fact]
        public async Task DeleteFoodAsync_ReturnsNoActionResult()
        {
            //Arrange
            Menu menu = MenuDataFaker.GetFakedMenuForInsert();

            _catalogDbContext.Menu.Add(menu);
            _catalogDbContext.SaveChanges();

            int employeeId = GetFakeEmployeeId(menu.RestaurantId);

            _catalogDbContext.ChangeTracker.Clear();

            _tokenParserMock.MockParseSubjectId(employeeId);

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _menuController.DeleteFoodAsync(menu.Id,
                cancellationToken);

            var noContentResult = result as NoContentResult;

            Menu? resultMenu = _catalogDbContext.Menu
                .FirstOrDefault(currentMenu => menu.Id == currentMenu.Id);

            //Assert
            noContentResult.Should().NotBeNull();

            resultMenu.Should().BeNull();

            _tokenParserMock.Verify();
        }

        private int GetFakeEmployeeId(int restaurantId)
        {
            Employee employee = EmployeeDataFaker.GetFakedEmployeeForInsert();

            employee.RestaurantId = restaurantId;

            bool isExisting = _catalogDbContext.Employees.Any(currentEmployee =>
                                    currentEmployee.Id == employee.Id);

            if (!isExisting)
            {
                _catalogDbContext.Employees.Add(employee);
            }
            else
            {
                _catalogDbContext.Employees.Update(employee);
            }

            _catalogDbContext.SaveChanges();

            return employee.Id;
        }
    }
}
