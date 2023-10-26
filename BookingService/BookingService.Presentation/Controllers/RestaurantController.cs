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
    }
}
