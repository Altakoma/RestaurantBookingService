using AutoMapper;
using OrderService.Application.DTOs.Menu;
using OrderService.Application.DTOs.Menu.Messages;
using OrderService.Domain.Entities;

namespace OrderService.Application.MappingProfiles
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, ReadMenuDTO>()
                .ForMember(readMenuDTO => readMenuDTO.Id,
                options => options.MapFrom(menu => menu.Id))
                .ForMember(readMenuDTO => readMenuDTO.FoodName,
                options => options.MapFrom(menu => menu.FoodName))
                .ReverseMap();

            CreateMap<UpdateMenuMessageDTO, Menu>()
                .ForMember(menu => menu.Id,
                options => options.MapFrom(updateMenuDTO => updateMenuDTO.Id))
                .ForMember(menu => menu.FoodName,
                options => options.MapFrom(updateMenuDTO => updateMenuDTO.FoodName))
                .ForMember(menu => menu.Cost,
                options => options.MapFrom(updateMenuDTO => updateMenuDTO.Cost))
                .ForMember(menu => menu.Orders,
                options => options.Ignore());

            CreateMap<InsertMenuMessageDTO, Menu>()
                .ForMember(menu => menu.Id,
                options => options.MapFrom(insertClientDTO => insertClientDTO.Id))
                .ForMember(menu => menu.FoodName,
                options => options.MapFrom(insertClientDTO => insertClientDTO.FoodName))
                .ForMember(menu => menu.Cost,
                options => options.MapFrom(insertClientDTO => insertClientDTO.Cost))
                .ForMember(menu => menu.Orders,
                options => options.Ignore());

            CreateMap<Menu, Menu>();
        }
    }
}
