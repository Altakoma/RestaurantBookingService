using BookingService.Application.DTOs.Table;
using BookingService.Domain.Interfaces.Services.Base;

namespace BookingService.Domain.Interfaces.Services
{
    public interface ITableService : IBaseService
    {
        Task<T> InsertAsync<T>(InsertTableDTO insertItemDTO, CancellationToken cancellationToken);
    }
}
