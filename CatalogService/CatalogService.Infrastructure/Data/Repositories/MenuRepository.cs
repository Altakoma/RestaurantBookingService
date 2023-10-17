using AutoMapper;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
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

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            Menu? menu = await _catalogServiceDbContext.Menu
                .FirstOrDefaultAsync(menu => menu.Id == id,
                cancellationToken);

            if (menu is null)
            {
                throw new NotFoundException(nameof(Employee), id.ToString(),
                    typeof(Employee));
            }

            Delete(menu);
        }

        public async Task<U?> GetByIdAsync<U>(int id,
            CancellationToken cancellationToken)
        {
            U? readMenuDTO = await _mapper.ProjectTo<U>(
                _catalogServiceDbContext.Menu.Where(menu => menu.Id == id))
                .SingleOrDefaultAsync(cancellationToken);

            return readMenuDTO;
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
