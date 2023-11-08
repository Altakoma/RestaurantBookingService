using AutoMapper;
using BookingService.Application.DTOs.Restaurant;
using BookingService.Application.DTOs.Restaurant.Messages;
using BookingService.Domain.Entities;

namespace BookingService.Application.MappingProfiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, ReadRestaurantDTO>()
                .ForMember(readRestaurantDTO => readRestaurantDTO.Id,
                options => options.MapFrom(restaurant => restaurant.Id))
                .ForMember(readRestaurantDTO => readRestaurantDTO.Name,
                options => options.MapFrom(restaurant => restaurant.Name))
                .ForMember(readRestaurantDTO => readRestaurantDTO.readTableDTOs,
                options => options.MapFrom(restaurant => restaurant.Tables));

            CreateMap<InsertRestaurantMessageDTO, Restaurant>()
                .ForMember(restaurant => restaurant.Id,
                           options => options.MapFrom(insertRestaurantDTO => insertRestaurantDTO.Id))
                .ForMember(restaurant => restaurant.Name,
                           options => options.MapFrom(insertRestaurantDTO => insertRestaurantDTO.Name));

            CreateMap<UpdateRestaurantMessageDTO, Restaurant>()
                .ForMember(restaurant => restaurant.Id,
                           options => options.MapFrom(updateRestaurantDTO => updateRestaurantDTO.Id))
                .ForMember(restaurant => restaurant.Name,
                           options => options.MapFrom(updateRestaurantDTO => updateRestaurantDTO.Name));

            CreateMap<Restaurant, Restaurant>();
        }
    }
}
