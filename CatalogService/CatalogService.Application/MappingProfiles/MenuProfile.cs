using AutoMapper;
using CatalogService.Application.DTOs.Menu;
using CatalogService.Application.DTOs.Menu.Messages;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.MappingProfiles
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, ReadMenuDTO>()
                .ForMember(
                readMenuDTO => readMenuDTO.FoodTypeDTO, 
                configuration => configuration.MapFrom(menu => menu.FoodType))
                .ForMember(
                readMenuDTO => readMenuDTO.RestaurantName, 
                configuration => configuration.MapFrom(menu => menu.Restaurant.Name))
                .ForMember(
                readMenuDTO => readMenuDTO.Cost,
                configuration => configuration.MapFrom(menu => menu.Cost))
                .ForMember(
                readMenuDTO => readMenuDTO.Id,
                configuration => configuration.MapFrom(menu => menu.Id))
                .ForMember(
                readMenuDTO => readMenuDTO.FoodName,
                configuration => configuration.MapFrom(menu => menu.FoodName));

            CreateMap<ReadMenuDTO, InsertMenuMessageDTO>()
                .ForMember(
                insertMenuDTO => insertMenuDTO.Cost,
                configuration => configuration.MapFrom(menu => menu.Cost))
                .ForMember(
                insertMenuDTO => insertMenuDTO.Id,
                configuration => configuration.MapFrom(menu => menu.Id))
                .ForMember(
                insertMenuDTO => insertMenuDTO.FoodName,
                configuration => configuration.MapFrom(menu => menu.FoodName));

            CreateMap<ReadMenuDTO, UpdateMenuMessageDTO>()
                .ForMember(
                updateMenuDTO => updateMenuDTO.Cost,
                configuration => configuration.MapFrom(menu => menu.Cost))
                .ForMember(
                updateMenuDTO => updateMenuDTO.Id,
                configuration => configuration.MapFrom(menu => menu.Id))
                .ForMember(
                updateMenuDTO => updateMenuDTO.FoodName,
                configuration => configuration.MapFrom(menu => menu.FoodName))
            .ReverseMap();

            CreateMap<UpdateMenuDTO, Menu>()
                .ForMember(
                menu => menu.FoodName,
                configuration => configuration.MapFrom(updateMenuDTO => updateMenuDTO.FoodName))
                .ForMember(
                menu => menu.RestaurantId,
                configuration => configuration.MapFrom(updateMenuDTO => updateMenuDTO.RestaurantId))
                .ForMember(
                menu => menu.Cost,
                configuration => configuration.MapFrom(updateMenuDTO => updateMenuDTO.Cost))
                .ForMember(
                menu => menu.FoodTypeId,
                configuration => configuration.MapFrom(updateMenuDTO => updateMenuDTO.FoodTypeId))
            .ReverseMap();

            CreateMap<InsertMenuDTO, Menu>()
                .ForMember(
                menu => menu.FoodName,
                configuration => configuration.MapFrom(insertMenuDTO => insertMenuDTO.FoodName))
                .ForMember(
                menu => menu.RestaurantId,
                configuration => configuration.MapFrom(insertMenuDTO => insertMenuDTO.RestaurantId))
                .ForMember(
                menu => menu.Cost,
                configuration => configuration.MapFrom(insertMenuDTO => insertMenuDTO.Cost))
                .ForMember(
                menu => menu.FoodTypeId,
                configuration => configuration.MapFrom(insertMenuDTO => insertMenuDTO.FoodTypeId))
            .ReverseMap();

            CreateMap<Menu, Menu>();

            CreateMap<Menu, MenuDTO>();
        }
    }
}
