using FluentAssertions;
using IdentityService.API.Controllers;
using IdentityService.API.Tests.Fakers;
using IdentityService.API.Tests.Mocks.Services;
using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IdentityService.API.Tests.ControllersTests
{
    public class AuthControllerTests
    {
        private readonly UserServiceMock _userServiceMock;
        private readonly RefreshTokenServiceMock _refreshTokenServiceMock;

        private readonly AuthController _authController;

        public AuthControllerTests()
        {
            _userServiceMock = new();
            _refreshTokenServiceMock = new();

            _authController = new AuthController(_userServiceMock.Object,
                _refreshTokenServiceMock.Object);
        }

        [Fact]
        public async Task RegisterAsync_ReturnsReadUserDTO()
        {
            //Arrange
            var insertUserDTO = new InsertUserDTO();
            ReadUserDTO readUserDTO = UserDataFaker.GetFakedReadUserDTO();

            _userServiceMock.MockInsertAsync(insertUserDTO, readUserDTO);

            //Act
            var result = await _authController.RegisterAsync(
                insertUserDTO, It.IsAny<CancellationToken>());

            var okResult = result as CreatedAtActionResult;
            var readUserDTOResult = okResult?.Value as ReadUserDTO;

            //Assert
            okResult.Should().BeOfType<CreatedAtActionResult>();

            readUserDTOResult.Should().BeEquivalentTo(readUserDTO);

            _userServiceMock.Verify();
        }

        [Fact]
        public async Task LogInAsync_ReturnsAccessToken()
        {
            //Arrange
            var loginDTO = new LoginDTO();
            AccessTokenDTO accessTokenDTO = UserDataFaker.GetFakedAccessTokenDTO();

            _userServiceMock.MockGetUserAsync(loginDTO.Login, loginDTO.Password, accessTokenDTO);

            //Act
            var result = await _authController.LogInAsync(loginDTO, It.IsAny<CancellationToken>());

            var okResult = result as OkObjectResult;
            var accessTokenDTOResult = okResult?.Value as AccessTokenDTO;

            //Assert
            okResult.Should().BeOfType<OkObjectResult>();

            accessTokenDTOResult.Should().BeEquivalentTo(accessTokenDTO);

            _userServiceMock.Verify();
        }

        [Fact]
        public async Task RefreshTokenAsync_ReturnsAccessToken()
        {
            //Arrange
            AccessTokenDTO accessTokenDTO = UserDataFaker.GetFakedAccessTokenDTO();

            _refreshTokenServiceMock.MockVerifyAndGenerateTokenAsync(accessTokenDTO);

            //Act
            var result = await _authController.RefreshTokenAsync(It.IsAny<CancellationToken>());

            var okResult = result as OkObjectResult;
            var accessTokenDTOResult = okResult?.Value as AccessTokenDTO;

            //Assert
            okResult.Should().BeOfType<OkObjectResult>();

            accessTokenDTOResult.Should().BeEquivalentTo(accessTokenDTO);

            _userServiceMock.Verify();
        }
    }
}
