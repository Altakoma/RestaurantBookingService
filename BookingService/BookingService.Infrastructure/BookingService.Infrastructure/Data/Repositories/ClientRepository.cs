using AutoMapper;
using BookingService.Application.RepositoryInterfaces;
using BookingService.Domain.Entities;
using BookingService.Domain.Exceptions;
using BookingService.Infrastructure.Data.ApplicationDbContext;
using BookingService.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Data.Repositories
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(BookingServiceDbContext bookingServiceDbContext,
            IMapper mapper) : base(bookingServiceDbContext, mapper)
        {
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            Client? client = await _bookingServiceDbContext.Clients
                .FirstOrDefaultAsync(client => client.Id == id,
                                     cancellationToken);

            if (client is null)
            {
                throw new NotFoundException(nameof(Client), id.ToString(),
                    typeof(Client));
            }

            Delete(client);
        }

        public async Task<U?> GetByIdAsync<U>(int id, CancellationToken cancellationToken)
        {
            U? readClientDTO = await _mapper.ProjectTo<U>(
                _bookingServiceDbContext.Clients.Where(client => client.Id == id))
                .SingleOrDefaultAsync(cancellationToken);

            return readClientDTO;
        }
    }
}
