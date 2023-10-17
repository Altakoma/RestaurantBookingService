using AutoMapper;
using IdentityService.BusinessLogic.DTOs.Token;
using IdentityService.DataAccess.Entities;

namespace IdentityService.BusinessLogic.MappingProfiles
{
    public class RefreshTokenProfile : Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<RefreshToken, CreationRefreshTokenDTO>()
                .ForMember(
                creationRefreshTokenDTO => creationRefreshTokenDTO.Id,
                configuration => configuration.MapFrom(refreshToken => refreshToken.UserId))
                .ForMember(
                creationRefreshTokenDTO => creationRefreshTokenDTO.Name,
                configuration => configuration.MapFrom(refreshToken => refreshToken.User.Name))
                .ForMember(
                creationRefreshTokenDTO => creationRefreshTokenDTO.UserRoleName,
                configuration => configuration.MapFrom(refreshToken => refreshToken.User.UserRole.Name));
        }
    }
}
