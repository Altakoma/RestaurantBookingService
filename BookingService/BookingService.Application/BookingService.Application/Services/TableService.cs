using AutoMapper;
using BookingService.Application.RepositoryInterfaces;
using BookingService.Application.Services.Base;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Services.Base;

namespace BookingService.Application.Services
{
    public class TableService : BaseService<Table>, IService<Table>
    {
        public TableService(ITableRepository tableRepository,
            IMapper mapper) : base(tableRepository, mapper)
        {
        }

        public Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<U> GetByIdAsync<U>(int id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<U> UpdateAsync<U>(int id, Table item, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
