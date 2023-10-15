using IdentityService.DataAccess.DatabaseContext;
using IdentityService.DataAccess.Repositories.Interfaces.Base;

namespace IdentityService.DataAccess.Repositories.Base
{
    public abstract class WriteRepository<T> : IWriteRepository<T>
        where T : notnull
    {
        private readonly IdentityDbContext _identityDbContext;

        public WriteRepository(IdentityDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
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
