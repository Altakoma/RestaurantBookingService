using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;
using OrderService.Infrastructure.Repositories.Base;

namespace OrderService.Infrastructure.Repositories.Write
{
    public class SqlOrderRepository : BaseSqlRepository<Order>, ISqlOrderRepository
    {
        public SqlOrderRepository(OrderServiceSqlDbContext orderServiceSqlDbContext,
            IMapper mapper) : base(orderServiceSqlDbContext, mapper)
        {
        }
    }
}
