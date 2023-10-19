﻿using AutoMapper;
using BookingService.Application.DTOs.Client;
using BookingService.Domain.Entities;

namespace BookingService.Application.MappingProfiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<InsertClientDTO, Client>()
                .ForMember(client => client.Id,
                options => options.MapFrom(insertClientDTO => insertClientDTO.Id))
                .ForMember(client => client.Name,
                options => options.MapFrom(insertClientDTO => insertClientDTO.Name));

            CreateMap<UpdateClientDTO, Client>()
                .ForMember(client => client.Name,
                options => options.MapFrom(updateClientDTO => updateClientDTO.Name));

            CreateMap<Client, ReadClientDTO>()
                .ForMember(readClientDTO => readClientDTO.Id,
                options => options.MapFrom(client => client.Id))
                .ForMember(readClientDTO => readClientDTO.Name,
                options => options.MapFrom(client => client.Name));

            CreateMap<Client, Client>();
        }
    }
}
