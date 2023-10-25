using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data.ApplicationNoSqlDbContext;
using OrderService.Infrastructure.Repositories.Base;

namespace OrderService.Infrastructure.Repositories.Read
{
    public class ReadClientRepository : BaseReadRepository<Client>, IReadClientRepository
    {
        public ReadClientRepository(IMongoDbSettings settings) : base(settings)
        {
        }
    }
}
