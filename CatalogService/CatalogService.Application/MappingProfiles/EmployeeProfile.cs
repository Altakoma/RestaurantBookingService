using AutoMapper;
using CatalogService.Application.DTOs.Employee;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, ReadEmployeeDTO>()
                .ForMember(re => re.RestaurantName, conf => conf.MapFrom(e => e.Restaurant.Name));
            CreateMap<InsertEmployeeDTO, Employee>();
            CreateMap<UpdateEmployeeDTO, Employee>();
        }
    }
}
