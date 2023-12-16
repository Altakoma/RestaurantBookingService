using AutoMapper;
using FluentAssertions;
using IdentityService.API.Controllers;
using IdentityService.API.Tests.Fakers;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.BusinessLogic.KafkaMessageBroker.Interfaces.Producers;
using IdentityService.DataAccess.DatabaseContext;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Exceptions;
using IdentityService.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace IdentityService.API.IntegrationTests
{
    public class UserControllerTests : IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly IServiceScope _serviceScope;

        private readonly UserController _userController;

        private readonly CancellationTokenSource _cancellationTokenSource;

        private readonly IdentityDbContext _identityDbContext;

        private readonly IMapper _mapper;

        private readonly Mock<IUserMessageProducer> _userMessageProducerMock;

        public UserControllerTests(IntegrationTestWebAppFactory factory)
        {
            _userMessageProducerMock = factory.UserMessageProducerMock;

            _serviceScope = factory.Services.CreateScope();

            _mapper = _serviceScope.ServiceProvider
                .GetRequiredService<IMapper>();

            _userController = _serviceScope.ServiceProvider
                .GetRequiredService<UserController>();

            _identityDbContext = _serviceScope.ServiceProvider
                .GetRequiredService<IdentityDbContext>();

            _cancellationTokenSource = new CancellationTokenSource();
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsReadUserRoleDTOs()
        {
            //Arrange
            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _userController.GetAllUsersAsync(cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult!.Value.Should().BeOfType<List<ReadUserDTO>>();
        }

        [Fact]
        public async Task GetByIdUserAsync_ReturnsReadUserRoleDTO()
        {
            //Arrange
            User user = UserDataFaker.GetFakedUser();

            _identityDbContext.Users.Add(user);
            _identityDbContext.SaveChanges();

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _userController.GetUserByIdAsync(user.Id,
                cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult!.Value.Should().BeEquivalentTo(user, options =>
                options.ExcludingNestedObjects().ExcludingMissingMembers());
        }

        [Theory]
        [InlineData(-1)]
        public async Task GetByIdUserAsync_WhenItIsNotExisting_ThrowsNotFoundException(
            int id)
        {
            //Arrange
            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = _userController.GetUserByIdAsync(id,
                cancellationToken);

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);
        }

        [Theory]
        [InlineData(3)]
        public async Task DeleteUserAsync_ReturnsNoContent(int id)
        {
            //Arrange
            var cancellationToken = _cancellationTokenSource.Token;

            var userRepository = _serviceScope.ServiceProvider
                .GetRequiredService<IUserRepository>();

            //Act
            var result = await _userController.DeleteUserAsync(id, cancellationToken);

            var noContentResult = result as NoContentResult;

            var readUserDTO = await userRepository
                                    .GetByIdAsync<ReadUserDTO>(id, cancellationToken);

            //Assert
            noContentResult.Should().NotBeNull();

            readUserDTO.Should().BeNull();
        }

        [Theory]
        [InlineData(-1)]
        public async Task DeleteUserAsync_WhenItIsNotFound_ThrowsNotFoundException(int id)
        {
            //Arrange
            var cancellationToken = _cancellationTokenSource.Token;

            var userRepository = _serviceScope.ServiceProvider
                .GetRequiredService<IUserRepository>();

            //Act
            var result = _userController.DeleteUserAsync(id, cancellationToken);

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);
        }

        [Fact]
        public async Task UpdateUserAsync_ReturnsReadUserDTO()
        {
            //Arrange
            User user = UserDataFaker.GetFakedUser();

            _identityDbContext.Users.Add(user);
            _identityDbContext.SaveChanges();

            _identityDbContext.ChangeTracker.Clear();

            int userId = user.Id;

            UpdateUserDTO updateUserDTO = UserDataFaker.GetFakedUpdateUserDTO();
            user = _mapper.Map<User>(updateUserDTO);
            user.Id = userId;

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _userController.UpdateUserAsync(userId,
                updateUserDTO, cancellationToken);

            var okResult = result as OkObjectResult;

            //Assert
            okResult.Should().NotBeNull();

            okResult!.Value.Should().BeEquivalentTo(user, options =>
                options.ExcludingNestedObjects().ExcludingMissingMembers());

            _userMessageProducerMock.Verify();
        }
    }
}
