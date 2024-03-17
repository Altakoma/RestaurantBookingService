using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.Base;

namespace OrderService.Application.Interfaces.Repositories.NoSql
{
    public interface INoSqlOrderRepository : INoSqlRepository<ReadOrderDTO>
    {
        Task UpdateAsync(ReadOrderDTO item, CancellationToken cancellationToken);
        Task DeleteOrdersByClientIdAsync(int clientId, CancellationToken cancellationToken);
        Task DeleteOrderByMenuIdAsync(int menuId, CancellationToken cancellationToken);
    }
}
