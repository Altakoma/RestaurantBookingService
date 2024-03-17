using AutoMapper;
using CatalogService.Application.Interfaces.Repositories;
using CatalogService.Domain.Entities;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using CatalogService.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Data.Repositories
{
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        public MenuRepository(CatalogServiceDbContext catalogServiceDbContext,
            IMapper mapper) : base(catalogServiceDbContext, mapper)
        {
        }

        public async Task<ICollection<U>> GetAllByRestaurantIdAsync<U>(int id,
            CancellationToken cancellationToken)
        {
            ICollection<U> readMenuDTOs = await _mapper.ProjectTo<U>(
                _catalogServiceDbContext.Menu.Where(menu => menu.RestaurantId == id))
                .ToListAsync(cancellationToken);

            return readMenuDTOs;
        }
    }
}
