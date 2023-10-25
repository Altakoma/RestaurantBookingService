using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data.ApplicationNoSqlDbContext;
using OrderService.Infrastructure.Repositories.Base;

namespace OrderService.Infrastructure.Repositories.Read
{
    public class ReadTableRepository : BaseReadRepository<Table>, IReadTableRepository
    {
        public ReadTableRepository(IMongoDbSettings settings) : base(settings)
        {
        }
    }
}
