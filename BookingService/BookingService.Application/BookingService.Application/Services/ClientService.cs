using AutoMapper;
using BookingService.Application.Interfaces.Repositories;
using BookingService.Application.Services.Base;
using BookingService.Domain.Entities;
using BookingService.Domain.Interfaces.Services.Base;

namespace BookingService.Application.Services
{
    internal class ClientService : BaseService<Client>, IService<Client>
    {
        public ClientService(IClientRepository clientRepository,
            IMapper mapper) : base(clientRepository, mapper)
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

        public Task<U> UpdateAsync<U>(int id, Client item, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
