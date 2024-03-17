using AutoMapper;
using BookingService.Application.DTOs.Restaurant;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.MappingProfiles;
using BookingService.Domain.Entities;
using BookingService.Infrastructure.Data.Repositories;
using BookingService.Tests.Fakers;
using BookingService.Tests.RepositoryTests.Base;

namespace BookingService.Tests.RepositoryTests
{
    public class RestaurantRepositoryTests : BaseRepositoryTests<Restaurant>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public RestaurantRepositoryTests() : base()
        {
            _restaurantRepository = new RestaurantRepository(_dbContextMock.Object,
                _mapperMock.Object);

            _mapper = new Mapper(new MapperConfiguration(
                configure => configure.AddProfile(new RestaurantProfile())));

            _repository = _restaurantRepository;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsReadRestaurantDTOs()
        {
            //Arrange
            var restaurants = new List<Restaurant> { RestaurantDataFaker.GetFakedRestaurant() };
            IQueryable<Restaurant> restaurantQuery = restaurants.AsQueryable();

            var restaurantReadDTOs = _mapper.Map<List<ReadRestaurantDTO>>(restaurants);

            //Act
            //Assert
            await GetAllAsync_ReturnsEntities(restaurantQuery, restaurantReadDTOs);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsRestaurants()
        {
            //Arrange
            var restaurants = new List<Restaurant> { RestaurantDataFaker.GetFakedRestaurant() };
            IQueryable<Restaurant> restaurantQuery = restaurants.AsQueryable();

            //Act
            //Assert
            await GetAllAsync_ReturnsEntities(restaurantQuery, restaurants);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsReadRestaurantDTO()
        {
            //Arrange
            var restaurant = RestaurantDataFaker.GetFakedRestaurant();
            IQueryable<Restaurant> restaurantQuery = new List<Restaurant> { restaurant }.AsQueryable();

            var readRestaurantDTO = _mapper.Map<ReadRestaurantDTO>(restaurant);

            //Act
            //Assert
            await GetByIdAsync_ReturnsEntity(restaurant.Id, restaurantQuery, readRestaurantDTO);
        }

        [Fact]
        public async Task GetByIdAsync_WhenItemIsNotExisting_ReturnsReadRestaurantDTO()
        {
            //Arrange
            var restaurant = RestaurantDataFaker.GetFakedRestaurant();

            //Act
            //Assert
            await GetByIdAsync_WhenItemIsNotExisting_ReturnsEntity<ReadRestaurantDTO>(restaurant.Id);
        }

        [Fact]
        public async Task InsertRestaurantAsync_ReturnsReadRestaurantDTO()
        {
            //Arrange
            var restaurant = RestaurantDataFaker.GetFakedRestaurant();
            var readRestaurantDTO = _mapper.Map<ReadRestaurantDTO>(restaurant);

            //Act
            //Assert
            await InsertAsync_ReturnsEntity(restaurant, readRestaurantDTO);
        }

        [Fact]
        public async Task InsertRestaurantAsync_WhenItIsNotFound_ThrowsNotFoundException()
        {
            //Arrange
            var restaurant = RestaurantDataFaker.GetFakedRestaurant();

            //Act
            //Assert
            await InsertAsync_WhenItIsNotFound_ThrowsNotFoundException<ReadRestaurantDTO>(restaurant);
        }

        [Fact]
        public async Task UpdateRestaurant__ReturnsReadRestaurantDTO()
        {
            //Arrange
            var restaurant = RestaurantDataFaker.GetFakedRestaurant();
            var readRestaurantDTO = _mapper.Map<ReadRestaurantDTO>(restaurant);

            //Act
            //Assert
            await UpdateEntity_ReturnsEntity(restaurant, readRestaurantDTO);
        }

        [Fact]
        public async Task UpdateRestaurant__WhenItIsNotFound_ThrowsNotFoundException()
        {
            //Arrange
            var restaurant = RestaurantDataFaker.GetFakedRestaurant();

            //Act
            //Assert
            await UpdateEntity__WhenItIsNotFound_ThrowsNotFoundException<ReadRestaurantDTO>(restaurant);
        }

        [Fact]
        public async Task DeleteRestaurant_WhenEntityIsExisting_SuccessfullyExecuted()
        {
            //Arrange
            var restaurant = RestaurantDataFaker.GetFakedRestaurant();

            var restaurantQuery = new List<Restaurant> { restaurant }.AsQueryable();

            //Act
            //Assert
            await DeleteEntityAsync_WhenEntityIsExisting_SuccessfullyExecuted(
                restaurantQuery, restaurant.Id);
        }

        [Fact]
        public async Task DeleteRestaurantAsync_WhenRestaurantIsNotExisting_ThrowsNotFoundException()
        {
            //Arrange
            var restaurant = RestaurantDataFaker.GetFakedRestaurant();

            var restaurantQuery = new List<Restaurant>().AsQueryable();

            //Act
            //Assert
            await DeleteEntityAsync_WhenEntityIsNotExisting_ThrowsNotFoundException(
                restaurantQuery, restaurant.Id);
        }
    }
}
