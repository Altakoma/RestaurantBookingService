﻿using AutoMapper;
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
            var userRoles = await _userRoleRepository
                                  .GetAllAsync(cancellationToken);

            var readUserRoleDTOs = _mapper
                                   .Map<ICollection<ReadUserRoleDTO>>(userRoles);

            return readUserRoleDTOs;
        }

        public async Task<ReadUserRoleDTO> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            var userRole = await _userRoleRepository
                                 .GetByIdAsync(id, cancellationToken);

            if (userRole is null)
            {
                throw new NotFoundException(id.ToString(), typeof(UserRole));
            }

            var readUserRoleDTO = _mapper.Map<ReadUserRoleDTO>(userRole);

            return readUserRoleDTO;
        }
    }
}
