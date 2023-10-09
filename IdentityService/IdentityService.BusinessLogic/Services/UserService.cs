using AutoMapper;
using IdentityService.BusinessLogic.DTOs.TokenDTOs;
using IdentityService.BusinessLogic.DTOs.UserDTOs;
using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.BusinessLogic.TokenGenerators;
using IdentityService.DataAccess.Entities;
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
            IMapper mapper, ITokenGenerator tokenGenerator,
            IRefreshTokenService refreshTokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
            _refreshTokenService = refreshTokenService;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            bool isDeleted;

            if (user is not null)
            {
                isDeleted = await _userRepository.DeleteAsync(user);
            }
            else
            {
                throw new NotFoundException(id.ToString(), typeof(User));
            }

            if (!isDeleted)
            {
                throw new DbOperationException(
                    nameof(DeleteAsync), id.ToString(), typeof(User));
            }
        }

        public async Task<ICollection<ReadUserDTO>?> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            var readUserDTOs = _mapper.Map<ICollection<ReadUserDTO>>(users);

            return readUserDTOs;
        }

        public async Task<ReadUserDTO?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user is null)
            {
                throw new NotFoundException(id.ToString(), typeof(User));
            }

            var readUserDTO = _mapper.Map<ReadUserDTO>(user);

            return readUserDTO;
        }

        public async Task<TokensDTO> GetUserAsync(string login, string password)
        {
            var user = await _userRepository.GetUserAsync(login, password);

            if (user is null)
            {
                throw new NotFoundException(login, typeof(User));
            }

            (var tokenDTO, var refreshToken) = _tokenGenerator
                .GenerateToken(user.Name, user.UserRole.Name, user.Id);

            await _refreshTokenService.SaveToken(refreshToken);

            return tokenDTO;
        }

        public async Task<ReadUserDTO> InsertAsync(InsertUserDTO item)
        {
            var user = _mapper.Map<User>(item);

            (user, var isInserted) = await _userRepository.InsertAsync(user);

            if (!isInserted)
            {
                throw new DbOperationException(
                    nameof(InsertAsync), user.Id.ToString(), typeof(User));
            }

            user = await _userRepository.GetByIdAsync(user.Id);

            var readUserDTO = _mapper.Map<ReadUserDTO>(user);

            return readUserDTO;
        }

        public async Task<ReadUserDTO> UpdateAsync(int id, UpdateUserDTO item)
        {
            var user = _mapper.Map<User>(item);

            user.Id = id;

            var isUpdated = await _userRepository.UpdateAsync(user);

            if (!isUpdated)
            {
                throw new DbOperationException(
                    nameof(UpdateAsync), user.Id.ToString(), typeof(User));
            }

            user = await _userRepository.GetByIdAsync(id);

            var readUserDTO = _mapper.Map<ReadUserDTO>(user);

            return readUserDTO;
        }
    }
}
