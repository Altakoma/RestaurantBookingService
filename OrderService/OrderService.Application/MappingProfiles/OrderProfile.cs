﻿using AutoMapper;
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
                options => options.MapFrom(insertOrderDTO => insertOrderDTO.MenuId));

            CreateMap<UpdateOrderDTO, UpdateOrderCommand>()
                .ForMember(updateOrderCommand => updateOrderCommand.MenuId,
                options => options.MapFrom(updateOrderDTO => updateOrderDTO.MenuId));

            CreateMap<InsertOrderCommand, Order>()
                .ForMember(order => order.BookingId,
                options => options.MapFrom(insertOrderCommand => insertOrderCommand.BookingId))
                .ForMember(order => order.MenuId,
                options => options.MapFrom(insertOrderCommand => insertOrderCommand.MenuId))
                .ForMember(order => order.ClientId,
                options => options.MapFrom(insertOrderCommand => insertOrderCommand.ClientId));

            CreateMap<UpdateOrderCommand, Order>()
                .ForMember(order => order.Id,
                options => options.MapFrom(updateOrderCommand => updateOrderCommand.Id))
                .ForMember(order => order.MenuId,
                options => options.MapFrom(updateOrderCommand => updateOrderCommand.MenuId))
                .ForMember(order => order.ClientId,
                options => options.MapFrom(updateOrderCommand => updateOrderCommand.ClientId));

            CreateMap<Order, ReadOrderDTO>()
                .ForMember(readOrderDTO => readOrderDTO.BookingId,
                options => options.MapFrom(order => order.BookingId))
                .ForMember(readOrderDTO => readOrderDTO.Id,
                options => options.MapFrom(order => order.Id))
                .ForMember(readOrderDTO => readOrderDTO.ReadTableDTO,
                options => options.MapFrom(order => order.Table))
                .ForMember(readOrderDTO => readOrderDTO.ReadClientDTO,
                options => options.MapFrom(order => order.Client))
                .ForMember(readOrderDTO => readOrderDTO.ReadMenuDTO,
                options => options.MapFrom(order => order.Menu))
                .ReverseMap();
        }
    }
}
