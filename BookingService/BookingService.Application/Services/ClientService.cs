﻿using AutoMapper;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Services.Base;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Services;

namespace BookingService.Application.Services
{
    public class ClientService : BaseService<Client>, IClientService
    {
        public ClientService(IClientRepository clientRepository,
            IMapper mapper) : base(clientRepository, mapper)
        {
        }
    }
}
