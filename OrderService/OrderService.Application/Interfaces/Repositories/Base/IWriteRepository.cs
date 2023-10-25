﻿namespace OrderService.Application.Interfaces.Repositories.Base
{
    public interface IWriteRepository<T>
    {
        Task InsertAsync(T item, CancellationToken cancellationToken);
        Task<bool> SaveChangesToDbAsync(CancellationToken cancellationToken);
        void Update(T item);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        void Delete(T item);
    }
}
