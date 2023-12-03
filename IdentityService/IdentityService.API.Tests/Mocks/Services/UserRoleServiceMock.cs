using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.BusinessLogic.DTOs.UserRole;
using IdentityService.BusinessLogic.Services.Interfaces;
using Moq;

namespace IdentityService.API.Tests.Mocks.Services
{
    public class UserRoleServiceMock : Mock<IUserRoleService>
    {
        public UserRoleServiceMock MockGetByIdAsync(int id, ReadUserRoleDTO readUserRoleDTO)
        {
            Setup(userRoleService => userRoleService.GetByIdAsync(
                id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(readUserRoleDTO)
            .Verifiable();

            return this;
        }

        public UserRoleServiceMock MockGetAlldAsync(ICollection<ReadUserRoleDTO> readUserRoleDTOs)
        {
            Setup(userRoleService => userRoleService.GetAllAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(readUserRoleDTOs)
            .Verifiable();

            return this;
        }
    }
}
