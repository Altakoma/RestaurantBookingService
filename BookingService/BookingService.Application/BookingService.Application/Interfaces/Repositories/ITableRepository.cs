using BookingService.Application.Interfaces.Repositories.Base;
using BookingService.Domain.Entities;

namespace BookingService.Application.Interfaces.Repositories
{
    public interface ITableRepository : IRepository<Table>
    {
        Task<U?> GetByIdAsync<U>(int id, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
