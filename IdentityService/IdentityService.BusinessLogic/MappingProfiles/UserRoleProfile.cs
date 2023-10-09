using AutoMapper;
using IdentityService.BusinessLogic.DTOs.UserRoleDTOs;
using IdentityService.DataAccess.Entities;

namespace IdentityService.BusinessLogic.MappingProfiles
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRole, ReadUserRoleDTO>();
        }
    }
}
