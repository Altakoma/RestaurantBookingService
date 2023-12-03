using FluentAssertions;
using IdentityService.API.Controllers;
using IdentityService.API.Tests.Fakers;
using IdentityService.API.Tests.Mocks.Services;
using IdentityService.BusinessLogic.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IdentityService.API.Tests.ControllersTests
{
    public class UserControllerTests
    {
        private readonly UserServiceMock _userServiceMock;

        private readonly UserController _userController;

        public UserControllerTests()
        {
            _userServiceMock = new();

            _userController = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task GetUserByIdAsync_ReturnsReadUserDTO()
        {
            //Arrange
            ReadUserDTO readUserDTO = UserDataFaker.GetFakedReadUserDTO();

            _userServiceMock.MockGetByIdAsync(readUserDTO.Id, readUserDTO);

            //Act
            var result = await _userController.GetUserByIdAsync(
                readUserDTO.Id, It.IsAny<CancellationToken>());

            var okResult = result as OkObjectResult;
            var readUserDTOResult = okResult?.Value as ReadUserDTO;

            //Assert
            okResult.Should().BeOfType<OkObjectResult>();

            readUserDTOResult.Should().NotBeNull();

            readUserDTOResult.Should().BeEquivalentTo(readUserDTO);

            _userServiceMock.Verify();
        }

        [Fact]
        public async Task GetAllUserAsync_ReturnsReadUserDTOs()
        {
            //Arrange
            var readUserDTOs = new List<ReadUserDTO>
            {
                UserDataFaker.GetFakedReadUserDTO()
            };

            _userServiceMock.MockGetAllAsync(readUserDTOs);

            //Act
            var result = await _userController.GetAllUsersAsync(It.IsAny<CancellationToken>());

            var okResult = result as OkObjectResult;
            var readUserDTOsResult = okResult?.Value as List<ReadUserDTO>;

            //Assert
            okResult.Should().BeOfType<OkObjectResult>();

            readUserDTOsResult.Should().NotBeNull();

            readUserDTOsResult.Should().BeEquivalentTo(readUserDTOs);

            _userServiceMock.Verify();
        }

        [Fact]
        public async Task UpdateUserAsync_ReturnsReadUserDTOs()
        {
            //Arrange
            var updateUserDTO = new UpdateUserDTO();
            ReadUserDTO readUserDTO = UserDataFaker.GetFakedReadUserDTO();

            _userServiceMock.MockUpdateAsync(readUserDTO.Id, updateUserDTO, readUserDTO);

            //Act
            var result = await _userController.UpdateUserAsync(
                readUserDTO.Id, updateUserDTO, It.IsAny<CancellationToken>());

            var okResult = result as OkObjectResult;
            var readUserDTOResult = okResult?.Value as ReadUserDTO;

            //Assert
            okResult.Should().BeOfType<OkObjectResult>();

            readUserDTOResult.Should().NotBeNull();

            readUserDTOResult.Should().BeEquivalentTo(readUserDTO);

            _userServiceMock.Verify();
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public async Task DeleteUserAsync_ReturnsReadUserDTOs(int maxId)
        {
            //Arrange
            int id = UserDataFaker.GetRandomNumber(maxId);

            _userServiceMock.MockDeleteAsync(id);

            //Act
            var result = await _userController.DeleteUserAsync(id, It.IsAny<CancellationToken>());

            var okResult = result as NoContentResult;

            //Assert
            okResult.Should().BeOfType<NoContentResult>();

            _userServiceMock.Verify();
        }
    }
}