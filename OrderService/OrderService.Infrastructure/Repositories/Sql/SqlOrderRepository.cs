using AutoMapper;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;
using OrderService.Infrastructure.Repositories.Base;

namespace OrderService.Infrastructure.Repositories.Sql
{
    public class SqlOrderRepository : BaseSqlRepository<Order>, ISqlOrderRepository
    {
        public SqlOrderRepository(OrderServiceSqlDbContext orderServiceSqlDbContext,
            IMapper mapper) : base(orderServiceSqlDbContext, mapper)
        {
        }
    }
}
