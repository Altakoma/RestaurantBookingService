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
                .ForMember(insertOrderCommand => insertOrderCommand.MenuId,
                options => options.MapFrom(insertOrderDTO => insertOrderDTO.MenuId))
                .ForMember(insertOrderCommand => insertOrderCommand.ClientId,
                options => options.MapFrom(insertOrderDTO => insertOrderDTO.ClientId))
                .ForMember(insertOrderCommand => insertOrderCommand.TableId,
                options => options.MapFrom(insertOrderDTO => insertOrderDTO.TableId));

            CreateMap<UpdateOrderDTO, UpdateOrderCommand>()
                .ForMember(updateOrderCommand => updateOrderCommand.MenuId,
                options => options.MapFrom(updateOrderDTO => updateOrderDTO.MenuId))
                .ForMember(updateOrderCommand => updateOrderCommand.ClientId,
                options => options.MapFrom(updateOrderDTO => updateOrderDTO.ClientId))
                .ForMember(updateOrderCommand => updateOrderCommand.TableId,
                options => options.MapFrom(updateOrderDTO => updateOrderDTO.TableId));

            CreateMap<InsertOrderCommand, Order>()
                .ForMember(order => order.MenuId,
                options => options.MapFrom(insertOrderCommand => insertOrderCommand.MenuId))
                .ForMember(order => order.ClientId,
                options => options.MapFrom(insertOrderCommand => insertOrderCommand.ClientId))
                .ForMember(order => order.TableId,
                options => options.MapFrom(insertOrderCommand => insertOrderCommand.TableId));

            CreateMap<UpdateOrderCommand, Order>()
                .ForMember(order => order.Id,
                options => options.MapFrom(updateOrderCommand => updateOrderCommand.Id))
                .ForMember(order => order.MenuId,
                options => options.MapFrom(updateOrderCommand => updateOrderCommand.MenuId))
                .ForMember(order => order.ClientId,
                options => options.MapFrom(updateOrderCommand => updateOrderCommand.ClientId))
                .ForMember(order => order.TableId,
                options => options.MapFrom(updateOrderCommand => updateOrderCommand.TableId));

            CreateMap<Order, ReadOrderDTO>()
                .ForMember(readOrderDTO => readOrderDTO.Id,
                options => options.MapFrom(order => order.Id))
                .ForMember(readOrderDTO => readOrderDTO.ReadMenuDTO,
                options => options.MapFrom(order => order.Menu))
                .ForMember(readOrderDTO => readOrderDTO.ReadClientDTO,
                options => options.MapFrom(order => order.Client))
                .ForMember(readOrderDTO => readOrderDTO.ReadMenuDTO,
                options => options.MapFrom(order => order.Menu));
        }
    }
}
