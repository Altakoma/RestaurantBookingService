using AutoMapper;
using IdentityService.BusinessLogic.DTOs.UserDTOs;
using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Interfaces;

namespace IdentityService.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<string> DeleteAsync(int id)
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

            return $"User having {id} key has been succesfully deleted";
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

        public async Task<string> InsertAsync(InsertUserDTO item)
        {
            var user = _mapper.Map<User>(item);

            var isInserted = await _userRepository.InsertAsync(user);

            if (isInserted)
            {
                throw new DbOperationException(
                    nameof(InsertAsync), user.Id.ToString(), typeof(User));
            }

            return $"User having {user.Id} key has been succesfully inserted";
        }

        public async Task<string> UpdateAsync(UpdateUserDTO item)
        {
            var user = _mapper.Map<User>(item);

            var isUpdated = await _userRepository.UpdateAsync(user);

            if (isUpdated)
            {
                throw new DbOperationException(
                    nameof(UpdateAsync), user.Id.ToString(), typeof(User));
            }

            return $"User having {user.Id} key has been succesfully updated";
        }
    }
}
