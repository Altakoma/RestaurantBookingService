using IdentityService.DataAccess.Repositories.Interfaces;
using Moq;

namespace IdentityService.API.Tests.Mocks.Repositories
{
    public class UserRoleRepositoryMock : Mock<IUserRoleRepository>
    {
        public UserRoleRepositoryMock MockGetByIdAsync<T>(int id, T userRoleDTO)
        {
            Setup(userRoleRepository => userRoleRepository.GetByIdAsync<T>(
                id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userRoleDTO)
            .Verifiable();

            return this;
        }

        public UserRoleRepositoryMock MockGetAllAsync<T>(ICollection<T> userRoleDTOs)
        {
            Setup(userRoleRepository =>
                userRoleRepository.GetAllAsync<T>(It.IsAny<CancellationToken>()))
            .ReturnsAsync(userRoleDTOs)
            .Verifiable();

            return this;
        }
    }
}
