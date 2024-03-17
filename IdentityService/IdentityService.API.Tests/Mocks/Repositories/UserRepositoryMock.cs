using FluentAssertions;
using IdentityService.API.Tests.Mocks.Services;
using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.BusinessLogic.DTOs.User.Messages;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories;
using IdentityService.DataAccess.Repositories.Interfaces;
using Moq;

namespace IdentityService.API.Tests.Mocks.Repositories
{
    public class UserRepositoryMock : Mock<IUserRepository>
    {
        public UserRepositoryMock MockGetByIdAsync<T>(int id, T userDTO)
        {
            Setup(userRepository => userRepository.GetByIdAsync<T>(
                id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userDTO)
            .Verifiable();

            return this;
        }

        public UserRepositoryMock MockGetAllAsync<T>(ICollection<T> userDTOs)
        {
            Setup(userRepository =>
                userRepository.GetAllAsync<T>(It.IsAny<CancellationToken>()))
            .ReturnsAsync(userDTOs)
            .Verifiable();

            return this;
        }

        public UserRepositoryMock MockUpdateAsync(User user)
        {
            Setup(userRepository => userRepository.Update(It.Is<User>(
                currentUser => currentUser.Should().BeEquivalentTo(user, string.Empty) != null)))
            .Verifiable();

            return this;
        }

        public UserRepositoryMock MockDeleteAsync(int id)
        {
            Setup(userRepository => userRepository.DeleteAsync(
                id, It.IsAny<CancellationToken>()))
            .Verifiable();

            return this;
        }

        public UserRepositoryMock MockInsertAsync(User user, User resultUser)
        {
            Setup(userRepository => userRepository.InsertAsync(It.Is<User>(
                currentUser => currentUser.Should().BeEquivalentTo(user, string.Empty) != null),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultUser)
                .Verifiable();

            return this;
        }

        public UserRepositoryMock MockGetUserAsync<T>(string login, string password, T userDTO)
        {
            Setup(userRepository =>
                userRepository.GetUserAsync<T>(login, password, It.IsAny<CancellationToken>()))
            .ReturnsAsync(userDTO)
            .Verifiable();

            return this;
        }

        public UserRepositoryMock MockSaveChanges(bool isSaved)
        {
            Setup(userRepository =>
                userRepository.SaveChangesToDbAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(isSaved)
            .Verifiable();

            return this;
        }
    }
}
