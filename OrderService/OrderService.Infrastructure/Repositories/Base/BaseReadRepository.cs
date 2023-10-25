using MongoDB.Driver;
using OrderService.Application.Interfaces.Repositories.Base;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data.ApplicationNoSqlDbContext;

namespace OrderService.Infrastructure.Repositories.Base
{
    public class BaseReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _collection;

        public BaseReadRepository(IMongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString).GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        protected string GetCollectionName(Type documentType)
        {
            return documentType.Name.ToLower();
        }

        public Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var filter = Builders<T>.Filter.Eq(doc => doc.Id, id);

            return _collection.Find(filter).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            var filter = Builders<T>.Filter.Empty;

            return await _collection.Find(filter).ToListAsync(cancellationToken);
        }
    }
}
