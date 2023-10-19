﻿using AutoMapper;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Services.Base;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Services.Base;

namespace BookingService.Application.Services
{
    internal class ClientService : BaseService<Client>, IBaseService
    {
        public ClientService(IClientRepository clientRepository,
            IMapper mapper) : base(clientRepository, mapper)
        {
        }
    }
}
