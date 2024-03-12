using FluentAssertions;
using IdentityService.API.Controllers;
using IdentityService.API.Tests.Fakers;
using IdentityService.API.Tests.Mocks.Services;
using IdentityService.BusinessLogic.DTOs.UserRole;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IdentityService.API.Tests.ControllersTests
{
    public class UserRoleControllerTests
    {
        private readonly UserRoleServiceMock _userRoleServiceMock;

        private readonly UserRoleController _userRoleController;

        public UserRoleControllerTests()
        {
            _userRoleServiceMock = new();

            _userRoleController = new UserRoleController(_userRoleServiceMock.Object);
        }

        [Fact]
        public async Task GetUserRoleByIdAsync_ReturnsReadUserRoleDTO()
        {
            //Arrange
            ReadUserRoleDTO readUserRoleDTO = UserDataFaker.GetFakedReadUserRoleDTO();

            _userRoleServiceMock.MockGetByIdAsync(readUserRoleDTO.Id, readUserRoleDTO);

            //Act
            var result = await _userRoleController.GetUserRoleByIdAsync(
                readUserRoleDTO.Id, It.IsAny<CancellationToken>());

            var okResult = result as OkObjectResult;
            var readUserRoleDTOResult = okResult?.Value as ReadUserRoleDTO;

            //Assert
            okResult.Should().BeOfType<OkObjectResult>();

            readUserRoleDTOResult.Should().NotBeNull();

            readUserRoleDTOResult.Should().BeEquivalentTo(readUserRoleDTO);

            _userRoleServiceMock.Verify();
        }

        [Fact]
        public async Task GetAllUserRolesAsync_ReturnsReadUserRoleDTOs()
        {
            //Arrange
            var readUserRoleDTOs = new List<ReadUserRoleDTO>
            {
                UserDataFaker.GetFakedReadUserRoleDTO()
            };

            _userRoleServiceMock.MockGetAlldAsync(readUserRoleDTOs);

            //Act
            var result = await _userRoleController.GetAllUserRolesAsync(It.IsAny<CancellationToken>());

            var okResult = result as OkObjectResult;
            var readUserRoleDTOsResult = okResult?.Value as List<ReadUserRoleDTO>;

            //Assert
            okResult.Should().BeOfType<OkObjectResult>();

            readUserRoleDTOsResult.Should().NotBeNull();

            readUserRoleDTOsResult.Should().BeEquivalentTo(readUserRoleDTOsResult);

            _userRoleServiceMock.Verify();
        }
    }
}
