using AutoMapper;
using CatalogService.Application.RepositoryInterfaces.Base;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Data.Repositories.Base
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly CatalogServiceDbContext _catalogServiceDbContext;
        protected readonly IMapper _mapper;

        public BaseRepository(CatalogServiceDbContext catalogServiceDbContext,
            IMapper mapper)
        {
            _catalogServiceDbContext = catalogServiceDbContext;
            _mapper = mapper;
        }

        public async Task<ICollection<U>> GetAllAsync<U>(CancellationToken cancellationToken)
        {
            ICollection<U> items = await _mapper.ProjectTo<U>(
                _catalogServiceDbContext.Set<T>().Select(item => item))
                .ToListAsync(cancellationToken);

            return items;
        }

        public void Delete(T item)
        {
            _catalogServiceDbContext.Remove(item);
        }

        public async Task<T> InsertAsync(T item,
            CancellationToken cancellationToken)
        {
            await _catalogServiceDbContext.AddAsync(item, cancellationToken);

            return item;
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
