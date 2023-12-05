using AutoMapper;
using CatalogService.Application.DTOs.Menu;
using CatalogService.Application.DTOs.Menu.Messages;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.MappingProfiles;
using CatalogService.Application.Services;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Tests.Fakers;
using CatalogService.Tests.Mocks.CacheAccessors;
using CatalogService.Tests.Mocks.HttpContextAccessors;
using CatalogService.Tests.Mocks.MessageProducers;
using CatalogService.Tests.Mocks.Repositories;
using CatalogService.Tests.Mocks.TokenParsers;
using CatalogService.Tests.ServicesTests.Base;
using FluentAssertions;
using Moq;

namespace CatalogService.Tests.ServicesTests
{
    public class MenuServiceTests : BaseServiceTests<IMenuRepository, Menu>
    {
        private readonly MenuRepositoryMock _menuRepositoryMock;
        private readonly EmployeeRepositoryMock _employeeRepositoryMock;
        private readonly RestaurantRepositoryMock _restaurantRepositoryMock;

        private readonly TokenParserMock _tokenParserMock;
        private readonly HttpContextAccessorMock _httpContextAccessorMock;

        private readonly MenuMessageProducerMock _menuMessageProducerMock;

        private readonly MenuCacheAccessorMock _menuCacheAccessorMock;

        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;

        public MenuServiceTests() : base()
        {
            _menuRepositoryMock = new();
            _employeeRepositoryMock = new();
            _restaurantRepositoryMock = new();

            _tokenParserMock = new();
            _httpContextAccessorMock = new();

            _menuMessageProducerMock = new();

            _menuCacheAccessorMock = new();

            _menuService = new MenuService(_menuRepositoryMock.Object,
                _mapperMock.Object, _httpContextAccessorMock.Object,
                _tokenParserMock.Object, _employeeRepositoryMock.Object,
                _restaurantRepositoryMock.Object, _menuMessageProducerMock.Object,
                _menuCacheAccessorMock.Object);

            var profiles = new List<Profile>
            {
                new MenuProfile(),
                new FoodTypeProfile()
            };

            _mapper = new Mapper(new MapperConfiguration(configure =>
                configure.AddProfiles(profiles)));

            _baseService = _menuService;
            _baseRepositoryMock = _menuRepositoryMock;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsReadMenuDTO()
        {
            //Arrange
            Menu menu = MenuDataFaker.GetFakedMenu();
            var menus = new List<Menu> { menu };
            var readMenuDTOs = _mapper.Map<List<ReadMenuDTO>>(menus);

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(readMenuDTOs);
        }

        [Fact]
        public async Task GetMenuByIdAsync_WhenItIsExisting_ReturnsReadMenuDTO()
        {
            //Arrange
            Menu menu = MenuDataFaker.GetFakedMenu();
            var readMenuDTO = _mapper.Map<ReadMenuDTO>(menu);

            _menuCacheAccessorMock.MockGetByResourceIdAsync(menu.Id.ToString(), readMenuDTO);

            //Act
            var result = await _menuService.GetByIdAsync<ReadMenuDTO>(menu.Id,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readMenuDTO);

            _menuCacheAccessorMock.Verify();
        }

        [Fact]
        public async Task InsertMenuAsync_WhenItIsSaved_ReturnsReadMenuDTO()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            Menu menu = MenuDataFaker.GetFakedMenu();
            var readMenuDTO = _mapper.Map<ReadMenuDTO>(menu);
            var insertMenuMessageDTO = _mapper.Map<InsertMenuMessageDTO>(readMenuDTO);
            var insertMenuDTO = _mapper.Map<InsertMenuDTO>(menu);

            _tokenParserMock.MockParseSubjectId(employee.Id);

            var isExisting = true;

            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            var isWorkingAtRestaurant = true;

            _restaurantRepositoryMock.MockIsWorkingAtRestaurant(employee.Id,
                menu.RestaurantId, isWorkingAtRestaurant);

            _menuCacheAccessorMock.MockGetByResourceIdAsync(menu.Id.ToString(), readMenuDTO);

            bool isSaved = true;

            _mapperMock.MockMap(insertMenuDTO, menu)
                       .MockMap(readMenuDTO, insertMenuMessageDTO);

            _baseRepositoryMock.MockInsertAsync(menu)
                               .MockSaveChangesAsync(isSaved);

            _menuMessageProducerMock.MockProduceMessageAsync(insertMenuMessageDTO);

            //Act
            var result = await _menuService.InsertAsync<ReadMenuDTO>(
                insertMenuDTO, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readMenuDTO);

            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
            _restaurantRepositoryMock.Verify();
            _mapperMock.Verify();
            _baseRepositoryMock.Verify();
            _menuCacheAccessorMock.Verify();
            _menuMessageProducerMock.Verify();
        }

        [Fact]
        public async Task InsertMenuAsync_WhenItNotSaved_ThrowsDbOperationException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            Menu menu = MenuDataFaker.GetFakedMenu();
            var insertMenuDTO = _mapper.Map<InsertMenuDTO>(menu);

            _tokenParserMock.MockParseSubjectId(employee.Id);

            var isExisting = true;

            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            var isWorkingAtRestaurant = true;

            _restaurantRepositoryMock.MockIsWorkingAtRestaurant(employee.Id,
                menu.RestaurantId, isWorkingAtRestaurant);

            bool isSaved = false;

            _mapperMock.MockMap(insertMenuDTO, menu);

            _baseRepositoryMock.MockInsertAsync(menu)
                               .MockSaveChangesAsync(isSaved);

            //Act
            var result = _menuService.InsertAsync<ReadMenuDTO>(
                insertMenuDTO, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<DbOperationException>(() => result);

            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
            _restaurantRepositoryMock.Verify();
            _mapperMock.Verify();
            _baseRepositoryMock.Verify();
            _menuCacheAccessorMock.Verify();
        }

        [Fact]
        public async Task InsertMenuAsync_WhenInitiatorIsNotWorkingAtRestaurant_ThrowsAuthorizationException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            Menu menu = MenuDataFaker.GetFakedMenu();
            var insertMenuDTO = _mapper.Map<InsertMenuDTO>(menu);

            _tokenParserMock.MockParseSubjectId(employee.Id);

            var isExisting = true;

            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            var isWorkingAtRestaurant = false;

            _restaurantRepositoryMock.MockIsWorkingAtRestaurant(employee.Id,
                menu.RestaurantId, isWorkingAtRestaurant);

            //Act
            var result = _menuService.InsertAsync<ReadMenuDTO>(
                insertMenuDTO, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => result);

            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
            _restaurantRepositoryMock.Verify();
        }

        [Fact]
        public async Task InsertMenuAsync_WhenInitiatorIsNotExisting_ThrowsAuthorizationException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            Menu menu = MenuDataFaker.GetFakedMenu();
            var insertMenuDTO = _mapper.Map<InsertMenuDTO>(menu);

            _tokenParserMock.MockParseSubjectId(employee.Id);

            var isExisting = false;

            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            //Act
            var result = _menuService.InsertAsync<ReadMenuDTO>(
                insertMenuDTO, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => result);

            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
        }

        [Fact]
        public async Task UpdateMenu_WhenItIsSaved_ReturnsMenu()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            Menu menu = MenuDataFaker.GetFakedMenu();
            var readMenuDTO = _mapper.Map<ReadMenuDTO>(menu);
            var updateMenuMessageDTO = _mapper.Map<UpdateMenuMessageDTO>(readMenuDTO);
            var updateMenuDTO = _mapper.Map<UpdateMenuDTO>(menu);

            _tokenParserMock.MockParseSubjectId(employee.Id);

            var isExisting = true;

            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            var isWorkingAtRestaurant = true;

            _restaurantRepositoryMock.MockIsWorkingAtRestaurant(employee.Id,
                menu.RestaurantId, isWorkingAtRestaurant);

            _menuCacheAccessorMock.MockDeleteResourceByIdAsync(menu.Id.ToString());

            bool isSaved = true;

            _mapperMock.MockMap(updateMenuDTO, menu)
                       .MockMap(readMenuDTO, updateMenuMessageDTO);

            _baseRepositoryMock.MockUpdate(menu)
                               .MockGetByIdAsync(menu.Id, readMenuDTO)
                               .MockSaveChangesAsync(isSaved);

            _menuMessageProducerMock.MockProduceMessageAsync(updateMenuMessageDTO);

            //Act
            var result = await _menuService.UpdateAsync<ReadMenuDTO>(menu.Id,
                updateMenuDTO, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readMenuDTO);

            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
            _restaurantRepositoryMock.Verify();
            _mapperMock.Verify();
            _baseRepositoryMock.Verify();
            _menuCacheAccessorMock.Verify();
            _menuMessageProducerMock.Verify();
        }

        [Fact]
        public async Task UpdateMenu_WhenItIsNotSaved_ThrowsDbOperationException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            Menu menu = MenuDataFaker.GetFakedMenu();
            var readMenuDTO = _mapper.Map<ReadMenuDTO>(menu);
            var updateMenuDTO = _mapper.Map<UpdateMenuDTO>(menu);

            _tokenParserMock.MockParseSubjectId(employee.Id);

            var isExisting = true;

            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            var isWorkingAtRestaurant = true;

            _restaurantRepositoryMock.MockIsWorkingAtRestaurant(employee.Id,
                menu.RestaurantId, isWorkingAtRestaurant);

            _menuCacheAccessorMock.MockDeleteResourceByIdAsync(menu.Id.ToString());

            bool isSaved = false;

            _mapperMock.MockMap(updateMenuDTO, menu);

            _baseRepositoryMock.MockUpdate(menu)
                               .MockGetByIdAsync(menu.Id, readMenuDTO)
                               .MockSaveChangesAsync(isSaved);

            //Act
            var result = _menuService.UpdateAsync<ReadMenuDTO>(menu.Id,
                updateMenuDTO, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<DbOperationException>(() => result);

            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
            _restaurantRepositoryMock.Verify();
            _mapperMock.Verify();
            _baseRepositoryMock.Verify();
            _menuCacheAccessorMock.Verify();
        }

        [Fact]
        public async Task UpdateMenu_WhenItIsNotFound_ThrowsNotFoundException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            Menu menu = MenuDataFaker.GetFakedMenu();
            var updateMenuDTO = _mapper.Map<UpdateMenuDTO>(menu);

            _tokenParserMock.MockParseSubjectId(employee.Id);

            var isExisting = true;

            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            var isWorkingAtRestaurant = true;

            _restaurantRepositoryMock.MockIsWorkingAtRestaurant(employee.Id,
                menu.RestaurantId, isWorkingAtRestaurant);

            _menuCacheAccessorMock.MockDeleteResourceByIdAsync(menu.Id.ToString());

            _baseRepositoryMock.MockGetByIdAsync(menu.Id, default(ReadMenuDTO));

            //Act
            var result = _menuService.UpdateAsync<ReadMenuDTO>(menu.Id,
                updateMenuDTO, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
            _restaurantRepositoryMock.Verify();
            _baseRepositoryMock.Verify();
            _menuCacheAccessorMock.Verify();
        }

        [Fact]
        public async Task UpdateMenu_WhenInitiatorIsNotWorkingAtRestaurant_ThrowsAuthorizationException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            Menu menu = MenuDataFaker.GetFakedMenu();
            var updateMenuDTO = _mapper.Map<UpdateMenuDTO>(menu);

            _tokenParserMock.MockParseSubjectId(employee.Id);

            var isExisting = true;

            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            var isWorkingAtRestaurant = false;

            _restaurantRepositoryMock.MockIsWorkingAtRestaurant(employee.Id,
                menu.RestaurantId, isWorkingAtRestaurant);

            //Act
            var result = _menuService.UpdateAsync<ReadMenuDTO>(menu.Id,
                updateMenuDTO, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => result);

            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
            _restaurantRepositoryMock.Verify();
        }

        [Fact]
        public async Task UpdateMenu_WhenInitiatorIsNotExisting_ThrowsAuthorizationException()
        {
            //Arrange
            Employee employee = EmployeeDataFaker.GetFakedEmployee();
            Menu menu = MenuDataFaker.GetFakedMenu();
            var updateMenuDTO = _mapper.Map<UpdateMenuDTO>(menu);

            _tokenParserMock.MockParseSubjectId(employee.Id);

            var isExisting = false;

            _employeeRepositoryMock.MockIsExistingAsync(employee.Id, isExisting);

            //Act
            var result = _menuService.UpdateAsync<ReadMenuDTO>(menu.Id,
                updateMenuDTO, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => result);

            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
        }

        [Fact]
        public async Task DeleteMenuAsync_WhenItIsSaved_ReturnsId()
        {
            //Arrange
            Menu menu = MenuDataFaker.GetFakedMenu();
            var menuDTO = _mapper.Map<MenuDTO>(menu);
            var deleteMessageDTO = new DeleteMenuMessageDTO
            {
                Id = menu.Id,
            };

            _baseRepositoryMock.MockGetByIdAsync(menu.Id, menuDTO);

            _tokenParserMock.MockParseSubjectId(menu.Id);

            var isExisting = true;

            _employeeRepositoryMock.MockIsExistingAsync(menu.Id, isExisting);

            var isWorkingAtRestaurant = true;

            _restaurantRepositoryMock.MockIsWorkingAtRestaurant(menu.Id,
                menu.RestaurantId, isWorkingAtRestaurant);

            _menuCacheAccessorMock.MockDeleteResourceByIdAsync(menu.Id.ToString());

            _menuMessageProducerMock.MockProduceMessageAsync(deleteMessageDTO);

            //Act
            await base.DeleteAsync_WhenItIsSaved_ReturnsId(menu);

            //Assert
            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
            _restaurantRepositoryMock.Verify();
            _menuCacheAccessorMock.Verify();
            _menuMessageProducerMock.Verify(producer => producer.ProduceMessageAsync(
                It.Is<DeleteMenuMessageDTO>(message => message.Id == menu.Id),
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task DeleteMenuAsync_WhenItIsNotSaved_ThrowsDbOperationException()
        {
            //Arrange
            Menu menu = MenuDataFaker.GetFakedMenu();
            var menuDTO = _mapper.Map<MenuDTO>(menu);

            _baseRepositoryMock.MockGetByIdAsync(menu.Id, menuDTO);

            _tokenParserMock.MockParseSubjectId(menu.Id);

            var isExisting = true;

            _employeeRepositoryMock.MockIsExistingAsync(menu.Id, isExisting);

            var isWorkingAtRestaurant = true;

            _restaurantRepositoryMock.MockIsWorkingAtRestaurant(menu.Id,
                menu.RestaurantId, isWorkingAtRestaurant);

            _menuCacheAccessorMock.MockDeleteResourceByIdAsync(menu.Id.ToString());

            //Act
            await base.DeleteAsync_WhenItIsNotSaved_ThrowsDbOperationException(menu);

            //Assert
            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
            _restaurantRepositoryMock.Verify();
            _menuCacheAccessorMock.Verify();
        }

        [Fact]
        public async Task DeleteMenuAsync_WhenItIsNotFound_ThrowsNotFoundException()
        {
            //Arrange
            Menu menu = MenuDataFaker.GetFakedMenu();

            _baseRepositoryMock.MockGetByIdAsync(menu.Id, default(MenuDTO));

            //Act
            var result = _menuService.DeleteAsync(menu.Id, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _baseRepositoryMock.Verify();
        }

        [Fact]
        public async Task DeleteMenuAsync_WhenInitiatorIsNotWorkingAtRestaurant_ThrowsAuthorizationException()
        {
            //Arrange
            Menu menu = MenuDataFaker.GetFakedMenu();
            var menuDTO = _mapper.Map<MenuDTO>(menu);

            _baseRepositoryMock.MockGetByIdAsync(menu.Id, menuDTO);

            _tokenParserMock.MockParseSubjectId(menu.Id);

            var isExisting = true;

            _employeeRepositoryMock.MockIsExistingAsync(menu.Id, isExisting);

            var isWorkingAtRestaurant = false;

            _restaurantRepositoryMock.MockIsWorkingAtRestaurant(menu.Id,
                menu.RestaurantId, isWorkingAtRestaurant);

            //Act
            var result = _menuService.DeleteAsync(menu.Id, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => result);

            _baseRepositoryMock.Verify();
            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
            _restaurantRepositoryMock.Verify();
        }

        [Fact]
        public async Task DeleteMenuAsync_WhenInitiatorIsNotExisting_ThrowsAuthorizationException()
        {
            //Arrange
            Menu menu = MenuDataFaker.GetFakedMenu();
            var menuDTO = _mapper.Map<MenuDTO>(menu);

            _baseRepositoryMock.MockGetByIdAsync(menu.Id, menuDTO);

            _tokenParserMock.MockParseSubjectId(menu.Id);

            var isExisting = false;

            _employeeRepositoryMock.MockIsExistingAsync(menu.Id, isExisting);

            //Act
            var result = _menuService.DeleteAsync(menu.Id, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<AuthorizationException>(() => result);

            _baseRepositoryMock.Verify();
            _tokenParserMock.Verify();
            _employeeRepositoryMock.Verify();
            _restaurantRepositoryMock.Verify();
        }
    }
}
