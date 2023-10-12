using AutoMapper;
using CatalogService.Application.DTOs.Employee;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.MappingProfiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
        }
    }
}
