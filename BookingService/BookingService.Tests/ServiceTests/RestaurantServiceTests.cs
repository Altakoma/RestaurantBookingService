using AutoMapper;
using BookingService.Application.DTOs.Restaurant;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Interfaces.Services;
using BookingService.Application.MappingProfiles;
using BookingService.Application.Services;
using BookingService.Domain.Entities;
using BookingService.Tests.Fakers;
using BookingService.Tests.Mocks.Repositories;
using BookingService.Tests.ServiceTests.Base;

namespace BookingService.Tests.ServiceTests
{
    public class RestaurantServiceTests : BaseServiceTests<IRestaurantRepository, Restaurant>
    {
        private readonly RestaurantRepositoryMock _restaurantRepositoryMock;

        private readonly IRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        public RestaurantServiceTests() : base()
        {
            _restaurantRepositoryMock = new();

            _mapper = new Mapper(new MapperConfiguration(configure =>
            configure.AddProfile(new RestaurantProfile())));

            _restaurantService = new Application.Services.RestaurantService(_restaurantRepositoryMock.Object,
                _mapperMock.Object);

            _baseRepositoryMock = _restaurantRepositoryMock;
            _baseService = _restaurantService;
        }

        [Fact]
        public async Task GetRestaurantByIdAsync_ReturnsReadRestaurantDTO()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();
            var readRestaurantDTO = _mapper.Map<ReadRestaurantDTO>(restaurant);

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(restaurant.Id, readRestaurantDTO);
        }

        [Fact]
        public async Task GetAllRestaurantsAsync_ReturnsReadRestaurantDTOs()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();
            var restaurants = new List<Restaurant> { restaurant };
            var readRestaurantDTOs = _mapper.Map<List<ReadRestaurantDTO>>(restaurants);

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(readRestaurantDTOs);
        }
    }
}
