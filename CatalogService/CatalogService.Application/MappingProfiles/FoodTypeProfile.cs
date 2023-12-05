using AutoMapper;
using CatalogService.Application.DTOs.FoodType;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.MappingProfiles
{
    public class FoodTypeProfile : Profile
    {
        public FoodTypeProfile()
        {
            CreateMap<FoodType, ReadFoodTypeDTO>()
                .ForMember(
                readFoodTypeDTO => readFoodTypeDTO.Id,
                configuration => configuration.MapFrom(foodType => foodType.Id))
                .ForMember(
                readFoodTypeDTO => readFoodTypeDTO.Name,
                configuration => configuration.MapFrom(foodType => foodType.Name));

            CreateMap<FoodTypeDTO, FoodType>()
                .ForMember(foodType => foodType.Name,
                configuration => configuration.MapFrom(foodTypeDTO => foodTypeDTO.Name))
                .ForMember(foodType => foodType.Menu,
                configuration => configuration.Ignore())
                .ForMember(foodType => foodType.Id,
                configuration => configuration.Ignore())
            .ReverseMap();

            CreateMap<FoodType, FoodType>();
        }
    }
}
