using AutoMapper;
using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Data.Repositories.Base
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly CatalogServiceDbContext _catalogServiceDbContext;
        protected readonly IMapper _mapper;

        public BaseRepository(CatalogServiceDbContext catalogServiceDbContext,
            IMapper mapper)
        {
            _catalogServiceDbContext = catalogServiceDbContext;
            _mapper = mapper;
        }

        public async Task<ICollection<U>> GetAllAsync<U>(
            CancellationToken cancellationToken)
        {
            ICollection<U> items = await _mapper.ProjectTo<U>(
                _catalogServiceDbContext.Set<T>()
                .OrderBy(item => item.Id).Take(15))
                .ToListAsync(cancellationToken);

            return items;
        }

        public async Task<U?> GetByIdAsync<U>(int id,
            CancellationToken cancellationToken)
        {
            U? item = await _mapper.ProjectTo<U>(
                _catalogServiceDbContext.Set<T>().Where(item => item.Id == id))
                .SingleOrDefaultAsync(cancellationToken);

            return item;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            T? item = await _catalogServiceDbContext.Set<T>()
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
            _catalogServiceDbContext.Remove(item);
        }

        public async Task InsertAsync(T item, CancellationToken cancellationToken)
        {
            await _catalogServiceDbContext.AddAsync(item, cancellationToken);
        }

        public void Update(T item)
        {
            _catalogServiceDbContext.Update(item);
        }

        public async Task<bool> SaveChangesToDbAsync(
            CancellationToken cancellationToken)
        {
            int saved = await _catalogServiceDbContext
                              .SaveChangesAsync(cancellationToken);

            return saved > 0;
        }
    }
}
