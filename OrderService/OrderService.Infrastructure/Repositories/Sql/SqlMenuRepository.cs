using AutoMapper;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;
using OrderService.Infrastructure.Repositories.Base;

namespace OrderService.Infrastructure.Repositories.Sql
{
    public class SqlMenuRepository : BaseSqlRepository<Menu>, ISqlMenuRepository
    {
        public SqlMenuRepository(OrderServiceSqlDbContext orderServiceSqlDbContext,
            IMapper mapper) : base(orderServiceSqlDbContext, mapper)
        {
        }
    }
}
