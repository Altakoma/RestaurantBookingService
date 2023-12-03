using FluentAssertions;
using IdentityService.API.Tests.Fakers;
using IdentityService.API.Tests.Mocks.Repositories;
using IdentityService.BusinessLogic.DTOs.UserRole;
using IdentityService.BusinessLogic.Services;
using IdentityService.DataAccess.Exceptions;
using Moq;

namespace IdentityService.API.Tests.ServicesTests
{
    public class UserRoleServiceTests
    {
        private readonly UserRoleRepositoryMock _userRoleRepositoryMock;

        private readonly UserRoleService _userRoleService;

        public UserRoleServiceTests()
        {
            _userRoleRepositoryMock = new();

            _userRoleService = new UserRoleService(_userRoleRepositoryMock.Object);
        }

        [Fact]
        public async Task GetUserRoleByIdAsync_WhenItIsExisting_ReturnsReadRoleDTO()
        {
            //Arrange
            var readRoleDTO = UserRoleDataFaker.GetFakedReadUserRoleDTO();

            _userRoleRepositoryMock.MockGetByIdAsync(readRoleDTO.Id, readRoleDTO);

            //Act
            var result = await _userRoleService.GetByIdAsync(readRoleDTO.Id, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readRoleDTO);

            _userRoleRepositoryMock.Verify();
        }

        [Fact]
        public async Task GetUserRoleByIdAsync_WhenItIsNotExisting_ThrowsNotFoundException()
        {
            //Arrange
            var readRoleDTO = UserRoleDataFaker.GetFakedReadUserRoleDTO();

            _userRoleRepositoryMock.MockGetByIdAsync(readRoleDTO.Id, default(ReadUserRoleDTO));

            //Act
            var result = _userRoleService.GetByIdAsync(readRoleDTO.Id, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _userRoleRepositoryMock.Verify();
        }

        [Fact]
        public async Task GetAllUserRolesAsync_ReturnsReadRoleDTOs()
        {
            //Arrange
            var readRoleDTOs = new List<ReadUserRoleDTO>
            {
                UserRoleDataFaker.GetFakedReadUserRoleDTO(),
            };

            _userRoleRepositoryMock.MockGetAllAsync(readRoleDTOs);

            //Act
            var result = await _userRoleService.GetAllAsync(It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readRoleDTOs);

            _userRoleRepositoryMock.Verify();
        }
    }
}
