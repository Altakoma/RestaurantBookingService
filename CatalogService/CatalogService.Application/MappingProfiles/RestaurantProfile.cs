using AutoMapper;
using CatalogService.Application.DTOs.Restaurant;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.MappingProfiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, ReadRestaurantDTO>();
            CreateMap<UpdateRestaurantDTO, Restaurant>();
            CreateMap<InsertRestaurantDTO, Restaurant>();
        }
    }
}
