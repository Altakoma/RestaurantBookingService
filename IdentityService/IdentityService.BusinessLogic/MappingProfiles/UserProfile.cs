using AutoMapper;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.DataAccess.Entities;

namespace IdentityService.BusinessLogic.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UpdateUserDTO, User>();
            CreateMap<InsertUserDTO, User>();
            CreateMap<User, ReadUserDTO>();
            CreateMap<User, UpdateUserDTO>();
        }
    }
}
