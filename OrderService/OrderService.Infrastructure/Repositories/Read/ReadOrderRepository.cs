using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data.ApplicationNoSqlDbContext;
using OrderService.Infrastructure.Repositories.Base;

namespace OrderService.Infrastructure.Repositories.Read
{
    public class ReadOrderRepository : BaseReadRepository<Order>, IReadOrderRepository
    {
        public ReadOrderRepository(IMongoDbSettings settings) : base(settings)
        {
        }
    }
}
