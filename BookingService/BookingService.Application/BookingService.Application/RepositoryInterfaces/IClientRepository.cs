using BookingService.Application.RepositoryInterfaces.Base;
using BookingService.Domain.Entities;

namespace BookingService.Application.RepositoryInterfaces
{
    public interface IClientRepository : IRepository<Client>
    {
        Task<U?> GetByIdAsync<U>(int id, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
