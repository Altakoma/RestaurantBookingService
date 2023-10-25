using AutoMapper;
using OrderService.Application.DTOs.Client;
using OrderService.Application.MediatR.Client.Commands;
using OrderService.Domain.Entities;

namespace OrderService.Application.MappingProfiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<InsertClientDTO, InsertClientCommand>()
                .ForMember(insertClientCommand => insertClientCommand.Id,
                options => options.MapFrom(insertClientDTO => insertClientDTO.Id))
                .ForMember(insertClientCommand => insertClientCommand.Name,
                options => options.MapFrom(insertClientDTO => insertClientDTO.Name));

            CreateMap<UpdateClientDTO, UpdateClientCommand>()
                .ForMember(updateClientCommand => updateClientCommand.Name,
                options => options.MapFrom(updateClientDTO => updateClientDTO.Name));

            CreateMap<InsertClientCommand, Client>()
                .ForMember(client => client.Id,
                options => options.MapFrom(insertClientCommand => insertClientCommand.Id))
                .ForMember(client => client.Name,
                options => options.MapFrom(insertClientCommand => insertClientCommand.Name));

            CreateMap<UpdateClientCommand, Client>()
                .ForMember(client => client.Id,
                options => options.MapFrom(updateClientCommand => updateClientCommand.Id))
                .ForMember(client => client.Name,
                options => options.MapFrom(updateClientCommand => updateClientCommand.Name));

            CreateMap<Client, ReadClientDTO>()
                .ForMember(readClientDTO => readClientDTO.Id,
                options => options.MapFrom(client => client.Id))
                .ForMember(readClientDTO => readClientDTO.Name,
                options => options.MapFrom(client => client.Name));
        }
    }
}
