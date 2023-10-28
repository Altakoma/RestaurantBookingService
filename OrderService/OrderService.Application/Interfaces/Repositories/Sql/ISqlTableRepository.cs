using OrderService.Application.Interfaces.Repositories.Base;
using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces.Repositories.Sql
{
    public interface ISqlTableRepository : ISqlRepository<Table>
    {
    }
}
