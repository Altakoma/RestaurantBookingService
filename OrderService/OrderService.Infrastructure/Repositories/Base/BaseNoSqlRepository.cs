using MongoDB.Driver;
using OrderService.Application.Interfaces.Repositories.Base;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Data.ApplicationNoSqlDbSettings;

namespace OrderService.Infrastructure.Repositories.Base
{
    public class BaseNoSqlRepository<T> : INoSqlRepository<T> where T : BaseEntity
    {
        protected readonly IMongoCollection<T> _collection;

        public BaseNoSqlRepository(IMongoDbSettings settings)
        {
            var database = new MongoClient(settings.ConnectionString)
                               .GetDatabase(settings.DatabaseName);

            _collection = database.GetCollection<T>(GetCollectionName(typeof(T)));
        }

        public BaseNoSqlRepository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        protected string GetCollectionName(Type documentType)
        {
            return documentType.Name.ToLower();
        }

        public async Task<ICollection<T>> GetAllAsync(int skipCount, int selectionAmount, 
            CancellationToken cancellationToken)
        {
            var filterDefinition = Builders<T>.Filter.Empty;

            return await _collection.Find(filterDefinition)
                                    .Skip(skipCount)
                                    .Limit(selectionAmount)
                                    .ToListAsync(cancellationToken);
        }

        public async Task InsertAsync(T item, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(item, cancellationToken: cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var filterDefinition = Builders<T>.Filter
                                              .Eq(element => element.Id, id);

            await _collection.DeleteOneAsync(filterDefinition, cancellationToken);
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var filterDefinition = Builders<T>.Filter
                                              .Eq(element => element.Id, id);

            T? item = await _collection.Find(filterDefinition)
                                       .SingleOrDefaultAsync(cancellationToken);

            return item;
        }
    }
}
