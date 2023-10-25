using OrderService.Application.Interfaces.Repositories.Base;
using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces.Repositories.Read
{
    public interface IReadMenuRepository : IReadRepository<Menu>
    {
    }
}
