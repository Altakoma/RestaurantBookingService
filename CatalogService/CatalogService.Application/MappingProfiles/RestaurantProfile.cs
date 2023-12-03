﻿using AutoMapper;
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
                configuration => configuration.MapFrom(restaurant => restaurant.Name));

            CreateMap<ReadRestaurantDTO, UpdateRestaurantMessageDTO>()
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.Id,
                configuration => configuration.MapFrom(restaurant => restaurant.Id))
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.Name,
                configuration => configuration.MapFrom(restaurant => restaurant.Name));

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
            .ReverseMap();

            CreateMap<InsertRestaurantDTO, Restaurant>()
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.Name,
                configuration => configuration.MapFrom(updateRestaurantDTO => updateRestaurantDTO.Name))
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.Street,
                configuration => configuration.MapFrom(updateRestaurantDTO => updateRestaurantDTO.Street))
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.House,
                configuration => configuration.MapFrom(updateRestaurantDTO => updateRestaurantDTO.House))
                .ForMember(
                readRestaurantDTO => readRestaurantDTO.City,
                configuration => configuration.MapFrom(updateRestaurantDTO => updateRestaurantDTO.City))
            .ReverseMap();

            CreateMap<Restaurant, Restaurant>();
        }
    }
}
