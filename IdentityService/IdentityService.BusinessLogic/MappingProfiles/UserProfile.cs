using AutoMapper;
using IdentityService.BusinessLogic.DTOs.UserDTOs;
using IdentityService.DataAccess.Entities;

namespace IdentityService.BusinessLogic.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<InsertUserDTO, User>();
            CreateMap<User, ReadUserDTO>();
            CreateMap<User, UpdateUserDTO>();
        }
    }
}
