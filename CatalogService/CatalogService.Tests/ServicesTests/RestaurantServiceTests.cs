using AutoMapper;
using CatalogService.Application.DTOs.Restaurant;
using CatalogService.Application.DTOs.Restaurant.Messages;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.MappingProfiles;
using CatalogService.Application.Services;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces.Services;
using CatalogService.Tests.Fakers;
using CatalogService.Tests.Mocks.CacheAccessors;
using CatalogService.Tests.Mocks.MessageProducers;
using CatalogService.Tests.Mocks.Repositories;
using CatalogService.Tests.ServicesTests.Base;
using FluentAssertions;
using Moq;

namespace CatalogService.Tests.ServicesTests
{
    public class RestaurantServiceTests : BaseServiceTests<IRestaurantRepository, Restaurant>
    {
        private readonly RestaurantMessageProducerMock _restaurantMessageProducerMock;
        private readonly RestaurantCacheAccessorMock _restaurantCacheAccessorMock;

        private readonly RestaurantRepositoryMock _restaurantRepositoryMock;

        private readonly IBaseRestaurantService _restaurantService;
        private readonly IMapper _mapper;

        public RestaurantServiceTests() : base()
        {
            _restaurantCacheAccessorMock = new();
            _restaurantMessageProducerMock = new();
            _restaurantRepositoryMock = new();

            _mapper = new Mapper(new MapperConfiguration(configure =>
            configure.AddProfile(new RestaurantProfile())));

            _restaurantService = new RestaurantService(_restaurantRepositoryMock.Object,
                _restaurantMessageProducerMock.Object, _restaurantCacheAccessorMock.Object,
                _mapperMock.Object);

            _baseService = _restaurantService;
            _baseRepositoryMock = _restaurantRepositoryMock;
        }

        [Fact]
        public async Task GetRestaurantByIdAsync_WhenItIsExisting_ReturnsReadRestaurantDTO()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();
            var readRestaurantDTO = _mapper.Map<ReadRestaurantDTO>(restaurant);

            _restaurantCacheAccessorMock.MockGetByResourceIdAsync(restaurant.Id.ToString(), readRestaurantDTO);

            //Act
            var result = await _restaurantService.GetByIdAsync<ReadRestaurantDTO>(restaurant.Id,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readRestaurantDTO);

            _restaurantCacheAccessorMock.Verify();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsReadRestaurantDTO()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();
            var restaurants = new List<Restaurant> { restaurant };
            var readRestaurantDTOs = _mapper.Map<List<ReadRestaurantDTO>>(restaurants);

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(readRestaurantDTOs);
        }

        [Fact]
        public async Task DeleteRestaurantAsync_WhenItIsSaved_ReturnsId()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();
            var deleteMessageDTO = new DeleteRestaurantMessageDTO
            {
                Id = restaurant.Id,
            };

            _restaurantCacheAccessorMock.MockDeleteResourceByIdAsync(restaurant.Id.ToString());

            _restaurantMessageProducerMock.MockProduceMessageAsync(deleteMessageDTO);

            //Act
            await base.DeleteAsync_WhenItIsSaved_ReturnsId(restaurant);

            //Assert
            _restaurantCacheAccessorMock.Verify();
            _restaurantMessageProducerMock.Verify(producer => producer.ProduceMessageAsync(
                It.Is<DeleteRestaurantMessageDTO>(message => message.Id == restaurant.Id),
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task DeleteRestaurantAsync_WhenItIsNotSaved_ThrowsDbOperationException()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();

            _restaurantCacheAccessorMock.MockDeleteResourceByIdAsync(restaurant.Id.ToString());

            //Act
            await base.DeleteAsync_WhenItIsNotSaved_ThrowsDbOperationException(restaurant);

            //Assert
            _restaurantCacheAccessorMock.Verify();
        }

        [Fact]
        public async Task DeleteRestaurantAsync_WhenItIsNotExisting_ThrowsNotFoundException()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();

            _restaurantCacheAccessorMock.MockDeleteResourceByIdAsync(restaurant.Id.ToString());

            //Act
            await base.DeleteAsync_WhenItIsNotExisting_ThrowsNotFoundException(restaurant.Id);

            //Assert
            _restaurantCacheAccessorMock.Verify();
        }

        [Fact]
        public async Task InsertRestaurantAsync_WhenItIsSaved_ReturnsEntity()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();
            var readRestaurantDTO = _mapper.Map<ReadRestaurantDTO>(restaurant);
            var insertRestaurantDTO = _mapper.Map<InsertRestaurantDTO>(restaurant);
            var insertMessageDTO = _mapper.Map<InsertRestaurantMessageDTO>(readRestaurantDTO);

            _mapperMock.MockMap(readRestaurantDTO, insertMessageDTO);

            _restaurantMessageProducerMock.MockProduceMessageAsync(insertMessageDTO);

            _restaurantCacheAccessorMock.MockGetByResourceIdAsync(restaurant.Id.ToString(), readRestaurantDTO);

            //Act
            await base.InsertAsync_WhenItIsSaved_ReturnsEntity(insertRestaurantDTO,
                restaurant, readRestaurantDTO);

            //Assert
            _mapperMock.Verify();
            _restaurantMessageProducerMock.Verify(producer => producer.ProduceMessageAsync(
                It.Is<InsertRestaurantMessageDTO>(message => message.Id == restaurant.Id),
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task InsertRestaurantAsync_WhenItIsNotSaved_ThrowsDbOperationException()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();
            var insertRestaurantDTO = _mapper.Map<InsertRestaurantDTO>(restaurant);

            //Act
            await base.InsertAsync_WhenItIsNotSaved_ThrowsDbOperationException<InsertRestaurantDTO, ReadRestaurantDTO>(
                insertRestaurantDTO, restaurant);

            //Assert
            _restaurantCacheAccessorMock.Verify();
        }

        [Fact]
        public async Task UpdateRestaurantAsync_WhenItIsSaved_ReturnsEntity()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();
            var readRestaurantDTO = _mapper.Map<ReadRestaurantDTO>(restaurant);
            var updateRestaurantDTO = _mapper.Map<UpdateRestaurantDTO>(restaurant);
            var updateMessageDTO = _mapper.Map<UpdateRestaurantMessageDTO>(readRestaurantDTO);

            _mapperMock.MockMap(readRestaurantDTO, updateMessageDTO);

            _restaurantCacheAccessorMock.MockDeleteResourceByIdAsync(restaurant.Id.ToString());

            _restaurantMessageProducerMock.MockProduceMessageAsync(updateMessageDTO);

            //Act
            await base.Update_WhenItIsSaved_ReturnsEntity(updateRestaurantDTO,
                restaurant, readRestaurantDTO);

            //Assert
            _mapperMock.Verify();
            _restaurantCacheAccessorMock.Verify();
            _restaurantMessageProducerMock.Verify(producer => producer.ProduceMessageAsync(
                It.Is<UpdateRestaurantMessageDTO>(message => message.Id == restaurant.Id),
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task UpdateRestaurantAsync_WhenItIsNotSaved_ThrowsDbOperationException()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();
            var readRestaurantDTO = _mapper.Map<ReadRestaurantDTO>(restaurant);
            var updateRestaurantDTO = _mapper.Map<UpdateRestaurantDTO>(restaurant);

            _restaurantCacheAccessorMock.MockDeleteResourceByIdAsync(restaurant.Id.ToString());

            //Act
            await base.Update_WhenItIsNotSaved_ThrowsDbOperationException(updateRestaurantDTO,
                restaurant, readRestaurantDTO);

            //Assert
            _restaurantCacheAccessorMock.Verify();
        }

        [Fact]
        public async Task UpdateRestaurantAsync_WhenItIsNotFound_ThrowsNotFoundException()
        {
            //Arrange
            Restaurant restaurant = RestaurantDataFaker.GetFakedRestaurant();
            var updateRestaurantDTO = _mapper.Map<UpdateRestaurantDTO>(restaurant);

            _restaurantCacheAccessorMock.MockDeleteResourceByIdAsync(restaurant.Id.ToString());

            //Act
            await base.Update_WhenItIsNotFound_ThrowsNotFoundException<UpdateRestaurantDTO, ReadRestaurantDTO>(
                updateRestaurantDTO, restaurant.Id);

            //Assert
            _restaurantCacheAccessorMock.Verify();
        }
    }
}
