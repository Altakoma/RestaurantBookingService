using AutoMapper;
using CatalogService.Application.DTOs.Menu;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using CatalogService.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace CatalogService.Infrastructure.Data.Repositories
{
    public class MenuRepository : WriteRepository<Menu>, IMenuRepository
    {
        public MenuRepository(CatalogServiceDbContext dbContext,
            IMapper mapper) : base(dbContext, mapper)
        {
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            Menu? menu = await _dbContext.Menu
                .FirstOrDefaultAsync(menu => menu.Id == id,
                cancellationToken);

            if (menu is null)
            {
                throw new NotFoundException(nameof(Employee), id.ToString(),
                    typeof(Employee));
            }

            Delete(menu);
        }

        public async Task<ICollection<ReadMenuDTO>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            var readMenuDTOs = await _mapper.ProjectTo<ReadMenuDTO>(
                _dbContext.Menu.Select(menu => menu))
                .ToListAsync(cancellationToken);

            return readMenuDTOs;
        }

        public async Task<ReadMenuDTO?> GetByIdAsync(int id,
            CancellationToken cancellationToken)
        {
            var readMenuDTO = await _mapper.ProjectTo<ReadMenuDTO>(
                _dbContext.Menu.Where(menu => menu.Id == id))
                .SingleOrDefaultAsync(cancellationToken);

            return readMenuDTO;
        }

        public async Task<ICollection<ReadMenuDTO>> GetAllByRestaurantIdAsync(int id,
            CancellationToken cancellationToken)
        {
            var readMenuDTOs = await _mapper.ProjectTo<ReadMenuDTO>(
                _dbContext.Menu.Where(menu => menu.RestaurantId == id))
                .ToListAsync(cancellationToken);

            return readMenuDTOs;
        }
    }
}
