﻿using AutoMapper;
using BookingService.Application.Interfaces.Repositories.Base;
using BookingService.Domain.Entities;
using BookingService.Domain.Exceptions;
using BookingService.Infrastructure.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Infrastructure.Data.Repositories.Base
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly BookingServiceDbContext _bookingServiceDbContext;
        protected readonly IMapper _mapper;

        public BaseRepository(BookingServiceDbContext bookingServiceDbContext,
            IMapper mapper)
        {
            _bookingServiceDbContext = bookingServiceDbContext;
            _mapper = mapper;
        }

        public async Task<ICollection<U>> GetAllAsync<U>(
            CancellationToken cancellationToken)
        {
            ICollection<U> items = await _mapper.ProjectTo<U>(
                _bookingServiceDbContext.Set<T>().Select(item => item))
                .ToListAsync(cancellationToken);

            return items;
        }

        public async Task<U?> GetByIdAsync<U>(int id,
            CancellationToken cancellationToken)
        {
            U? item = await _mapper.ProjectTo<U>(
                _bookingServiceDbContext.Set<T>().Where(item => item.Id == id))
                .SingleOrDefaultAsync(cancellationToken);

            return item;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            T? item = await _bookingServiceDbContext.Set<T>()
                .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);

            if (item is null)
            {
                throw new NotFoundException(nameof(T), id.ToString(),
                    typeof(T));
            }

            Delete(item);
        }

        public void Delete(T item)
        {
            _bookingServiceDbContext.Remove(item);
        }

        public async Task InsertAsync(T item, CancellationToken cancellationToken)
        {
            await _bookingServiceDbContext.AddAsync(item, cancellationToken);
        }

        public void Update(T item)
        {
            _bookingServiceDbContext.Update(item);
        }

        public async Task<bool> SaveChangesToDbAsync(
            CancellationToken cancellationToken)
        {
            int saved = await _bookingServiceDbContext
                              .SaveChangesAsync(cancellationToken);

            return saved > 0;
        }
    }
}
