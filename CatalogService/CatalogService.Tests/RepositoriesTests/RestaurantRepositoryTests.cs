using AutoMapper;
using CatalogService.Application.DTOs.Restaurant;
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
    public class RestaurantRepositoryTests : BaseRepositoryTests<Restaurant>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMapper _mapper;

        public RestaurantRepositoryTests() : base(typeof(RestaurantRepository))
        {
            _restaurantRepository = new RestaurantRepository(_catalogServiceDbContextMock.Object,
                _mapperMock.Object);

            _mapper = new Mapper(new MapperConfiguration(
                configure => configure.AddProfile(new RestaurantProfile())));
        }

        [Fact]
        public async Task GetAllAsync_ReturnsReadRestaurantDTOs()
        {
            //Arrange
            var restaurants = new List<Restaurant> { RestaurantDataFaker.GetFakedRestaurant() };
            IQueryable<Restaurant> menuQuery = restaurants.AsQueryable();

            var menuReadDTOs = _mapper.Map<List<ReadRestaurantDTO>>(restaurants);

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(menuQuery, menuReadDTOs);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsRestaurants()
        {
            //Arrange
            var restaurants = new List<Restaurant> { RestaurantDataFaker.GetFakedRestaurant() };
            IQueryable<Restaurant> menuQuery = restaurants.AsQueryable();

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(menuQuery, restaurants);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsRestaurant()
        {
            //Arrange
            var menu = RestaurantDataFaker.GetFakedRestaurant();
            IQueryable<Restaurant> menuQuery = new List<Restaurant> { menu }.AsQueryable();

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(menu.Id, menuQuery, menu);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsReadRestaurantDTO()
        {
            //Arrange
            var menu = RestaurantDataFaker.GetFakedRestaurant();
            IQueryable<Restaurant> menuQuery = new List<Restaurant> { menu }.AsQueryable();

            var readRestaurantDTO = _mapper.Map<ReadRestaurantDTO>(menu);

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(menu.Id, menuQuery, readRestaurantDTO);
        }

        [Fact]
        public async Task InsertRestaurantAsync_SuccessfullyExecuted()
        {
            //Arrange
            var menu = RestaurantDataFaker.GetFakedRestaurant();

            //Act
            //Assert
            await base.InsertAsync_SuccessfullyExecuted(menu);
        }

        [Fact]
        public void UpdateRestaurant_SuccessfullyExecuted()
        {
            //Arrange
            var menu = RestaurantDataFaker.GetFakedRestaurant();

            //Act
            //Assert
            base.UpdateEntity_SuccessfullyExecuted(menu);
        }

        [Fact]
        public void DeleteRestaurant_SuccessfullyExecuted()
        {
            //Arrange
            var menu = RestaurantDataFaker.GetFakedRestaurant();

            //Act
            //Assert
            base.DeleteEntity_SuccessfullyExecuted(menu);
        }

        [Fact]
        public async Task DeleteRestaurantAsync_WhenRestaurantIsExisting_SuccessfullyExecuted()
        {
            //Arrange
            var menu = RestaurantDataFaker.GetFakedRestaurant();
            ICollection<Restaurant> restaurants = new List<Restaurant> { menu };

            IQueryable<Restaurant> query = restaurants.AsQueryable();

            _catalogServiceDbContextMock.MockDataSet(query);

            //Act
            await _restaurantRepository.DeleteAsync(menu.Id, It.IsAny<CancellationToken>());

            //Assert
            _catalogServiceDbContextMock.Verify();
        }

        [Fact]
        public async Task DeleteRestaurantAsync_WhenRestaurantIsNotExisting_ThrowsNotFoundException()
        {
            //Arrange
            var menu = RestaurantDataFaker.GetFakedRestaurant();
            int id = menu.Id;
            menu.Id = 0;

            IQueryable<Restaurant> query = new List<Restaurant> { menu }.AsQueryable();

            _catalogServiceDbContextMock.MockDataSet(query);

            //Act
            var result = _restaurantRepository.DeleteAsync(id, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _catalogServiceDbContextMock.Verify();
        }
    }
}
