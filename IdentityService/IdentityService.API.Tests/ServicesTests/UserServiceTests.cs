using AutoMapper;
using FluentAssertions;
using IdentityService.API.Tests.Fakers;
using IdentityService.API.Tests.Mocks.Generators;
using IdentityService.API.Tests.Mocks.Producers;
using IdentityService.API.Tests.Mocks.Repositories;
using IdentityService.API.Tests.Mocks.Services;
using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.BusinessLogic.DTOs.User.Messages;
using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.MappingProfiles;
using IdentityService.BusinessLogic.Services;
using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Exceptions;
using Moq;

namespace IdentityService.API.Tests.ServicesTests
{
    public class UserServiceTests
    {
        private readonly UserRepositoryMock _userRepositoryMock;
        private readonly JwtTokenGeneratorMock _tokenGeneratorMock;
        private readonly RefreshTokenServiceMock _refreshTokenServiceMock;
        private readonly UserMessageProducerMock _userMessageProducerMock;

        private readonly IMapper _mapper;

        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new();
            _tokenGeneratorMock = new();
            _refreshTokenServiceMock = new();
            _userMessageProducerMock = new();

            _mapper = new Mapper(new MapperConfiguration(
                configure => configure.AddProfile(new UserProfile())));

            var cookieService = new Mock<ICookieService>();

            _userService = new UserService(_userRepositoryMock.Object, _mapper,
                cookieService.Object, _tokenGeneratorMock.Object,
                _refreshTokenServiceMock.Object, _userMessageProducerMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsReadUserDTOs()
        {
            //Arrange
            var readUserDTOs = new List<ReadUserDTO> { UserDataFaker.GetFakedReadUserDTO() };

            _userRepositoryMock.MockGetAllAsync(readUserDTOs);

            //Act
            ICollection<ReadUserDTO> result =
                await _userService.GetAllAsync(It.IsAny<CancellationToken>());

            //Assert
            result.Should().NotBeNull();

            result.Should().BeEquivalentTo(readUserDTOs);

            _userRepositoryMock.Verify();
        }

        [Fact]
        public async Task GetUserByIdAsync_WhenUserIsExisting_ReturnsReadUserDTO()
        {
            //Arrange
            ReadUserDTO readUserDTO = UserDataFaker.GetFakedReadUserDTO();

            _userRepositoryMock.MockGetByIdAsync(readUserDTO.Id, readUserDTO);

            //Act
            var result = await _userService.GetByIdAsync(readUserDTO.Id, It.IsAny<CancellationToken>());

            //Assert
            result.Should().NotBeNull();

            result.Should().BeEquivalentTo(readUserDTO);

            _userRepositoryMock.Verify();
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public async Task GetUserByIdAsync_WhenUserIsNotExisting_ThrowsNotFoundException(int maxId)
        {
            //Arrange
            int id = UserDataFaker.GetRandomNumber(maxId);

            ReadUserDTO? readUserDTO = default;

            _userRepositoryMock.MockGetByIdAsync(id, readUserDTO);

            //Act
            var result = _userService.GetByIdAsync(id, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _userRepositoryMock.Verify();
        }

        [Theory]
        [InlineData("testing_login", "testing_password")]
        public async Task GetUserAsync_WhenUserIsExisting_ReturnsAccessToken(
            string login, string password)
        {
            //Arrange
            ReadUserDTO readUserDTO = UserDataFaker.GetFakedReadUserDTO(login);

            AccessTokenDTO accessTokenDTO = UserDataFaker.GetFakedAccessTokenDTO();

            string refreshToken = UserDataFaker.FakeRandomString(5);

            _userRepositoryMock.MockGetUserAsync(login, password, readUserDTO);

            _tokenGeneratorMock.MockGenerateTokens(readUserDTO.Name, readUserDTO.UserRoleName,
                readUserDTO.Id.ToString(), refreshToken, accessTokenDTO);

            _refreshTokenServiceMock.MockSetAsync(readUserDTO.Id.ToString(), refreshToken);

            //Act
            var result = await _userService.GetUserAsync(login, password, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(accessTokenDTO);

            _userRepositoryMock.Verify();
            _tokenGeneratorMock.Verify();
            _refreshTokenServiceMock.Verify();
        }

        [Theory]
        [InlineData("logfortest", "pwdfortest")]
        public async Task GetUserAsync_WhenUserIsNotExisting_ThrowsNotFoundException(
            string login, string password)
        {
            //Arrange
            ReadUserDTO? readUserDTO = default;

            _userRepositoryMock.MockGetUserAsync(login, password, readUserDTO);

            //Act
            var result = _userService.GetUserAsync(login, password, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(() => result);

            _userRepositoryMock.Verify();
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public async Task DeleteUserAsync_WhenItIsSaved_SuccessfullyExecuted(int maxId)
        {
            //Arrange
            int id = UserDataFaker.GetRandomNumber(maxId);
            bool isDeleted = true;

            var message = new DeleteUserMessageDTO
            {
                Id = id,
            };

            _refreshTokenServiceMock.MockDeleteAsync(id.ToString());

            _userRepositoryMock.MockDeleteAsync(id)
                               .MockSaveChanges(isDeleted);

            _userMessageProducerMock.MockProduceMessageAsync(message);

            //Act
            await _userService.DeleteAsync(id, It.IsAny<CancellationToken>());

            //Assert
            _userRepositoryMock.Verify();
            _refreshTokenServiceMock.Verify();
            _userMessageProducerMock.Verify();
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public async Task DeleteUserAsync_WhenItIsNotSaved_ThrowsDbOperationException(
            int maxId)
        {
            //Arrange
            int id = UserDataFaker.GetRandomNumber(maxId);
            bool isDeleted = false;

            var message = new DeleteUserMessageDTO
            {
                Id = id,
            };

            _refreshTokenServiceMock.MockDeleteAsync(id.ToString());

            _userRepositoryMock.MockDeleteAsync(id)
                               .MockSaveChanges(isDeleted);

            _userMessageProducerMock.MockProduceMessageAsync(message);

            //Act
            var result = _userService.DeleteAsync(id, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<DbOperationException>(() => result);

            _refreshTokenServiceMock.Verify();
            _userRepositoryMock.Verify();
            _userMessageProducerMock.Verify();
        }


        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public async Task UpdateUserAsync_WhenItIsSaved_ReturnsReadUserDTO(int maxId)
        {
            //Arrange
            int id = UserDataFaker.GetRandomNumber(maxId);
            UpdateUserDTO updateUserDTO = UserDataFaker.GetFakedUpdateUserDTO();
            bool isUpdated = true;

            var user = _mapper.Map<User>(updateUserDTO);
            user.Id = id;

            var readUserDTO = _mapper.Map<ReadUserDTO>(user);

            var updateMessage = _mapper.Map<UpdateUserMessageDTO>(user);

            _refreshTokenServiceMock.MockDeleteAsync(id.ToString());

            _userRepositoryMock.MockUpdateAsync(user)
                               .MockGetByIdAsync(id, readUserDTO)
                               .MockSaveChanges(isUpdated);

            _userMessageProducerMock.MockProduceMessageAsync(updateMessage);

            //Act
            var result = await _userService.UpdateAsync(id, updateUserDTO, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readUserDTO);

            _refreshTokenServiceMock.Verify();
            _userRepositoryMock.Verify();
            _userMessageProducerMock.Verify();
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public async Task UpdateUserAsync_WhenItIsNotSaved_ThrowsDbOperationException(int maxId)
        {
            //Arrange
            int id = UserDataFaker.GetRandomNumber(maxId);
            UpdateUserDTO updateUserDTO = UserDataFaker.GetFakedUpdateUserDTO();
            bool isUpdated = false;

            var user = _mapper.Map<User>(updateUserDTO);
            user.Id = id;

            _refreshTokenServiceMock.MockDeleteAsync(id.ToString());

            _userRepositoryMock.MockUpdateAsync(user)
                               .MockSaveChanges(isUpdated);

            //Act
            var result = _userService.UpdateAsync(id, updateUserDTO, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<DbOperationException>(() => result);

            _refreshTokenServiceMock.Verify();
            _userRepositoryMock.Verify();
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public async Task InsertUserAsync_WhenItIsSaved_ReturnsReadUserDTO(int maxId)
        {
            //Arrange
            int id = UserDataFaker.GetRandomNumber(maxId);

            InsertUserDTO insertUserDTO = UserDataFaker.GetFakedInsertUserDTO();
            bool isInserted = true;

            var user = _mapper.Map<User>(insertUserDTO);
            var resultUser = _mapper.Map<User>(insertUserDTO);
            resultUser.Id = id;

            var readUserDTO = _mapper.Map<ReadUserDTO>(resultUser);
            var insertMessageDTO = _mapper.Map<InsertUserMessageDTO>(resultUser);

            _userRepositoryMock.MockInsertAsync(user, resultUser)
                               .MockSaveChanges(isInserted)
                               .MockGetByIdAsync(id, readUserDTO);

            _userMessageProducerMock.MockProduceMessageAsync(insertMessageDTO);

            //Act
            var result = await _userService.InsertAsync(insertUserDTO, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeEquivalentTo(readUserDTO);

            _userRepositoryMock.Verify();
            _userMessageProducerMock.Verify();
        }

        [Theory]
        [InlineData(UserDataFaker.StandartMaximumId)]
        public async Task InsertUserAsync_WhenItIsNotSaved_ThrowsDbOperationException(int maxId)
        {
            //Arrange
            InsertUserDTO insertUserDTO = UserDataFaker.GetFakedInsertUserDTO();
            bool isInserted = false;

            var user = _mapper.Map<User>(insertUserDTO);

            var resultUser = _mapper.Map<User>(insertUserDTO);
            int id = UserDataFaker.GetRandomNumber(maxId);
            resultUser.Id = id;

            var insertMessageDTO = _mapper.Map<InsertUserMessageDTO>(resultUser);

            _userRepositoryMock.MockInsertAsync(user, resultUser)
                               .MockSaveChanges(isInserted);

            //Act
            var result = _userService.InsertAsync(insertUserDTO, It.IsAny<CancellationToken>());

            //Assert
            await Assert.ThrowsAsync<DbOperationException>(() => result);

            _userRepositoryMock.Verify();
        }
    }
}
