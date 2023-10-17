using AutoMapper;
using CatalogService.Application.RepositoryInterfaces;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Exceptions;
using CatalogService.Infrastructure.Data.ApplicationDbContext;
using CatalogService.Infrastructure.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Data.Repositories
{
    public class FoodTypeRepository : BaseRepository<FoodType>, IFoodTypeRepository
    {
        public FoodTypeRepository(CatalogServiceDbContext catalogServiceDbContext,
            IMapper mapper) : base(catalogServiceDbContext, mapper)
        {
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            FoodType? foodType = await _catalogServiceDbContext.FoodTypes
                .FirstOrDefaultAsync(foodType => foodType.Id == id,
                                     cancellationToken);

            if (foodType is null)
            {
                throw new NotFoundException(nameof(Employee), id.ToString(),
                    typeof(Employee));
            }

            Delete(foodType);
        }

        public async Task<U?> GetByIdAsync<U>(int id,
            CancellationToken cancellationToken)
        {
            U? readFoodTypeDTO = await _mapper.ProjectTo<U>(
                _catalogServiceDbContext.FoodTypes.Where(foodType => foodType.Id == id))
                .SingleOrDefaultAsync(cancellationToken);

            return readFoodTypeDTO;
        }
    }
}
