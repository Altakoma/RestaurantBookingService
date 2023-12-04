using CatalogService.Application.Interfaces.Repositories.Base;
using CatalogService.Application.Redis.Interfaces;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Redis.Base;
using Microsoft.Extensions.Caching.Distributed;

namespace CatalogService.Infrastructure.Redis.CacheAccessors
{
    public class EmployeeCacheAccessor : BaseCacheAccessor<Employee>, IEmployeeCacheAccessor
    {
        private const string EmployeePreposition = "employee";
        private const int EmployeeCacheExpiratonTime = 30;

        public EmployeeCacheAccessor(IRepository<Employee> repository,
            IDistributedCache distributedCache)
            : base(repository, distributedCache, EmployeeCacheExpiratonTime,
                   EmployeePreposition)
        {
        }
    }
}
