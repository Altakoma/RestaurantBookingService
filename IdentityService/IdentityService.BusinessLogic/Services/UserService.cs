using AutoMapper;
using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.BusinessLogic.TokenGenerators;
using IdentityService.DataAccess.DTOs.User;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Exceptions;
using IdentityService.DataAccess.Repositories.Interfaces;

namespace IdentityService.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IRefreshTokenService _refreshTokenService;

        public UserService(IUserRepository userRepository,
            IMapper mapper,
            ITokenGenerator tokenGenerator,
            IRefreshTokenService refreshTokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
            _refreshTokenService = refreshTokenService;
        }

        public async Task DeleteAsync(int id,
            CancellationToken cancellationToken)
        {
            await _userRepository.DeleteAsync(id, cancellationToken);

            bool isDeleted = await _userRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isDeleted)
            {
                throw new DbOperationException(
                    nameof(DeleteAsync), id.ToString(), typeof(User));
            }
        }

        public async Task<ICollection<ReadUserDTO>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadUserDTO> readUserDTOs =
                await _userRepository.GetAllAsync(cancellationToken);

            return readUserDTOs;
        }

        public async Task<ReadUserDTO> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            ReadUserDTO? user = await _userRepository
                                      .GetByIdAsync(id, cancellationToken);

            if (user is null)
            {
                throw new NotFoundException(id.ToString(), typeof(User));
            }

            var readUserDTO = _mapper.Map<ReadUserDTO>(user);

            return readUserDTO;
        }

        public async Task<TokenDTO> GetUserAsync(string login,
            string password, CancellationToken cancellationToken)
        {
            ReadUserDTO? user = await _userRepository
                .GetUserAsync(login, password, cancellationToken);

            if (user is null)
            {
                throw new NotFoundException(login, typeof(User));
            }

            (TokenDTO tokenDTO, RefreshToken refreshToken) = _tokenGenerator
                .GenerateToken(user.Name, user.UserRoleName, user.Id);

            await _refreshTokenService
                  .SaveTokenAsync(refreshToken, cancellationToken);

            _refreshTokenService.RefreshTokenCookie = refreshToken.Token;

            return tokenDTO;
        }

        public async Task<ReadUserDTO> InsertAsync(InsertUserDTO item,
            CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(item);

            user = await _userRepository.InsertAsync(user, cancellationToken);

            bool isInserted = await _userRepository
                                    .SaveChangesToDbAsync(cancellationToken);

            if (!isInserted)
            {
                throw new DbOperationException(
                    nameof(InsertAsync), user.Id.ToString(), typeof(User));
            }

            ReadUserDTO? readUserDTO = await _userRepository
                .GetByIdAsync(user.Id, cancellationToken);

            if (readUserDTO is null)
            {
                throw new NotFoundException(user.Id.ToString(), typeof(User));
            }

            return readUserDTO;
        }

        public async Task<ReadUserDTO> UpdateAsync(int id,
            UpdateUserDTO item, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(item);

            user.Id = id;

            _userRepository.Update(user);

            bool isUpdated = await _userRepository
                                   .SaveChangesToDbAsync(cancellationToken);

            if (!isUpdated)
            {
                throw new DbOperationException(
                    nameof(UpdateAsync), user.Id.ToString(), typeof(User));
            }

            ReadUserDTO? readUserDTO = await _userRepository
                .GetByIdAsync(id, cancellationToken);

            if (readUserDTO is null)
            {
                throw new NotFoundException(id.ToString(), typeof(User));
            }

            return readUserDTO;
        }
    }
}
