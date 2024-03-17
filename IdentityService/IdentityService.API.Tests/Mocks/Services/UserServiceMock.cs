using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.BusinessLogic.Services.Interfaces;
using Moq;

namespace IdentityService.API.Tests.Mocks.Services
{
    public class UserServiceMock : Mock<IUserService>
    {
        public UserServiceMock MockGetByIdAsync(int id, ReadUserDTO readUserDTO)
        {
            Setup(userService => userService.GetByIdAsync(
                id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(readUserDTO)
            .Verifiable();

            return this;
        }

        public UserServiceMock MockGetAllAsync(ICollection<ReadUserDTO> readUserDTOs)
        {
            Setup(userService => userService.GetAllAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(readUserDTOs)
            .Verifiable();

            return this;
        }

        public UserServiceMock MockUpdateAsync(int id, UpdateUserDTO updateUserDTO,
            ReadUserDTO readUserDTO)
        {
            Setup(userService => userService.UpdateAsync(
                id, updateUserDTO, It.IsAny<CancellationToken>()))
            .ReturnsAsync(readUserDTO)
            .Verifiable();

            return this;
        }

        public UserServiceMock MockDeleteAsync(int id)
        {
            Setup(userService => userService.DeleteAsync(
                id, It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }

        public UserServiceMock MockInsertAsync(InsertUserDTO insertUserDTO,
            ReadUserDTO readUserDTO)
        {
            Setup(userService => userService.InsertAsync(
                insertUserDTO, It.IsAny<CancellationToken>()))
            .ReturnsAsync(readUserDTO)
            .Verifiable();

            return this;
        }

        public UserServiceMock MockGetUserAsync(string login, string password,
            AccessTokenDTO accessToken)
        {
            Setup(userService => userService.GetUserAsync(
                login, password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(accessToken)
            .Verifiable();

            return this;
        }
    }
}
