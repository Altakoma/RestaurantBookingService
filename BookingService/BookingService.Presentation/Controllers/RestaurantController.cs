using BookingService.Application.DTOs.Exception;
using BookingService.Application.DTOs.Restaurant;
using BookingService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadRestaurantDTO>))]
        public async Task<IActionResult> GetAllRestaurantsAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadRestaurantDTO> restaurantDTOs =
                await _restaurantService.GetAllAsync<ReadRestaurantDTO>(cancellationToken);

            return Ok(restaurantDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadRestaurantDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> GetRestaurantAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadRestaurantDTO restaurantDTO = await _restaurantService
                .GetByIdAsync<ReadRestaurantDTO>(id, cancellationToken);

            return Ok(restaurantDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReadRestaurantDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> InsertRestaurantAsync([FromBody] InsertRestaurantDTO restaurantDTO,
            CancellationToken cancellationToken)
        {
            ReadRestaurantDTO readRestaurantDTO = await _restaurantService
                .InsertAsync<InsertRestaurantDTO, ReadRestaurantDTO>(restaurantDTO, cancellationToken);

            return CreatedAtAction(nameof(GetRestaurantAsync),
                                   new { id = readRestaurantDTO }, restaurantDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadRestaurantDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> UpdateRestaurantAsync([FromRoute] int id,
            [FromBody] UpdateRestaurantDTO updateRestaurantDTO,
            CancellationToken cancellationToken)
        {
            ReadRestaurantDTO restaurantDTO = await _restaurantService
                .UpdateAsync<UpdateRestaurantDTO, ReadRestaurantDTO>(id,
                updateRestaurantDTO, cancellationToken);

            return Ok(restaurantDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> DeleteRestaurantAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _restaurantService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
