using AutoMapper;
using CatalogService.Application.DTOs.Menu;
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
    public class MenuControllerTests
    {
        private readonly MenuServiceMock _menuServiceMock;

        private readonly IMapper _mapper;
        private readonly MenuController _menuController;

        public MenuControllerTests()
        {
            _menuServiceMock = new();

            var profiles = new List<Profile>
            {
                new MenuProfile(),
                new FoodTypeProfile()
            };

            _mapper = new Mapper(new MapperConfiguration(configure =>
                configure.AddProfiles(profiles)));

            _menuController = new MenuController(_menuServiceMock.Object);
        }

        [Fact]
        public async Task GetAllMenusAsync_ReturnsReadMenuDTOs()
        {
            //Arrange
            Menu menu = MenuDataFaker.GetFakedMenu();
            var menus = new List<Menu> { menu };
            var readMenuDTOs = _mapper.Map<List<ReadMenuDTO>>(menus);

            _menuServiceMock.MockGetAllAsync(readMenuDTOs);

            //Act
            var result = await _menuController.GetAllFoodAsync(It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(readMenuDTOs);
        }

        [Fact]
        public async Task GetMenuAsync_ReturnsReadMenuDTO()
        {
            //Arrange
            Menu menu = MenuDataFaker.GetFakedMenu();
            var readMenuDTO = _mapper.Map<ReadMenuDTO>(menu);

            _menuServiceMock.MockGetItemAsync(readMenuDTO);

            //Act
            var result = await _menuController.GetFoodAsync(menu.Id,
                It.IsAny<CancellationToken>());
            var okResult = result as OkObjectResult;

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));

            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeEquivalentTo(readMenuDTO);
        }

        [Fact]
        public async Task InsertMenuAsync_ReturnsReadMenuDTO()
        {
            //Arrange
            Menu menu = MenuDataFaker.GetFakedMenu();
            var insertMenuDTO = _mapper.Map<InsertMenuDTO>(menu);
            var readMenuDTO = _mapper.Map<ReadMenuDTO>(menu);

            _menuServiceMock.MockInsertMenuAsync(insertMenuDTO,
                readMenuDTO);

            //Act
            var result = await _menuController.InsertFoodAsync(insertMenuDTO,
                It.IsAny<CancellationToken>());
            var createdAtActionResult = result as CreatedAtActionResult;

            //Assert
            result.Should().BeOfType(typeof(CreatedAtActionResult));

            createdAtActionResult.Should().NotBeNull();
            createdAtActionResult!.Value.Should().BeEquivalentTo(readMenuDTO);
        }

        [Fact]
        public async Task DeleteMenuAsync_ReturnsNoContent()
        {
            //Arrange
            Menu menu = MenuDataFaker.GetFakedMenu();

            _menuServiceMock.MockDeleteItemAsync(menu.Id);

            //Act
            var result = await _menuController.DeleteFoodAsync(menu.Id,
                It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeOfType(typeof(NoContentResult));
        }
    }
}
