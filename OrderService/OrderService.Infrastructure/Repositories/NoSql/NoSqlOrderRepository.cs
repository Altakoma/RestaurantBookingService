using MongoDB.Driver;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.NoSql;
using OrderService.Infrastructure.Data.ApplicationNoSqlDbSettings;
using OrderService.Infrastructure.Repositories.Base;

namespace OrderService.Infrastructure.Repositories.NoSql
{
    public class NoSqlOrderRepository : BaseNoSqlRepository<ReadOrderDTO>, INoSqlOrderRepository
    {
        public NoSqlOrderRepository(IMongoDbSettings settings) : base(settings)
        {
        }

        public NoSqlOrderRepository(IMongoCollection<ReadOrderDTO> collection) : base(collection)
        {
        }

        public async Task DeleteOrderByMenuIdAsync(int menuId, CancellationToken cancellationToken)
        {
            var filterDefinition = Builders<ReadOrderDTO>.Filter
                .Eq(element => element.ReadMenuDTO.Id, menuId);

            await _collection.DeleteManyAsync(filterDefinition,
                cancellationToken: cancellationToken);
        }

        public async Task DeleteOrdersByClientIdAsync(int clientId, CancellationToken cancellationToken)
        {
            var filterDefinition = Builders<ReadOrderDTO>.Filter
                .Eq(element => element.ReadClientDTO.Id, clientId);

            await _collection.DeleteManyAsync(filterDefinition,
                cancellationToken: cancellationToken);
        }

        public async Task UpdateAsync(ReadOrderDTO item, CancellationToken cancellationToken)
        {
            var filterDefinition = Builders<ReadOrderDTO>.Filter
                                              .Eq(element => element.Id, item.Id);

            await _collection.ReplaceOneAsync(filterDefinition, item,
                cancellationToken: cancellationToken);
        }
    }
}
