﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces.Repositories.Base;
using OrderService.Domain.Entities;
using OrderService.Domain.Exceptions;
using OrderService.Infrastructure.Data.ApplicationSQLDbContext;

namespace OrderService.Infrastructure.Repositories.Base
{
    public abstract class BaseWriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        protected readonly OrderServiceSqlDbContext _orderServiceSqlDbContext;
        protected readonly IMapper _mapper;

        public BaseWriteRepository(OrderServiceSqlDbContext orderServiceSqlDbContext,
            IMapper mapper)
        {
            _orderServiceSqlDbContext = orderServiceSqlDbContext;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            T? item = await _orderServiceSqlDbContext.Set<T>()
                .FirstOrDefaultAsync(item => item.Id == id, cancellationToken);

            if (item is null)
            {
                throw new NotFoundException(nameof(T), id.ToString(),
                    typeof(T));
            }

            Delete(item);
        }

        public void Delete(T item)
        {
            _orderServiceSqlDbContext.Remove(item);
        }

        public async Task InsertAsync(T item, CancellationToken cancellationToken)
        {
            await _orderServiceSqlDbContext.AddAsync(item, cancellationToken);
        }

        public void Update(T item)
        {
            _orderServiceSqlDbContext.Update(item);
        }

        public async Task<bool> SaveChangesToDbAsync(
            CancellationToken cancellationToken)
        {
            int saved = await _orderServiceSqlDbContext
                              .SaveChangesAsync(cancellationToken);

            return saved > 0;
        }
    }
}
