using AutoMapper;
using FluentAssertions;
using IdentityService.API.Tests.Fakers;
using IdentityService.API.Tests.RepositoriesTests.Base;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.BusinessLogic.MappingProfiles;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Exceptions;
using IdentityService.DataAccess.Repositories;
using IdentityService.DataAccess.Repositories.Interfaces;
using Moq;

namespace IdentityService.API.Tests.RepositoriesTests
{
    public class UserRepositoryTests : BaseRepositoryTests<User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserRepositoryTests() : base()
        {
            _userRepository = new UserRepository(_identityDbContextMock.Object,
                _mapperMock.Object);

            _repository = _userRepository;

            _mapper = new Mapper(new MapperConfiguration(
                configure => configure.AddProfile(new UserProfile())));
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public async Task GetAllAsync_ReturnsReadUserDTOs(int maxId)
        {
            //Arrange
            var users = new List<User> { UserDataFaker.GetFakedUser(maxId) };
            IQueryable<User> userQuery = users.AsQueryable();

            var userReadDTOs = _mapper.Map<List<ReadUserDTO>>(users);

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(userQuery, userReadDTOs);
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public async Task GetAllAsync_ReturnsUsers(int maxId)
        {
            //Arrange
            var users = new List<User> { UserDataFaker.GetFakedUser(maxId) };
            IQueryable<User> userQuery = users.AsQueryable();

            //Act
            //Assert
            await base.GetAllAsync_ReturnsEntities(userQuery, users);
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public async Task GetByIdAsync_ReturnsUser(int maxId)
        {
            //Arrange
            var user = UserDataFaker.GetFakedUser(maxId);
            IQueryable<User> userQuery = new List<User> { user }.AsQueryable();

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(user.Id, userQuery, user);
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public async Task GetUserAsync_ReturnsUser(int maxId)
        {
            //Arrange
            var user = UserDataFaker.GetFakedUser(maxId);
            ICollection<User> users = new List<User> { user };

            IQueryable<User> query = users.AsQueryable();

            _identityDbContextMock.MockDataUsers(query);
            _mapperMock.MockProjectTo(query, users);

            //Act
            var result = await _userRepository.GetUserAsync<User>(user.Login,
                user.Password, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(user);

            _identityDbContextMock.Verify();
            _mapperMock.Verify();
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public async Task GetByIdAsync_ReturnsReadUserDTO(int maxId)
        {
            //Arrange
            var user = UserDataFaker.GetFakedUser(maxId);
            IQueryable<User> userQuery = new List<User> { user }.AsQueryable();

            var readUserDTO = _mapper.Map<ReadUserDTO>(user);

            //Act
            //Assert
            await base.GetByIdAsync_ReturnsEntity(user.Id, userQuery, readUserDTO);
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public async Task InsertUserAsync_SuccessfullyExecuted(int maxId)
        {
            //Arrange
            var user = UserDataFaker.GetFakedUser(maxId);

            //Act
            //Assert
            await base.InsertAsync_SuccessfullyExecuted(user);
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public void UpdateUser_SuccessfullyExecuted(int maxId)
        {
            //Arrange
            var user = UserDataFaker.GetFakedUser(maxId);

            //Act
            //Assert
            base.UpdateEntity_SuccessfullyExecuted(user);
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public void DeleteUser_SuccessfullyExecuted(int maxId)
        {
            //Arrange
            var user = UserDataFaker.GetFakedUser(maxId);

            //Act
            //Assert
            base.DeleteEntity_SuccessfullyExecuted(user);
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public async Task DeleteUserAsync_WhenUserIsExisting_SuccessfullyExecuted(int maxId)
        {
            //Arrange
            var user = UserDataFaker.GetFakedUser(maxId);
            ICollection<User> users = new List<User> { user };

            IQueryable<User> query = users.AsQueryable();

            _identityDbContextMock.MockDataUsers(query);

            //Act
            await _userRepository.DeleteAsync(user.Id, It.IsAny<CancellationToken>());

            //Assert
            _identityDbContextMock.Verify();
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public async Task DeleteUserAsync_WhenUserIsNotExisting_ThrowsNotFoundException(int maxId)
        {
            //Arrange
            var user = UserDataFaker.GetFakedUser(maxId);
            user.Id = 0;

            IQueryable<User> query = new List<User> { user }.AsQueryable();

            _identityDbContextMock.MockDataUsers(query);

            //Act
            var result = _userRepository.DeleteAsync(maxId, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _identityDbContextMock.Verify();
        }

        [Theory]
        [InlineData(1, true)]
        public async Task SaveChangesToDbAsync_SuccessfullyExecuted(int saved, bool isSaved)
        {
            //Arrange
            _identityDbContextMock.MockSaveChangesToDbAsync(saved);

            //Act
            var result = await _userRepository.SaveChangesToDbAsync(It.IsAny<CancellationToken>());

            //Assert
            result.Should().Be(isSaved);
            _identityDbContextMock.Verify();
        }
    }
}
