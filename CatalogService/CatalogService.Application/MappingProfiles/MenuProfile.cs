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
                configuration => configuration.MapFrom(updateMenuDTO => updateMenuDTO.FoodTypeId));

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
                configuration => configuration.MapFrom(insertMenuDTO => insertMenuDTO.FoodTypeId));

            CreateMap<Menu, Menu>();
        }
    }
}
