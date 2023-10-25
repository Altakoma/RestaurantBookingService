using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTOs.Client;
using OrderService.Application.DTOs.Exception;
using OrderService.Application.MediatR.Client.Commands;
using OrderService.Application.MediatR.Client.Queries;

namespace OrderService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ClientController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadClientDTO>))]
        public async Task<IActionResult> GetAllClientsAsync(CancellationToken cancellationToken)
        {
            ICollection<ReadClientDTO> clientDTOs =
                await _mediator.Send(new GetAllClientsQuery(), cancellationToken);

            return Ok(clientDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadClientDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> GetClientAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadClientDTO clientDTO =
                await _mediator.Send(new GetClientByIdQuery { Id = id }, cancellationToken);

            return Ok(clientDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReadClientDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> InsertClientAsync([FromBody] InsertClientDTO clientDTO,
            CancellationToken cancellationToken)
        {
            var insertClientCommand = _mapper.Map<InsertClientCommand>(clientDTO);

            ReadClientDTO readClientDTO = await _mediator.Send(insertClientCommand, cancellationToken);

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
            var updateClientCommand = _mapper.Map<UpdateClientCommand>(updateClientDTO);
            updateClientCommand.Id = id;

            ReadClientDTO clientDTO = await _mediator.Send(updateClientCommand, cancellationToken);

            return Ok(clientDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> DeleteClientAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteClientCommand { Id = id }, cancellationToken);

            return NoContent();
        }
    }
}
