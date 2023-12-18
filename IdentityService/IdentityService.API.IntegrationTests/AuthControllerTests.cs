using FluentAssertions;
using IdentityService.API.Controllers;
using IdentityService.API.Tests.Fakers;
using IdentityService.API.Tests.Mocks.Producers;
using IdentityService.API.Tests.Mocks.Services;
using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.BusinessLogic.DTOs.User.Messages;
using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.DataAccess.DatabaseContext;
using IdentityService.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.API.IntegrationTests
{
    public class AuthControllerTests : IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly IServiceScope _serviceScope;

        private readonly AuthController _authController;

        private readonly CancellationTokenSource _cancellationTokenSource;

        private readonly IdentityDbContext _identityDbContext;

        private readonly CookieServiceMock _cookieServiceMock;

        private readonly UserMessageProducerMock _userMessageProducerMock;

        public AuthControllerTests(IntegrationTestWebAppFactory factory)
        {
            _serviceScope = factory.Services.CreateScope();

            _userMessageProducerMock = factory.UserMessageProducerMock;

            _cookieServiceMock = factory.CookieServiceMock;

            _authController = _serviceScope.ServiceProvider
                .GetRequiredService<AuthController>();

            _identityDbContext = _serviceScope.ServiceProvider
                .GetRequiredService<IdentityDbContext>();

            _cancellationTokenSource = new CancellationTokenSource();
        }

        [Fact]
        public async Task RegisterUserAsync_ReturnsReadUserDTO()
        {
            //Arrange
            InsertUserDTO insertUserDTO = UserDataFaker.GetFakedInsertUserDTO();

            var cancellationToken = _cancellationTokenSource.Token;

            //Act
            var result = await _authController.RegisterAsync(insertUserDTO,
                cancellationToken);

            var createdAtActionResult = result as CreatedAtActionResult;

            //Assert
            createdAtActionResult.Should().NotBeNull();

            createdAtActionResult!.Value.Should().BeEquivalentTo(insertUserDTO, options =>
                options.ExcludingNestedObjects().ExcludingMissingMembers());
        }

        [Fact]
        public async Task LoginUserAsync_AccessTokenDTO()
        {
            //Arrange
            Environment.SetEnvironmentVariable("JWTExpirationTime", "00:09:59");
            Environment.SetEnvironmentVariable("JWTSecret", "secretsfmnoiagdhvsfd");

            User user = UserDataFaker.GetFakedUser();

            _identityDbContext.Users.Add(user);
            _identityDbContext.SaveChanges();

            _identityDbContext.ChangeTracker.Clear();

            var cancellationToken = _cancellationTokenSource.Token;

            var loginDTO = new LoginDTO
            {
                Login = user.Login,
                Password = user.Password,
            };

            //Act
            var result = await _authController.LogInAsync(loginDTO,
                cancellationToken);

            var okResult = result as OkObjectResult;

            var accessToken = result as AccessTokenDTO;

            //Assert
            okResult?.Should().NotBeNull();

            accessToken?.EncodedToken.Should().NotBeNull();
        }

        [Fact]
        public async Task RefreshTokenAsync_ReturnsAccessTokenDTO()
        {
            //Arrange
            User user = UserDataFaker.GetFakedUser();

            _identityDbContext.Users.Add(user);
            _identityDbContext.SaveChanges();

            _identityDbContext.ChangeTracker.Clear();

            string refreshToken = "refreshToken";
            int time = 600;

            var cancellationToken = _cancellationTokenSource.Token;

            _cookieServiceMock.MockGetCookieValue("UserId", user.Id.ToString());

            _cookieServiceMock.MockGetCookieValue("RefreshToken", refreshToken);

            var refreshTokenService = _serviceScope.ServiceProvider
                .GetRequiredService<IRefreshTokenService>();

            await refreshTokenService.SetAsync(user.Id.ToString(), refreshToken,
                time, cancellationToken);

            _cookieServiceMock.MockSetCookieValue("RefreshToken");

            Environment.SetEnvironmentVariable("JWTExpirationTime", "00:09:59");
            Environment.SetEnvironmentVariable("JWTSecret", "secretsfmnoiagdhvsfd");

            //Act
            var result = await _authController.RefreshTokenAsync(
                cancellationToken);

            var okResult = result as OkObjectResult;

            var accessToken = result as AccessTokenDTO;

            //Assert
            okResult?.Should().NotBeNull();

            accessToken?.EncodedToken.Should().NotBeNull();

            _cookieServiceMock.Verify();
        }
    }
}
