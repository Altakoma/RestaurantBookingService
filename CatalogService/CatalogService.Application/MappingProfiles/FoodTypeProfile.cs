using AutoMapper;
using CatalogService.Application.DTOs.FoodType;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.MappingProfiles
{
    public class FoodTypeProfile : Profile
    {
        public FoodTypeProfile()
        {
            CreateMap<FoodType, ReadFoodTypeDTO>();
            CreateMap<FoodTypeDTO, FoodType>();
        }
    }
}
