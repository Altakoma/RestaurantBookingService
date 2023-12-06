using AutoMapper;
using OrderService.Application.DTOs.Client;
using OrderService.Application.DTOs.Client.Messages;
using OrderService.Domain.Entities;

namespace OrderService.Application.MappingProfiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
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

            CreateMap<InsertClientMessageDTO, Client>()
                .ForMember(client => client.Id,
                options => options.MapFrom(insertClientDTO => insertClientDTO.Id))
                .ForMember(client => client.Name,
                options => options.MapFrom(insertClientDTO => insertClientDTO.Name));

            CreateMap<Client, Client>();
        }
    }
}
