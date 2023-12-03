using AutoMapper;
using CatalogService.Application.DTOs.FoodType;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Application.MappingProfiles;
using CatalogService.Application.Services;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Domain.Interfaces.Services;
using CatalogService.Tests.Fakers;
using CatalogService.Tests.Mocks.CacheAccessors;
using CatalogService.Tests.Mocks.HttpContextAccessors;
using CatalogService.Tests.Mocks.Repositories;
using CatalogService.Tests.Mocks.TokenParsers;
using CatalogService.Tests.ServicesTests.Base;
using FluentAssertions;
using Moq;

namespace CatalogService.Tests.ServicesTests
{
    public class FoodTypeServiceTests : BaseServiceTests<IFoodTypeRepository, FoodType>
    {
        private readonly HttpContextAccessorMock _httpContextAccessorMock;
        private readonly JwtTokenParserMock _tokenParserMock;

        private readonly EmployeeRepositoryMock _employeeRepositoryMock;
        private readonly FoodTypeRepositoryMock _foodTypeRepositoryMock;

        private readonly FoodTypeCacheAccessorMock _foodTypeCacheAccessorMock;

        private readonly IBaseFoodTypeService _foodTypeService;
        private readonly IMapper _mapper;

        public FoodTypeServiceTests() : base()
        {
            _httpContextAccessorMock = new();
            _tokenParserMock = new();
            _employeeRepositoryMock = new();
            _foodTypeCacheAccessorMock = new();
            _foodTypeRepositoryMock = new();

            _foodTypeService = new FoodTypeService(_foodTypeRepositoryMock.Object,
                _mapperMock.Object, _httpContextAccessorMock.Object,
                _tokenParserMock.Object, _employeeRepositoryMock.Object,
                _foodTypeCacheAccessorMock.Object);

            _mapper = new Mapper(new MapperConfiguration(
                configure => configure.AddProfile(new FoodTypeProfile())));

            _baseRepositoryMock = _foodTypeRepositoryMock;
            _baseService = _foodTypeService;
        }

        [Fact]
        public async Task GetFoodTypeByIdAsync_WhenItIsExisting_ReturnsReadFoodTypeDTO()
        {
            //Arrange
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodType();
            var readFoodTypeDTO = _mapper.Map<ReadFoodTypeDTO>(foodType);

            _foodTypeCacheAccessorMock.MockGetByResourceIdAsync(foodType.Id.ToString(), readFoodTypeDTO);

            //Act
            var result = await _foodTypeService.GetByIdAsync<ReadFoodTypeDTO>(foodType.Id,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readFoodTypeDTO);

            _foodTypeCacheAccessorMock.Verify();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsReadFoodTypeDTOs()
        {
            //Arrange
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodType();
            var foodTypes = new List<FoodType> { foodType };
            var readFoodTypeDTOs = _mapper.Map<List<ReadFoodTypeDTO>>(foodTypes);

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(readFoodTypeDTOs);
        }

        [Fact]
        public async Task InsertFoodTypeAsync_WhenItIsSaved_ReturnsReadFoodTypeDTO()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodType();

            var insertFoodTypeDTO = _mapper.Map<FoodTypeDTO>(foodType);
            var readFoodTypeDTO = _mapper.Map<ReadFoodTypeDTO>(foodType);

            var isExisting = true;

            _foodTypeCacheAccessorMock.MockGetByResourceIdAsync(foodType.Id.ToString(), readFoodTypeDTO);
            _tokenParserMock.MockParseSubjectId(employee.Id);
            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            //Act
            await base.InsertAsync_WhenItIsSaved_ReturnsEntity(insertFoodTypeDTO, foodType, readFoodTypeDTO);

            //Assert
            _foodTypeCacheAccessorMock.Verify();
            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
        }

        [Fact]
        public async Task InsertFoodTypeAsync_WhenItIsNotSaved_ThrowsDbOperationException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodType();

            var insertFoodTypeDTO = _mapper.Map<FoodTypeDTO>(foodType);

            var isExisting = true;

            _tokenParserMock.MockParseSubjectId(employee.Id);
            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            //Act
            await base.InsertAsync_WhenItIsNotSaved_ThrowsDbOperationException<FoodTypeDTO, ReadFoodTypeDTO>(
                insertFoodTypeDTO, foodType);

            //Assert
            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
        }

        [Fact]
        public async Task InsertFoodTypeAsync_WhenInititatorIsNotEmployee_ThrowsAuthorizationException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodType();

            var insertFoodTypeDTO = _mapper.Map<FoodTypeDTO>(foodType);
            var readFoodTypeDTO = _mapper.Map<ReadFoodTypeDTO>(foodType);

            var isExisting = false;

            _tokenParserMock.MockParseSubjectId(employee.Id);
            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            //Act
            var result = _foodTypeService.InsertAsync<FoodTypeDTO, ReadFoodTypeDTO>(
                insertFoodTypeDTO, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => result);

            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
        }

        [Fact]
        public async Task UpdateFoodTypeAsync_WhenItIsSaved_ReturnsReadFoodTypeDTO()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodType();

            var updateFoodTypeDTO = _mapper.Map<FoodTypeDTO>(foodType);
            var readFoodTypeDTO = _mapper.Map<ReadFoodTypeDTO>(foodType);

            var isExisting = true;

            _foodTypeCacheAccessorMock.MockDeleteResourceByIdAsync(foodType.Id.ToString());
            _tokenParserMock.MockParseSubjectId(employee.Id);
            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            //Act
            await base.Update_WhenItIsSaved_ReturnsEntity(updateFoodTypeDTO, foodType, readFoodTypeDTO);

            //Assert
            _foodTypeCacheAccessorMock.Verify();
            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
        }

        [Fact]
        public async Task UpdateFoodTypeAsync_WhenItIsNotSaved_ThrowsDbOperationException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodType();

            var updateFoodTypeDTO = _mapper.Map<FoodTypeDTO>(foodType);
            var readFoodTypeDTO = _mapper.Map<ReadFoodTypeDTO>(foodType);

            var isExisting = true;

            _foodTypeCacheAccessorMock.MockDeleteResourceByIdAsync(foodType.Id.ToString());
            _tokenParserMock.MockParseSubjectId(employee.Id);
            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            //Act
            await base.Update_WhenItIsNotSaved_ThrowsDbOperationException(
                updateFoodTypeDTO, foodType, readFoodTypeDTO);

            //Assert
            _foodTypeCacheAccessorMock.Verify();
            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
        }

        [Fact]
        public async Task UpdateFoodTypeAsync_WhenItIsNotFound_ThrowsNotFoundException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodType();

            var updateFoodTypeDTO = _mapper.Map<FoodTypeDTO>(foodType);

            var isExisting = true;

            _foodTypeCacheAccessorMock.MockDeleteResourceByIdAsync(foodType.Id.ToString());
            _tokenParserMock.MockParseSubjectId(employee.Id);
            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            //Act
            await base.Update_WhenItIsNotFound_ThrowsNotFoundException<FoodTypeDTO, ReadFoodTypeDTO>(
                updateFoodTypeDTO, foodType.Id);

            //Assert
            _foodTypeCacheAccessorMock.Verify();
            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
        }

        [Fact]
        public async Task UpdateFoodTypeAsync_WhenInitiatorIsNotEmployee_ThrowsAuthorizationException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodType();

            var updateFoodTypeDTO = _mapper.Map<FoodTypeDTO>(foodType);

            var isExisting = false;

            _tokenParserMock.MockParseSubjectId(employee.Id);
            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            //Act
            var result = _foodTypeService.UpdateAsync<FoodTypeDTO, ReadFoodTypeDTO>(foodType.Id,
                updateFoodTypeDTO, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => result);

            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
        }

        [Fact]
        public async Task DeleteFoodTypeAsync_WhenInitiatorIsNotEmployee_ThrowsAuthorizationException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodType();

            var isExisting = false;

            _tokenParserMock.MockParseSubjectId(employee.Id);
            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            //Act
            var result = _foodTypeService.DeleteAsync(foodType.Id,
                It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => result);

            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
        }

        [Fact]
        public async Task DeleteFoodTypeAsync_WhenItIsSaved_ReturnsId()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodType();

            var isExisting = true;

            _foodTypeCacheAccessorMock.MockDeleteResourceByIdAsync(foodType.Id.ToString());
            _tokenParserMock.MockParseSubjectId(employee.Id);
            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            //Act
            await base.DeleteAsync_WhenItIsSaved_ReturnsId(foodType);

            //Assert
            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
            _foodTypeCacheAccessorMock.Verify();
        }

        [Fact]
        public async Task DeleteFoodTypeAsync_WhenItIsNotSaved_ThrowsDbOperationException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodType();

            var isExisting = true;

            _foodTypeCacheAccessorMock.MockDeleteResourceByIdAsync(foodType.Id.ToString());
            _tokenParserMock.MockParseSubjectId(employee.Id);
            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            //Act
            await base.DeleteAsync_WhenItIsNotSaved_ThrowsDbOperationException(foodType);

            //Assert
            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
            _foodTypeCacheAccessorMock.Verify();
        }

        [Fact]
        public async Task DeleteFoodTypeAsync_WhenItIsNotExisting_ThrowsNotFoundException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            FoodType foodType = FoodTypeDataFaker.GetFakedFoodType();

            var isExisting = true;

            _foodTypeCacheAccessorMock.MockDeleteResourceByIdAsync(foodType.Id.ToString());
            _tokenParserMock.MockParseSubjectId(employee.Id);
            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            //Act
            await base.DeleteAsync_WhenItIsNotExisting_ThrowsNotFoundException(foodType.Id);

            //Assert
            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
            _foodTypeCacheAccessorMock.Verify();
        }
    }
}
