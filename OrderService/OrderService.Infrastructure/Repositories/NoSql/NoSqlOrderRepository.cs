﻿using MongoDB.Driver;
using OrderService.Application.DTOs.Order;
using OrderService.Application.Interfaces.Repositories.NoSql;
using OrderService.Infrastructure.Data.ApplicationNoSqlDbSettings;
using OrderService.Infrastructure.Repositories.Base;

namespace OrderService.Infrastructure.Repositories.Read
{
    public class NoSqlOrderRepository : BaseNoSqlRepository<ReadOrderDTO>, INoSqlOrderRepository
    {
        public NoSqlOrderRepository(IMongoDbSettings settings) : base(settings)
        {
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
