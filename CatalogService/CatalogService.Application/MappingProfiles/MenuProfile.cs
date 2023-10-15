using AutoMapper;
using CatalogService.Application.DTOs.Menu;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.MappingProfiles
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, ReadMenuDTO>()
                .ForMember(m => m.FoodTypeDTO, conf => conf.MapFrom(m => m.FoodType))
                .ForMember(m => m.RestaurantName, conf => conf.MapFrom(r => r.Restaurant.Name));
            CreateMap<UpdateMenuDTO, Menu>();
            CreateMap<InsertMenuDTO, Menu>();
        }
    }
}
