using AutoMapper;
using OrderService.Application.DTOs.Client.Messages;
using OrderService.Application.DTOs.Menu;
using OrderService.Application.DTOs.Menu.Messages;
using OrderService.Application.MediatR.Menu.Commands;
using OrderService.Application.MediatR.Order.Commands;
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
                options => options.MapFrom(updateMenuDTO => updateMenuDTO.Cost));

            CreateMap<InsertMenuMessageDTO, Menu>()
                .ForMember(menu => menu.Id,
                options => options.MapFrom(insertClientDTO => insertClientDTO.Id))
                .ForMember(menu => menu.FoodName,
                options => options.MapFrom(insertClientDTO => insertClientDTO.FoodName))
                .ForMember(menu => menu.Cost,
                options => options.MapFrom(insertClientDTO => insertClientDTO.Cost));

            CreateMap<Menu, DeleteMenuCommand>()
                .ForMember(deleteMenuCommand => deleteMenuCommand.Id,
                options => options.MapFrom(menu => menu.Id));

            CreateMap<Menu, Menu>();
        }
    }
}
