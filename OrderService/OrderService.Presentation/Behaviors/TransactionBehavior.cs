﻿using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using OrderService.Application.Interfaces.Command;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;
using System.Data.Common;

namespace OrderService.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : ITransactional
    {
        private readonly OrderServiceSqlDbContext _dbContext;

        public TransactionBehavior(OrderServiceSqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            using var transaction = await BeginTransactionAsync(cancellationToken);

            TResponse response;

            try
            {
                response = await next();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();

                throw new Exception();
            }

            return response;
        }

        private async Task<DbTransaction> BeginTransactionAsync(
            CancellationToken cancellationToken)
        {
            var transaction = await _dbContext.Database
                                              .BeginTransactionAsync(cancellationToken);

            return transaction.GetDbTransaction();
        }
    }
}
