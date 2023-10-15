using CatalogService.Application.DTOs.FoodType;
using CatalogService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FoodTypeController : ControllerBase
    {
        private readonly IFoodTypeService _foodTypeService;

        public FoodTypeController(IFoodTypeService foodTypeService)
        {
            _foodTypeService = foodTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFoodTypesAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadFoodTypeDTO> readFoodTypeDTOs =
                await _foodTypeService.GetAllAsync(cancellationToken);

            return Ok(readFoodTypeDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadFoodTypeDTO readFoodTypeDTO =
                await _foodTypeService.GetByIdAsync(id, cancellationToken);

            return Ok(readFoodTypeDTO);
        }

        [HttpPost]
        public async Task<IActionResult> InsertFoodTypeAsync(
            [FromBody] FoodTypeDTO foodTypeDTO,
            CancellationToken cancellationToken)
        {
            ReadFoodTypeDTO readFoodTypeDTO = await _foodTypeService
                .InsertAsync(foodTypeDTO, cancellationToken);

            return CreatedAtAction(nameof(GetEmployeeAsync),
                                   new { readFoodTypeDTO.Id }, readFoodTypeDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFoodTypeAsync([FromRoute] int id,
            [FromBody] FoodTypeDTO foodTypeDTO,
            CancellationToken cancellationToken)
        {
            ReadFoodTypeDTO readFoodTypeDTO =
                await _foodTypeService.UpdateAsync(id, foodTypeDTO, cancellationToken);

            return Ok(readFoodTypeDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodTypeAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _foodTypeService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
