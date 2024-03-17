using AutoMapper;
using CatalogService.Application.DTOs.Menu;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Application.MappingProfiles;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Infrastructure.Data.Repositories;
using CatalogService.Tests.Fakers;
using CatalogService.Tests.RepositoriesTests.Base;
using FluentAssertions;
using Moq;

namespace CatalogService.Tests.RepositoriesTests
{
    public class MenuRepositoryTests : BaseRepositoryTests<Menu>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;

        public MenuRepositoryTests() : base()
        {
            _menuRepository = new MenuRepository(_catalogServiceDbContextMock.Object,
                _mapperMock.Object);

            var profiles = new List<Profile>
            {
                new MenuProfile(),
                new FoodTypeProfile()
            };

            _mapper = new Mapper(new MapperConfiguration(
                configure => configure.AddProfiles(profiles)));

            _repository = _menuRepository;
        }

        [Fact]
        public async Task GetAllAsync_ReturnsReadMenuDTOs()
        {
            //Arrange
            var menus = new List<Menu> { MenuDataFaker.GetFakedMenu() };
            IQueryable<Menu> menuQuery = menus.AsQueryable();

            var menuReadDTOs = _mapper.Map<ICollection<ReadMenuDTO>>(menus);

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(menuQuery, menuReadDTOs);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsMenus()
        {
            //Arrange
            var menus = new List<Menu> { MenuDataFaker.GetFakedMenu() };
            IQueryable<Menu> menuQuery = menus.AsQueryable();

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(menuQuery, menus);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsMenu()
        {
            //Arrange
            var menu = MenuDataFaker.GetFakedMenu();
            IQueryable<Menu> menuQuery = new List<Menu> { menu }.AsQueryable();

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(menu.Id, menuQuery, menu);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsReadMenuDTO()
        {
            //Arrange
            var menu = MenuDataFaker.GetFakedMenu();
            IQueryable<Menu> menuQuery = new List<Menu> { menu }.AsQueryable();

            var readMenuDTO = _mapper.Map<ReadMenuDTO>(menu);

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(menu.Id, menuQuery, readMenuDTO);
        }

        [Fact]
        public async Task InsertMenuAsync_SuccessfullyExecuted()
        {
            //Arrange
            var menu = MenuDataFaker.GetFakedMenu();

            //Act
            //Assert
            await base.InsertAsync_SuccessfullyExecuted(menu);
        }

        [Fact]
        public void UpdateMenu_SuccessfullyExecuted()
        {
            //Arrange
            var menu = MenuDataFaker.GetFakedMenu();

            //Act
            //Assert
            base.UpdateEntity_SuccessfullyExecuted(menu);
        }

        [Fact]
        public void DeleteMenu_SuccessfullyExecuted()
        {
            //Arrange
            var menu = MenuDataFaker.GetFakedMenu();

            //Act
            //Assert
            base.DeleteEntity_SuccessfullyExecuted(menu);
        }

        [Fact]
        public async Task DeleteMenuAsync_WhenMenuIsExisting_SuccessfullyExecuted()
        {
            //Arrange
            var menu = MenuDataFaker.GetFakedMenu();
            ICollection<Menu> menus = new List<Menu> { menu };

            IQueryable<Menu> query = menus.AsQueryable();

            _catalogServiceDbContextMock.MockDataSet(query);

            //Act
            await _menuRepository.DeleteAsync(menu.Id, It.IsAny<CancellationToken>());

            //Assert
            _catalogServiceDbContextMock.Verify();
        }

        [Fact]
        public async Task DeleteMenuAsync_WhenMenuIsNotExisting_ThrowsNotFoundException()
        {
            //Arrange
            var menu = MenuDataFaker.GetFakedMenu();
            int id = menu.Id;
            menu.Id = 0;

            IQueryable<Menu> query = new List<Menu> { menu }.AsQueryable();

            _catalogServiceDbContextMock.MockDataSet(query);

            //Act
            var result = _menuRepository.DeleteAsync(id, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _catalogServiceDbContextMock.Verify();
        }

        [Theory]
        [InlineData(1, true)]
        public async Task SaveChangesToDbAsync_SuccessfullyExecuted(int saved, bool isSaved)
        {
            //Arrange
            _catalogServiceDbContextMock.MockSaveChangesToDbAsync(saved);

            //Act
            var result = await _menuRepository.SaveChangesToDbAsync(It.IsAny<CancellationToken>());

            //Assert
            result.Should().Be(isSaved);
            _catalogServiceDbContextMock.Verify();
        }
    }
}
