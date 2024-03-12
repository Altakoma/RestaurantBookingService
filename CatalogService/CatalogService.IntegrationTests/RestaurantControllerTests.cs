using CatalogService.Application.DTOs.Restaurant;
using CatalogService.Application.DTOs.Restaurant.Messages;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using CatalogService.Presentation.Controllers;
using CatalogService.Tests.Fakers;
using CatalogService.Tests.Mocks.MessageProducers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.IntegrationTests
{
    public class RestaurantControllerTests : IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly IServiceScope _serviceScope;
        private readonly RestaurantController _restaurantController;

        private readonly CancellationTokenSource _cancellationTokenSource;

        private readonly CatalogServiceDbContext _catalogDbContext;

        private readonly RestaurantMessageProducerMock _restaurantMessageProducerMock;

        public RestaurantControllerTests(IntegrationTestWebAppFactory factory)
        {
            _serviceScope = factory.Services.CreateScope();
            _restaurantController = _serviceScope.ServiceProvider
            .GetRequiredService<RestaurantController>();

            _catalogDbContext = _serviceScope.ServiceProvider
                .GetRequiredService<CatalogServiceDbContext>();

            _restaurantMessageProducerMock = factory.RestaurantMessageProducerMock;

            _cancellationTokenSource = new CancellationTokenSource();
        }

        [Fact]
        public async Task GetAllRestaurantsAsync_ReturnsReadRestaurantDTOs()
        {
            //Arrange
            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _restaurantController.GetAllRestaurantsAsync(cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult?.Value.Should().BeOfType<List<ReadRestaurantDTO>>();
        }

        [Fact]
        public async Task GetRestaurantAsync_ReturnsReadRestaurantDTO()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurantForInsert();

            _catalogDbContext.Restaurants.Add(restaurant);
            _catalogDbContext.SaveChanges();

            _catalogDbContext.ChangeTracker.Clear();

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _restaurantController.GetRestaurantAsync(
                restaurant.Id, cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult?.Value.Should().BeEquivalentTo(restaurant, options =>
                options.ExcludingNestedObjects().ExcludingMissingMembers());
        }

        [Fact]
        public async Task InsertRestaurantAsync_ReturnsReadRestaurantDTO()
        {
            //Arrange
            InsertRestaurantDTO insertRestaurantDTO = RestaurantDataFaker
                .GetFakedInsertRestaurantDTO();

            var cancellationToken = _cancellationTokenSource.Token;

            _restaurantMessageProducerMock.MockProduceMessageAsync<InsertRestaurantMessageDTO>();

            //Act
            var result = await _restaurantController.InsertRestaurantAsync(
                insertRestaurantDTO, cancellationToken);

            var createdAtActionResult = result as CreatedAtActionResult;

            //Assert
            createdAtActionResult.Should().NotBeNull();

            createdAtActionResult?.Value.Should().BeEquivalentTo(insertRestaurantDTO,
                options => options.ExcludingNestedObjects().ExcludingMissingMembers());

            _restaurantMessageProducerMock.Verify();
        }

        [Fact]
        public async Task UpdateRestaurantAsync_ReturnsReadRestaurantDTO()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurantForInsert();

            _catalogDbContext.Restaurants.Add(restaurant);
            _catalogDbContext.SaveChanges();

            _catalogDbContext.ChangeTracker.Clear();

            UpdateRestaurantDTO updateRestaurantDTO = RestaurantDataFaker
                .GetFakedUpdateRestaurantDTO();

            var cancellationToken = _cancellationTokenSource.Token;

            _restaurantMessageProducerMock.MockProduceMessageAsync<UpdateRestaurantMessageDTO>();

            //Act
            var result = await _restaurantController.UpdateRestaurantAsync(restaurant.Id,
                updateRestaurantDTO, cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult?.Value.Should().BeEquivalentTo(updateRestaurantDTO,
                options => options.ExcludingNestedObjects().ExcludingMissingMembers());

            okResult?.Value.Should().NotBeEquivalentTo(restaurant,
                options => options.ExcludingNestedObjects().ExcludingMissingMembers());

            _restaurantMessageProducerMock.Verify();
        }

        [Fact]
        public async Task DeleteRestaurantAsync_ReturnsReadRestaurantDTO()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurantForInsert();

            _catalogDbContext.Restaurants.Add(restaurant);
            _catalogDbContext.SaveChanges();

            _catalogDbContext.ChangeTracker.Clear();

            var cancellationToken = _cancellationTokenSource.Token;

            _restaurantMessageProducerMock.MockProduceMessageAsync<DeleteRestaurantMessageDTO>();

            //Act
            var result = await _restaurantController.DeleteRestaurantAsync(
                restaurant.Id, cancellationToken);

            var noContentResult = result as NoContentResult;

            Restaurant? resultRestaurant = _catalogDbContext.Restaurants
                .FirstOrDefault(currentRestaurant => currentRestaurant.Id == restaurant.Id);

            //Assert
            noContentResult.Should().NotBeNull();

            resultRestaurant.Should().BeNull();

            _restaurantMessageProducerMock.Verify();
        }
    }
}
