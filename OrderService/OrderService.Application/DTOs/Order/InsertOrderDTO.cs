﻿namespace OrderService.Application.DTOs.Order
{
    public class InsertOrderDTO
    {
        public int BookingId { get; set; }
        public int ClientId { get; set; }
        public int TableId { get; set; }
        public int MenuId { get; set; }
    }
}
