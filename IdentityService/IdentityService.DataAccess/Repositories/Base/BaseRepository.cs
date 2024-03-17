﻿using AutoMapper;
using IdentityService.DataAccess.DatabaseContext;
using IdentityService.DataAccess.Entities;
using IdentityService.DataAccess.Repositories.Interfaces.Base;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.DataAccess.Repositories.Base
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly IdentityDbContext _identityDbContext;
        protected readonly IMapper _mapper;

        public BaseRepository(IdentityDbContext identityDbContext,
            IMapper mapper)
        {
            _identityDbContext = identityDbContext;
            _mapper = mapper;
        }

        public async Task<ICollection<U>> GetAllAsync<U>(CancellationToken cancellationToken)
        {
            ICollection<U> items = await _mapper.ProjectTo<U>(
                _identityDbContext.Set<T>().Select(item => item))
                .ToListAsync(cancellationToken);

            return items;
        }

        public async Task<U?> GetByIdAsync<U>(int id,
            CancellationToken cancellationToken)
        {
            U? item = await _mapper.ProjectTo<U>(
                _identityDbContext.Set<T>().Where(item => item.Id == id))
                .SingleOrDefaultAsync(cancellationToken);

            return item;
        }

        public void Delete(T item)
        {
            _identityDbContext.Remove(item);
        }

        public async Task<T> InsertAsync(T item,
            CancellationToken cancellationToken)
        {
            await _identityDbContext.AddAsync(item, cancellationToken);

            return item;
        }

        public void Update(T item)
        {
            _identityDbContext.Update(item);
        }

        public async Task<bool> SaveChangesToDbAsync(
            CancellationToken cancellationToken)
        {
            int saved = await _identityDbContext
                              .SaveChangesAsync(cancellationToken);

            return saved > 0;
        }
    }
}
