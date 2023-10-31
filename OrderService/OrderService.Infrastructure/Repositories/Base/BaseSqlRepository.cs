using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces.Repositories.Base;
using OrderService.Domain.Entities;
using OrderService.Domain.Exceptions;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;

namespace OrderService.Infrastructure.Repositories.Base
{
    public abstract class BaseSqlRepository<T> : ISqlRepository<T> where T : BaseEntity
    {
        protected readonly OrderServiceSqlDbContext _orderServiceSqlDbContext;
        protected readonly IMapper _mapper;

        public BaseSqlRepository(OrderServiceSqlDbContext orderServiceSqlDbContext,
            IMapper mapper)
        {
            _orderServiceSqlDbContext = orderServiceSqlDbContext;
            _mapper = mapper;
        }

        public async Task<U> GetByIdAsync<U>(int id,
            CancellationToken cancellationToken)
        {
            U? item = await _mapper.ProjectTo<U>(
                _orderServiceSqlDbContext.Set<T>().Where(item => item.Id == id))
                .SingleOrDefaultAsync(cancellationToken);

            if (item is null)
            {
                throw new NotFoundException(id.ToString(), typeof(T));
            }

            return item;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            T? item = await _orderServiceSqlDbContext.Set<T>()
                .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);

            if (item is null)
            {
                throw new NotFoundException(id.ToString(),
                    typeof(T));
            }

            Delete(item);
        }

        public void Delete(T item)
        {
            _orderServiceSqlDbContext.Remove(item);
        }

        public async Task<bool> SaveChangesToDbAsync(
            CancellationToken cancellationToken)
        {
            int saved = await _orderServiceSqlDbContext
                              .SaveChangesAsync(cancellationToken);

            return saved > 0;
        }

        public async Task<U> InsertAsync<U>(T item, CancellationToken cancellationToken)
        {
            await _orderServiceSqlDbContext.AddAsync(item, cancellationToken);

            return await GetByIdAsync<U>(item.Id, cancellationToken);
        }

        public void Update(T item)
        {
            _orderServiceSqlDbContext.Update(item);
        }
    }
}
