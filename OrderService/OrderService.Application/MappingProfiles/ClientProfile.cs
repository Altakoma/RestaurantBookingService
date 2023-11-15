using AutoMapper;
using OrderService.Application.DTOs.Base.Messages;
using OrderService.Application.DTOs.Client;
using OrderService.Application.DTOs.Client.Messages;
using OrderService.Application.MediatR.Client.Commands;
using OrderService.Domain.Entities;

namespace OrderService.Application.MappingProfiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
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
                options => options.MapFrom(client => client.Name))
                .ReverseMap();

            CreateMap<UpdateClientMessageDTO, Client>()
                .ForMember(client => client.Id,
                options => options.MapFrom(updateClientDTO => updateClientDTO.Id))
                .ForMember(client => client.Name,
                options => options.MapFrom(updateClientDTO => updateClientDTO.Name));

            CreateMap<UpdateClientMessageDTO, UpdateClientCommand>()
                .ForMember(updateClientCommand => updateClientCommand.Id,
                options => options.MapFrom(updateClientMessageDTO => updateClientMessageDTO.Id))
                .ForMember(updateClientCommand => updateClientCommand.Name,
                options => options.MapFrom(updateClientMessageDTO => updateClientMessageDTO.Name));

            CreateMap<InsertClientMessageDTO, Client>()
                .ForMember(client => client.Id,
                options => options.MapFrom(insertClientDTO => insertClientDTO.Id))
                .ForMember(client => client.Name,
                options => options.MapFrom(insertClientDTO => insertClientDTO.Name));

            CreateMap<Client, DeleteClientCommand>()
                .ForMember(deleteClientCommand => deleteClientCommand.Id,
                options => options.MapFrom(client => client.Id));

            CreateMap<Client, Client>();
        }
    }
}
