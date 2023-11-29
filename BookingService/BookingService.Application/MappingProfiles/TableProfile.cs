using AutoMapper;
using BookingService.Application.DTOs.Table;
using BookingService.Domain.Entities;

namespace BookingService.Application.MappingProfiles
{
    public class TableProfile : Profile
    {
        public TableProfile()
        {
            CreateMap<InsertTableDTO, Table>()
                .ForMember(table => table.RestaurantId,
                           options => options.MapFrom(insertTableDTO => insertTableDTO.RestaurantId))
                .ForMember(table => table.SeatsCount,
                           options => options.MapFrom(insertTableDTO => insertTableDTO.SeatsCount));

            CreateMap<UpdateTableDTO, Table>()
                .ForMember(table => table.SeatsCount,
                           options => options.MapFrom(updateTableDTO => updateTableDTO.SeatsCount));

            CreateMap<Table, ReadTableDTO>()
                .ForMember(readTableDTO => readTableDTO.Id,
                           options => options.MapFrom(table => table.Id))
                .ForMember(readTableDTO => readTableDTO.RestaurantId,
                           options => options.MapFrom(table => table.RestaurantId))
                .ForMember(readTableDTO => readTableDTO.RestaurantName,
                           options => options.MapFrom(table => table.Restaurant.Name))
                .ForMember(readTableDTO => readTableDTO.SeatsCount,
                           options => options.MapFrom(table => table.SeatsCount));

            CreateMap<Table, ReadTableForRestaurantDTO>()
                .ForMember(readTableDTO => readTableDTO.Id,
                           options => options.MapFrom(table => table.Id))
                .ForMember(readTableDTO => readTableDTO.SeatsCount,
                           options => options.MapFrom(table => table.SeatsCount));

            CreateMap<Table, Table>();
        }
    }
}
