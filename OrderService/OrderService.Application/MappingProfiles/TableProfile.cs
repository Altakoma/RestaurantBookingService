using AutoMapper;
using OrderService.Application.DTOs.Table;
using OrderService.Application.MediatR.Table.Commands;
using OrderService.Domain.Entities;

namespace OrderService.Application.MappingProfiles
{
    public class TableProfile : Profile
    {
        public TableProfile()
        {
            CreateMap<InsertTableCommand, Table>()
                .ForMember(table => table.Id,
                           options => options.MapFrom(insertTableCommand => insertTableCommand.Id))
                .ForMember(table => table.RestaurantId,
                           options => options.MapFrom(insertTableCommand => insertTableCommand.RestaurantId));

            CreateMap<InsertTableCommand, Table>()
                .ForMember(table => table.Id,
                           options => options.MapFrom(insertTableCommand => insertTableCommand.Id))
                .ForMember(table => table.RestaurantId,
                           options => options.MapFrom(insertTableCommand => insertTableCommand.RestaurantId));

            CreateMap<Table, ReadTableDTO>()
                .ForMember(readTableDTO => readTableDTO.Id,
                           options => options.MapFrom(table => table.Id))
                .ForMember(readTableDTO => readTableDTO.RestaurantId,
                           options => options.MapFrom(table => table.RestaurantId));
        }
    }
}
