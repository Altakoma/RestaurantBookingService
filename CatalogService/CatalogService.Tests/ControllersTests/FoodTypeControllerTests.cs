using AutoMapper;
using CatalogService.Application.DTOs.FoodType;
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
    public class FoodTypeControllerTests
    {
        private readonly FoodTypeServiceMock _foodTypeServiceMock;

        private readonly IMapper _mapper;
        private readonly FoodTypeController _foodTypeController;

        public FoodTypeControllerTests()
        {
            _foodTypeServiceMock = new();

            var profiles = new List<Profile>
            {
                new FoodTypeProfile(),
                new FoodTypeProfile()
            };

            _mapper = new Mapper(new MapperConfiguration(configure =>
                configure.AddProfiles(profiles)));

            _foodTypeController = new FoodTypeController(_foodTypeServiceMock.Object);
        }

        [Fact]
        public async Task GetAllFoodTypesAsync_ReturnsReadFoodTypeDTOs()
        {
            //Arrange
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodType();
            var foodTypes = new List<FoodType> { foodType };
            var readFoodTypeDTOs = _mapper.Map<List<ReadFoodTypeDTO>>(foodTypes);

            _foodTypeServiceMock.MockGetAllAsync(readFoodTypeDTOs);

            //Act
            var result = await _foodTypeController.GetAllFoodTypesAsync(It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(readFoodTypeDTOs);
        }

        [Fact]
        public async Task GetFoodTypeAsync_ReturnsReadFoodTypeDTO()
        {
            //Arrange
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodType();
            var readFoodTypeDTO = _mapper.Map<ReadFoodTypeDTO>(foodType);

            _foodTypeServiceMock.MockGetItemAsync(readFoodTypeDTO);

            //Act
            var result = await _foodTypeController.GetFoodTypeAsync(foodType.Id,
                It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(readFoodTypeDTO);
        }

        [Fact]
        public async Task InsertFoodTypeAsync_ReturnsReadFoodTypeDTO()
        {
            //Arrange
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodType();
            var insertFoodTypeDTO = _mapper.Map<FoodTypeDTO>(foodType);
            var readFoodTypeDTO = _mapper.Map<ReadFoodTypeDTO>(foodType);

            _foodTypeServiceMock.MockInsertItemAsync(insertFoodTypeDTO,
                readFoodTypeDTO);

            //Act
            var result = await _foodTypeController.InsertFoodTypeAsync(insertFoodTypeDTO,
                It.IsAny<CancellationToken>());
            var createdAtActionResult = result as CreatedAtActionResult;

            //Assert
            result.Should().BeOfType(typeof(CreatedAtActionResult));

            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult!.Value.Should().BeEquivalentTo(readFoodTypeDTO);
        }

        [Fact]
        public async Task DeleteFoodTypeAsync_ReturnsNoContent()
        {
            //Arrange
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodType();

            _foodTypeServiceMock.MockDeleteItemAsync(foodType.Id);

            //Act
            var result = await _foodTypeController.DeleteFoodTypeAsync(foodType.Id,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeOfType(typeof(NoContentResult));
        }
    }
}
