using AutoMapper;
using BookingService.Application.Interfaces.Repositories;
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
    }
}
