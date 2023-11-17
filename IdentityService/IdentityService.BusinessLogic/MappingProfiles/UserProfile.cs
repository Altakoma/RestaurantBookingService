using AutoMapper;
using IdentityService.BusinessLogic.DTOs.User;
using IdentityService.BusinessLogic.DTOs.User.Messages;
using IdentityService.DataAccess.Entities;

namespace IdentityService.BusinessLogic.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UpdateUserDTO>()
                .ForMember(
                updateUserDTO => updateUserDTO.UserRoleId,
                configuration => configuration.MapFrom(user => user.UserRoleId))
                .ForMember(
                updateUserDTO => updateUserDTO.Name,
                configuration => configuration.MapFrom(user => user.Name))
                .ForMember(
                updateUserDTO => updateUserDTO.Login,
                configuration => configuration.MapFrom(user => user.Login))
                .ForMember(
                updateUserDTO => updateUserDTO.Password,
                configuration => configuration.MapFrom(user => user.Password))
                .ReverseMap();

            CreateMap<User, UpdateUserMessageDTO>()
                .ForMember(
                updateUserDTO => updateUserDTO.Id,
                configuration => configuration.MapFrom(user => user.Id))
                .ForMember(
                updateUserDTO => updateUserDTO.Name,
                configuration => configuration.MapFrom(user => user.Name));

            CreateMap<User, InsertUserMessageDTO>()
                .ForMember(
                updateUserDTO => updateUserDTO.Id,
                configuration => configuration.MapFrom(user => user.Id))
                .ForMember(
                updateUserDTO => updateUserDTO.Name,
                configuration => configuration.MapFrom(user => user.Name));

            CreateMap<InsertUserDTO, User>()
                .ForMember(
                user => user.UserRoleId,
                configuration => configuration.MapFrom(insertUserDTO => insertUserDTO.UserRoleId))
                .ForMember(
                user => user.Name,
                configuration => configuration.MapFrom(insertUserDTO => insertUserDTO.Name))
                .ForMember(
                user => user.Login,
                configuration => configuration.MapFrom(insertUserDTO => insertUserDTO.Login))
                .ForMember(
                user => user.Password,
                configuration => configuration.MapFrom(insertUserDTO => insertUserDTO.Password));

            CreateMap<User, ReadUserDTO>()
                .ForMember(
                readUserDTO => readUserDTO.Id,
                configuration => configuration.MapFrom(user => user.Id))
                .ForMember(
                readUserDTO => readUserDTO.Name,
                configuration => configuration.MapFrom(user => user.Name))
                .ForMember(
                readUserDTO => readUserDTO.Login,
                configuration => configuration.MapFrom(user => user.Login))
                .ForMember(
                readUserDTO => readUserDTO.Password,
                configuration => configuration.MapFrom(user => user.Password))
                .ForMember(
                readUserDTO => readUserDTO.UserRoleName,
                configuration => configuration.MapFrom(user => user.UserRole.Name));

            CreateMap<User, User>();
        }
    }
}
