using AutoMapper;
using CatalogService.Application.DTOs.Base.Messages;
using CatalogService.Application.DTOs.Restaurant;
using CatalogService.Application.DTOs.Restaurant.Messages;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.MappingProfiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, ReadRestaurantDTO>()
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.Id,
                configuration => configuration.MapFrom(restaurant => restaurant.Id))
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.Name,
                configuration => configuration.MapFrom(restaurant => restaurant.Name))
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.Street,
                configuration => configuration.MapFrom(restaurant => restaurant.Street))
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.House,
                configuration => configuration.MapFrom(restaurant => restaurant.House))
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.City,
                configuration => configuration.MapFrom(restaurant => restaurant.City));

            CreateMap<ReadRestaurantDTO, InsertRestaurantMessageDTO>()
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.Id,
                configuration => configuration.MapFrom(restaurant => restaurant.Id))
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.Name,
                configuration => configuration.MapFrom(restaurant => restaurant.Name))
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.Type,
                configuration => configuration.MapFrom(restaurant => MessageType.Insert));

            CreateMap<ReadRestaurantDTO, UpdateRestaurantMessageDTO>()
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.Id,
                configuration => configuration.MapFrom(restaurant => restaurant.Id))
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.Name,
                configuration => configuration.MapFrom(restaurant => restaurant.Name))
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.Type,
                configuration => configuration.MapFrom(restaurant => MessageType.Update));

            CreateMap<UpdateRestaurantDTO, Restaurant>()
                .ForMember(
                restaurant => restaurant.Name,
                configuration => configuration.MapFrom(updateRestaurantDTO => updateRestaurantDTO.Name))
                .ForMember(
                restaurant => restaurant.Street,
                configuration => configuration.MapFrom(updateRestaurantDTO => updateRestaurantDTO.Street))
                .ForMember(
                restaurant => restaurant.House,
                configuration => configuration.MapFrom(updateRestaurantDTO => updateRestaurantDTO.House))
                .ForMember(
                restaurant => restaurant.City,
                configuration => configuration.MapFrom(updateRestaurantDTO => updateRestaurantDTO.City))
                .ForMember(
                restaurant => restaurant.Employees,
                configuration => configuration.Ignore())
                .ForMember(
                restaurant => restaurant.Menu,
                configuration => configuration.Ignore())
                .ForMember(
                restaurant => restaurant.Id,
                configuration => configuration.Ignore())
            .ReverseMap();

            CreateMap<InsertRestaurantDTO, Restaurant>()
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.Name,
                configuration => configuration.MapFrom(insertRestaurantDTO => insertRestaurantDTO.Name))
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.Street,
                configuration => configuration.MapFrom(insertRestaurantDTO => insertRestaurantDTO.Street))
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.House,
                configuration => configuration.MapFrom(insertRestaurantDTO => insertRestaurantDTO.House))
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.City,
                configuration => configuration.MapFrom(insertRestaurantDTO => insertRestaurantDTO.City))
                .ForMember(
                restaurant => restaurant.Employees,
                configuration => configuration.Ignore())
                .ForMember(
                restaurant => restaurant.Menu,
                configuration => configuration.Ignore())
                .ForMember(
                restaurant => restaurant.Id,
                configuration => configuration.Ignore())
            .ReverseMap();

            CreateMap<Restaurant, Restaurant>();
        }
    }
}
