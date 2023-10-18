using AutoMapper;
using IdentityService.BusinessLogic.DTOs.UserRole;
using IdentityService.BusinessLogic.Services.Interfaces;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Exceptions;
using IdentityService.DataAccess.Repositories.Interfaces;

namespace IdentityService.BusinessLogic.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;

        public UserRoleService(IUserRoleRepository userRoleRepository,
            IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<ReadUserRoleDTO>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadUserRoleDTO> readUserRoleDTOs = await _userRoleRepository
                                  .GetAllAsync<ReadUserRoleDTO>(cancellationToken);

            return readUserRoleDTOs;
        }

        public async Task<ReadUserRoleDTO> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            ReadUserRoleDTO? readUserRoleDTO = await _userRoleRepository
                .GetByIdAsync<ReadUserRoleDTO>(id, cancellationToken);

            if (readUserRoleDTO is null)
            {
                throw new NotFoundException(id.ToString(), typeof(UserRole));
            }

            return readUserRoleDTO;
        }
    }
}
