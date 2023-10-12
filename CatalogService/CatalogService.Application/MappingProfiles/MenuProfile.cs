using AutoMapper;
using CatalogService.Application.DTOs.Menu;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.MappingProfiles
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, ReadMenuDTO>();
            CreateMap<UpdateMenuDTO, Menu>();
            CreateMap<InsertMenuDTO, Menu>();
        }
    }
}
