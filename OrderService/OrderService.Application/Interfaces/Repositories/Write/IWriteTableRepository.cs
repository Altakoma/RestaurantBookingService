using OrderService.Application.Interfaces.Repositories.Base;
using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces.Repositories.Write
{
    public interface IWriteTableRepository : IWriteRepository<Table>
    {
    }
}
