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
                .ForMember(
                readEmployeeDTO => readEmployeeDTO.RestaurantName,
                configuration => configuration.MapFrom(employee => employee.Restaurant.Name))
                .ForMember(
                readEmployeeDTO => readEmployeeDTO.Name,
                configuration => configuration.MapFrom(employee => employee.Name))
                .ForMember(
                readEmployeeDTO => readEmployeeDTO.Id,
                configuration => configuration.MapFrom(employee => employee.Id));

            CreateMap<InsertEmployeeDTO, Employee>()
                .ForMember(
                employee => employee.Name,
                configuration => configuration.MapFrom(insertEmployeeDTO => insertEmployeeDTO.Name))
                .ForMember(
                employee => employee.Id,
                configuration => configuration.MapFrom(insertEmployeeDTO => insertEmployeeDTO.Id))
                .ForMember(
                employee => employee.RestaurantId,
                configuration => configuration.MapFrom(insertEmployeeDTO => insertEmployeeDTO.RestaurantId));

            CreateMap<UpdateEmployeeDTO, Employee>()
                .ForMember(
                employee => employee.Name,
                configuration => configuration.MapFrom(updateEmployeeDTO => updateEmployeeDTO.Name))
                .ForMember(
                employee => employee.RestaurantId,
                configuration => configuration.MapFrom(updateEmployeeDTO => updateEmployeeDTO.RestaurantId));

            CreateMap<Employee, Employee>();
        }
    }
}
