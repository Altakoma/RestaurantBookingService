using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using OrderService.Application.Interfaces.Command;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;
using System.Data.Common;

namespace OrderService.Presentation.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : Transactional
    {
        private readonly OrderServiceSqlDbContext _dbContext;

        public TransactionBehavior(OrderServiceSqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response;

            if (request.IsTransactionSkipped)
            {
                response = await next();
            }
            else
            {
                using var transaction = await BeginTransactionAsync(cancellationToken);

                try
                {
                    response = await next();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();

                    throw;
                }
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
