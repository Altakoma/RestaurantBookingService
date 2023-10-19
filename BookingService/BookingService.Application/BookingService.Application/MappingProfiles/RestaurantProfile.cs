using AutoMapper;
using BookingService.Application.DTOs.Restaurant;
using BookingService.Domain.Entities;

namespace BookingService.Application.MappingProfiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<InsertRestaurantDTO, Restaurant>()
                .ForMember(restaurant => restaurant.Id,
                options => options.MapFrom(insertRestaurantDTO => insertRestaurantDTO.Id))
                .ForMember(restaurant => restaurant.Name,
                options => options.MapFrom(insertRestaurantDTO => insertRestaurantDTO.Name));

            CreateMap<UpdateRestaurantDTO, Restaurant>()
                .ForMember(restaurant => restaurant.Name,
                options => options.MapFrom(updateRestaurantDTO => updateRestaurantDTO.Name));

            CreateMap<Restaurant, ReadRestaurantDTO>()
                .ForMember(readRestaurantDTO => readRestaurantDTO.Id,
                options => options.MapFrom(restaurant => restaurant.Id))
                .ForMember(readRestaurantDTO => readRestaurantDTO.Name,
                options => options.MapFrom(restaurant => restaurant.Name))
                .ForMember(readRestaurantDTO => readRestaurantDTO.readTableDTOs,
                options => options.MapFrom(restaurant => restaurant.Tables));

            CreateMap<Restaurant, Restaurant>();
        }
    }
}
