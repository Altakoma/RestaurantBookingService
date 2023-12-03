using AutoMapper;
using CatalogService.Application.DTOs.FoodType;
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
    public class FoodTypeRepositoryTests : BaseRepositoryTests<FoodType>
    {
        private readonly IFoodTypeRepository _foodTypeRepository;
        private readonly IMapper _mapper;

        public FoodTypeRepositoryTests() : base(typeof(FoodTypeRepository))
        {
            _foodTypeRepository = new FoodTypeRepository(_catalogServiceDbContextMock.Object,
                _mapperMock.Object);

            _mapper = new Mapper(new MapperConfiguration(
                configure => configure.AddProfile(new FoodTypeProfile())));
        }

        [Fact]
        public async Task GetAllAsync_ReturnsReadFoodTypeDTOs()
        {
            //Arrange
            var foodTypes = new List<FoodType> { FoodTypeDataFaker.GetFakedFoodType() };
            IQueryable<FoodType> menuQuery = foodTypes.AsQueryable();

            var menuReadDTOs = _mapper.Map<List<ReadFoodTypeDTO>>(foodTypes);

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(menuQuery, menuReadDTOs);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsFoodTypes()
        {
            //Arrange
            var foodTypes = new List<FoodType> { FoodTypeDataFaker.GetFakedFoodType() };
            IQueryable<FoodType> menuQuery = foodTypes.AsQueryable();

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(menuQuery, foodTypes);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsFoodType()
        {
            //Arrange
            var menu = FoodTypeDataFaker.GetFakedFoodType();
            IQueryable<FoodType> menuQuery = new List<FoodType> { menu }.AsQueryable();

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(menu.Id, menuQuery, menu);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsReadFoodTypeDTO()
        {
            //Arrange
            var menu = FoodTypeDataFaker.GetFakedFoodType();
            IQueryable<FoodType> menuQuery = new List<FoodType> { menu }.AsQueryable();

            var readFoodTypeDTO = _mapper.Map<ReadFoodTypeDTO>(menu);

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(menu.Id, menuQuery, readFoodTypeDTO);
        }

        [Fact]
        public async Task InsertFoodTypeAsync_SuccessfullyExecuted()
        {
            //Arrange
            var menu = FoodTypeDataFaker.GetFakedFoodType();

            //Act
            //Assert
            await base.InsertAsync_SuccessfullyExecuted(menu);
        }

        [Fact]
        public void UpdateFoodType_SuccessfullyExecuted()
        {
            //Arrange
            var menu = FoodTypeDataFaker.GetFakedFoodType();

            //Act
            //Assert
            base.UpdateEntity_SuccessfullyExecuted(menu);
        }

        [Fact]
        public void DeleteFoodType_SuccessfullyExecuted()
        {
            //Arrange
            var menu = FoodTypeDataFaker.GetFakedFoodType();

            //Act
            //Assert
            base.DeleteEntity_SuccessfullyExecuted(menu);
        }

        [Fact]
        public async Task DeleteFoodTypeAsync_WhenFoodTypeIsExisting_SuccessfullyExecuted()
        {
            //Arrange
            var menu = FoodTypeDataFaker.GetFakedFoodType();
            ICollection<FoodType> foodTypes = new List<FoodType> { menu };

            IQueryable<FoodType> query = foodTypes.AsQueryable();

            _catalogServiceDbContextMock.MockDataSet(query);

            //Act
            await _foodTypeRepository.DeleteAsync(menu.Id, It.IsAny<CancellationToken>());

            //Assert
            _catalogServiceDbContextMock.Verify();
        }

        [Fact]
        public async Task DeleteFoodTypeAsync_WhenFoodTypeIsNotExisting_ThrowsNotFoundException()
        {
            //Arrange
            var menu = FoodTypeDataFaker.GetFakedFoodType();
            int id = menu.Id;
            menu.Id = 0;

            IQueryable<FoodType> query = new List<FoodType> { menu }.AsQueryable();

            _catalogServiceDbContextMock.MockDataSet(query);

            //Act
            var result = _foodTypeRepository.DeleteAsync(id, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _catalogServiceDbContextMock.Verify();
        }
    }
}
