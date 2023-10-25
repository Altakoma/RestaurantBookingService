using OrderService.Application.Interfaces.Repositories.Read;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data.ApplicationNoSqlDbContext;
using OrderService.Infrastructure.Repositories.Base;

namespace OrderService.Infrastructure.Repositories.Read
{
    public class ReadMenuRepository : BaseReadRepository<Menu>, IReadMenuRepository
    {
        public ReadMenuRepository(IMongoDbSettings settings) : base(settings)
        {
        }
    }
}
