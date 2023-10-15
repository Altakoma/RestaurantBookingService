using AutoMapper;
using CatalogService.Application.RepositoryInterfaces.Base;
using CatalogService.Infrastructure.Data.ApplicationDbContext;

namespace CatalogService.Infrastructure.Data.Repositories.Base
{
    public abstract class WriteRepository<T> : IWriteRepository<T>
        where T : notnull
    {
        protected readonly CatalogServiceDbContext _dbContext;
        protected readonly IMapper _mapper;

        public WriteRepository(CatalogServiceDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Delete(T item)
        {
            _dbContext.Remove(item);
        }

        public async Task<T> InsertAsync(T item, CancellationToken cancellationToken)
        {
            await _dbContext.AddAsync(item);

            return item;
        }

        public void Update(T item)
        {
            _dbContext.Update(item);
        }

        public async Task<bool> SaveChangesToDbAsync(
            CancellationToken cancellationToken)
        {
            int saved = await _dbContext
                              .SaveChangesAsync(cancellationToken);

            return saved > 0;
        }
    }
}
