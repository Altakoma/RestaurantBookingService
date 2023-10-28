using AutoMapper;
using OrderService.Application.DTOs.Menu;
using OrderService.Application.MediatR.Menu.Commands;
using OrderService.Domain.Entities;

namespace OrderService.Application.MappingProfiles
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<InsertMenuCommand, Menu>()
                .ForMember(menu => menu.Id,
                options => options.MapFrom(insertMenuCommand => insertMenuCommand.Id))
                .ForMember(menu => menu.FoodName,
                options => options.MapFrom(insertMenuCommand => insertMenuCommand.FoodName));

            CreateMap<UpdateMenuCommand, Menu>()
                .ForMember(menu => menu.Id,
                options => options.MapFrom(updateMenuCommand => updateMenuCommand.Id))
                .ForMember(menu => menu.FoodName,
                options => options.MapFrom(updateMenuCommand => updateMenuCommand.FoodName));

            CreateMap<Menu, ReadMenuDTO>()
                .ForMember(readMenuDTO => readMenuDTO.Id,
                options => options.MapFrom(client => client.Id))
                .ForMember(readMenuDTO => readMenuDTO.FoodName,
                options => options.MapFrom(client => client.FoodName));
        }
    }
}
