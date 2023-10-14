using AutoMapper;
using IdentityService.BusinessLogic.DTOs.UserRole;
using IdentityService.DataAccess.Entities;

namespace IdentityService.BusinessLogic.MappingProfiles
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRole, ReadUserRoleDTO>()
                .ForMember(
                readUserRoleDTO => readUserRoleDTO.Id,
                configuration => configuration.MapFrom(userRole => userRole.Id))
                .ForMember(
                readUserRoleDTO => readUserRoleDTO.Name,
                configuration => configuration.MapFrom(userRole => userRole.Name));
        }
    }
}
