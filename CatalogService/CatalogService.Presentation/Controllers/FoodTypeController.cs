using CatalogService.Application.DTOs.Exception;
using CatalogService.Application.DTOs.FoodType;
using CatalogService.Domain.Interfaces.Services;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadFoodTypeDTO>))]
        public async Task<IActionResult> GetAllFoodTypesAsync(CancellationToken cancellationToken)
        {
            ICollection<ReadFoodTypeDTO> readFoodTypeDTOs =
                await _foodTypeService.GetAllAsync<ReadFoodTypeDTO>(cancellationToken);

            return Ok(readFoodTypeDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadFoodTypeDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> GetFoodTypeAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadFoodTypeDTO readFoodTypeDTO =
                await _foodTypeService.GetByIdAsync<ReadFoodTypeDTO>(id, cancellationToken);

            return Ok(readFoodTypeDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReadFoodTypeDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> InsertFoodTypeAsync([FromBody] FoodTypeDTO foodTypeDTO,
            CancellationToken cancellationToken)
        {
            ReadFoodTypeDTO readFoodTypeDTO = await _foodTypeService
                .InsertAsync<FoodTypeDTO, ReadFoodTypeDTO>(foodTypeDTO, cancellationToken);

            return CreatedAtAction(nameof(GetFoodTypeAsync),
                                   new { readFoodTypeDTO.Id }, readFoodTypeDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadFoodTypeDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> UpdateFoodTypeAsync([FromRoute] int id,
            [FromBody] FoodTypeDTO foodTypeDTO,
            CancellationToken cancellationToken)
        {
            ReadFoodTypeDTO readFoodTypeDTO = await _foodTypeService
                .UpdateAsync<FoodTypeDTO, ReadFoodTypeDTO>(id, foodTypeDTO, cancellationToken);

            return Ok(readFoodTypeDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> DeleteFoodTypeAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _foodTypeService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
