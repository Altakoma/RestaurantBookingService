using AutoMapper;
using BookingService.Application.DTOs.Client.Messages;
using BookingService.Domain.Entities;

namespace BookingService.Application.MappingProfiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<InsertClientMessageDTO, Client>()
                .ForMember(client => client.Id,
                           options => options.MapFrom(insertClientDTO => insertClientDTO.Id))
                .ForMember(client => client.Name,
                           options => options.MapFrom(insertClientDTO => insertClientDTO.Name));

            CreateMap<UpdateClientMessageDTO, Client>()
                .ForMember(client => client.Id,
                           options => options.MapFrom(updateClientDTO => updateClientDTO.Id))
                .ForMember(client => client.Name,
                           options => options.MapFrom(updateClientDTO => updateClientDTO.Name));

            CreateMap<Client, Client>();
        }
    }
}
