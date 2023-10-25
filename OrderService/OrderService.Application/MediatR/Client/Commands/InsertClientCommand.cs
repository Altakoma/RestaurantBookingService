﻿using MediatR;
using OrderService.Application.DTOs.Client;

namespace OrderService.Application.MediatR.Client.Commands
{
    public class InsertClientCommand : IRequest<ReadClientDTO>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
