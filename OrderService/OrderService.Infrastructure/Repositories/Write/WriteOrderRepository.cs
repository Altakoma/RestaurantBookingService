﻿using AutoMapper;
using OrderService.Application.Interfaces.Repositories.Write;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;
using OrderService.Infrastructure.Repositories.Base;

namespace OrderService.Infrastructure.Repositories.Write
{
    public class WriteOrderRepository : BaseWriteRepository<Order>, IWriteOrderRepository
    {
        public WriteOrderRepository(OrderServiceSqlDbContext orderServiceSqlDbContext,
            IMapper mapper) : base(orderServiceSqlDbContext, mapper)
        {
        }
    }
}
