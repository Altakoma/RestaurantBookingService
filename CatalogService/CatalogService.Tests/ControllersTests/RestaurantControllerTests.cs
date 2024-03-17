using AutoMapper;
using CatalogService.Application.DTOs.Restaurant;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.MappingProfiles;
using CatalogService.Domain.Entities;
using CatalogService.Presentation.Controllers;
using CatalogService.Tests.Fakers;
using CatalogService.Tests.Mocks.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CatalogService.Tests.ControllersTests
{
    public class RestaurantControllerTests
    {
        private readonly RestaurantServiceMock _restaurantServiceMock;
        private readonly Mock<IMenuService> _menuServiceMock;
        private readonly Mock<IEmployeeService> _employeeServiceMock;

        private readonly IMapper _mapper;
        private readonly RestaurantController _restaurantController;

        public RestaurantControllerTests()
        {
            _restaurantServiceMock = new();
            _menuServiceMock = new();
            _employeeServiceMock = new();

            var profiles = new List<Profile>
            {
                new RestaurantProfile(),
                new RestaurantProfile()
            };

            _mapper = new Mapper(new MapperConfiguration(configure =>
                configure.AddProfiles(profiles)));

            _restaurantController = new RestaurantController(_restaurantServiceMock.Object,
                _menuServiceMock.Object, _employeeServiceMock.Object);
        }

        [Fact]
        public async Task GetAllRestaurantsAsync_ReturnsReadRestaurantDTOs()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();
            var restaurants = new List<Restaurant> { restaurant };
            var readRestaurantDTOs = _mapper.Map<List<ReadRestaurantDTO>>(restaurants);

            _restaurantServiceMock.MockGetAllAsync(readRestaurantDTOs);

            //Act
            var result = await _restaurantController.GetAllRestaurantsAsync(It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(readRestaurantDTOs);
        }

        [Fact]
        public async Task GetRestaurantAsync_ReturnsReadRestaurantDTO()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();
            var readRestaurantDTO = _mapper.Map<ReadRestaurantDTO>(restaurant);

            _restaurantServiceMock.MockGetItemAsync(readRestaurantDTO);

            //Act
            var result = await _restaurantController.GetRestaurantAsync(restaurant.Id,
                It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(readRestaurantDTO);
        }

        [Fact]
        public async Task InsertRestaurantAsync_ReturnsReadRestaurantDTO()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();
            var insertRestaurantDTO = _mapper.Map<InsertRestaurantDTO>(restaurant);
            var readRestaurantDTO = _mapper.Map<ReadRestaurantDTO>(restaurant);

            _restaurantServiceMock.MockInsertItemAsync(insertRestaurantDTO,
                readRestaurantDTO);

            //Act
            var result = await _restaurantController.InsertRestaurantAsync(insertRestaurantDTO,
                It.IsAny<CancellationToken>());
            var createdAtActionResult = result as CreatedAtActionResult;

            //Assert
            result.Should().BeOfType(typeof(CreatedAtActionResult));

            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult!.Value.Should().BeEquivalentTo(readRestaurantDTO);
        }

        [Fact]
        public async Task DeleteRestaurantAsync_ReturnsNoContent()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();

            _restaurantServiceMock.MockDeleteItemAsync(restaurant.Id);

            //Act
            var result = await _restaurantController.DeleteRestaurantAsync(restaurant.Id,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeOfType(typeof(NoContentResult));
        }
    }
}
