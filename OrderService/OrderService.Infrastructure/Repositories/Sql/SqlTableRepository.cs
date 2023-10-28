using AutoMapper;
using OrderService.Application.Interfaces.Repositories.Sql;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;
using OrderService.Infrastructure.Repositories.Base;

namespace OrderService.Infrastructure.Repositories.Write
{
    public class SqlTableRepository : BaseSqlRepository<Table>, ISqlTableRepository
    {
        public SqlTableRepository(OrderServiceSqlDbContext orderServiceSqlDbContext,
            IMapper mapper) : base(orderServiceSqlDbContext, mapper)
        {
        }
    }
}
