using BookingService.Application.DTOs.Client;
using BookingService.Application.DTOs.Exception;
using BookingService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadClientDTO>))]
        public async Task<IActionResult> GetAllClientsAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadClientDTO> clientDTOs =
                await _clientService.GetAllAsync<ReadClientDTO>(cancellationToken);

            return Ok(clientDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadClientDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> GetClientAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadClientDTO clientDTO = await _clientService
                .GetByIdAsync<ReadClientDTO>(id, cancellationToken);

            return Ok(clientDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReadClientDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> InsertClientAsync([FromBody] InsertClientDTO clientDTO,
            CancellationToken cancellationToken)
        {
            ReadClientDTO readClientDTO = await _clientService
                .InsertAsync<InsertClientDTO, ReadClientDTO>(clientDTO, cancellationToken);

            return CreatedAtAction(nameof(GetClientAsync),
                                   new { id = readClientDTO }, clientDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadClientDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> UpdateClientAsync([FromRoute] int id,
            [FromBody] UpdateClientDTO updateClientDTO,
            CancellationToken cancellationToken)
        {
            ReadClientDTO clientDTO = await _clientService
                .UpdateAsync<UpdateClientDTO, ReadClientDTO>(id,
                updateClientDTO, cancellationToken);

            return Ok(clientDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> DeleteClientAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _clientService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
