using OrderService.Application.Interfaces.Repositories.Base;
using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces.Repositories.Read
{
    public interface IReadTableRepository : IReadRepository<Table>
    {
    }
}
