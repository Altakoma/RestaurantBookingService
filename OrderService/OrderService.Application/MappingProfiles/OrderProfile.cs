using AutoMapper;
using OrderService.Application.DTOs.Order;
using OrderService.Application.MediatR.Order.Commands;
using OrderService.Domain.Entities;

namespace OrderService.Application.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<InsertOrderDTO, InsertOrderCommand>()
                .ForMember(insertOrderCommand => insertOrderCommand.BookingId,
                options => options.MapFrom(insertOrderDTO => insertOrderDTO.BookingId))
                .ForMember(insertOrderCommand => insertOrderCommand.MenuId,
                options => options.MapFrom(insertOrderDTO => insertOrderDTO.MenuId))
                .ForMember(insertOrderCommand => insertOrderCommand.ClientId,
                options => options.Ignore());

            CreateMap<Order, InsertOrderCommand>()
                .ForMember(insertOrderCommand => insertOrderCommand.BookingId,
                options => options.MapFrom(order => order.BookingId))
                .ForMember(insertOrderCommand => insertOrderCommand.MenuId,
                options => options.MapFrom(order => order.MenuId))
                .ForMember(insertOrderCommand => insertOrderCommand.ClientId,
                options => options.MapFrom(order => order.ClientId))
                .ReverseMap();

            CreateMap<UpdateOrderDTO, UpdateOrderCommand>()
                .ForMember(updateOrderCommand => updateOrderCommand.MenuId,
                options => options.MapFrom(updateOrderDTO => updateOrderDTO.MenuId))
                .ForMember(updateOrderCommand => updateOrderCommand.Id,
                options => options.Ignore())
                .ForMember(updateOrderCommand => updateOrderCommand.ClientId,
                options => options.Ignore());

            CreateMap<UpdateOrderCommand, Order>()
                .ForMember(order => order.Id,
                options => options.MapFrom(updateOrderCommand => updateOrderCommand.Id))
                .ForMember(order => order.MenuId,
                options => options.MapFrom(updateOrderCommand => updateOrderCommand.MenuId))
                .ForMember(order => order.ClientId,
                options => options.MapFrom(updateOrderCommand => updateOrderCommand.ClientId))
                .ForMember(order => order.BookingId,
                options => options.Ignore())
                .ForMember(order => order.Menu,
                options => options.Ignore())
                .ForMember(order => order.Client,
                options => options.Ignore())
                .ReverseMap();

            CreateMap<Order, ReadOrderDTO>()
                .ForMember(readOrderDTO => readOrderDTO.BookingId,
                options => options.MapFrom(order => order.BookingId))
                .ForMember(readOrderDTO => readOrderDTO.Id,
                options => options.MapFrom(order => order.Id))
                .ForMember(readOrderDTO => readOrderDTO.ReadClientDTO,
                options => options.MapFrom(order => order.Client))
                .ForMember(readOrderDTO => readOrderDTO.ReadMenuDTO,
                options => options.MapFrom(order => order.Menu));

            CreateMap<ReadOrderDTO, Order>()
                .ForMember(order => order.BookingId,
                options => options.MapFrom(readOrderDTO => readOrderDTO.BookingId))
                .ForMember(order => order.Id,
                options => options.MapFrom(readOrderDTO => readOrderDTO.Id))
                .ForMember(order => order.MenuId,
                options => options.MapFrom(readOrderDTO => readOrderDTO.ReadMenuDTO.Id))
                .ForMember(order => order.ClientId,
                options => options.MapFrom(readOrderDTO => readOrderDTO.ReadClientDTO.Id))
                .ForMember(order => order.Menu,
                options => options.Ignore())
                .ForMember(order => order.Client,
                options => options.Ignore());

            CreateMap<Order, DeleteOrderCommand>()
                .ForMember(deleteOrderCommand => deleteOrderCommand.Id,
                options => options.MapFrom(order => order.Id));
        }
    }
}
