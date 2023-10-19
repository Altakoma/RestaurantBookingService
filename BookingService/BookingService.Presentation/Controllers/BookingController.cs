using BookingService.Application.DTOs.Booking;
using BookingService.Application.DTOs.Exception;
using BookingService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BookingController : ControllerBase
    {
        private readonly IBookService _bookingService;

        public BookingController(IBookService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadBookingDTO>))]
        public async Task<IActionResult> GetAllBookingsAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadBookingDTO> bookingDTOs =
                await _bookingService.GetAllAsync<ReadBookingDTO>(cancellationToken);

            return Ok(bookingDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadBookingDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> GetBookingAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadBookingDTO bookingDTO = await _bookingService
                .GetByIdAsync<ReadBookingDTO>(id, cancellationToken);

            return Ok(bookingDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReadBookingDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> InsertBookingAsync([FromBody] InsertBookingDTO bookingDTO,
            CancellationToken cancellationToken)
        {
            ReadBookingDTO readBookingDTO = await _bookingService
                .InsertAsync<InsertBookingDTO, ReadBookingDTO>(bookingDTO, cancellationToken);

            return CreatedAtAction(nameof(GetBookingAsync),
                                   new { id = readBookingDTO }, bookingDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadBookingDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> UpdateBookingAsync([FromRoute] int id,
            [FromBody] UpdateBookingDTO updateBookingDTO,
            CancellationToken cancellationToken)
        {
            ReadBookingDTO bookingDTO = await _bookingService
                .UpdateAsync<UpdateBookingDTO, ReadBookingDTO>(id,
                updateBookingDTO, cancellationToken);

            return Ok(bookingDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> DeleteBookingAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _bookingService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
