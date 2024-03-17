using AutoMapper;
using BookingService.Application.DTOs.Restaurant;
using BookingService.Application.MappingProfiles;
using BookingService.Domain.Entities;
using BookingService.Presentation.Controllers;
using BookingService.Tests.Fakers;
using BookingService.Tests.Mocks.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BookingService.Tests.ControllerTests
{
    public class RestaurantControllerTests
    {
        private readonly RestaurantServiceMock _restaurantServiceMock;

        private readonly IMapper _mapper;
        private readonly RestaurantController _restaurantController;

        public RestaurantControllerTests()
        {
            _restaurantServiceMock = new();

            _mapper = new Mapper(new MapperConfiguration(configure =>
                configure.AddProfile(new RestaurantProfile())));

            _restaurantController = new RestaurantController(_restaurantServiceMock.Object);
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
    }
}
