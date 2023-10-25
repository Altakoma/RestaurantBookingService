using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTOs.Exception;
using OrderService.Application.DTOs.Table;
using OrderService.Application.MediatR.Table.Commands;
using OrderService.Application.MediatR.Table.Queries;

namespace OrderService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TableController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadTableDTO>))]
        public async Task<IActionResult> GetAllTableAsync(CancellationToken cancellationToken)
        {
            ICollection<ReadTableDTO> tableDTOs =
                await _mediator.Send(new GetAllTablesQuery(), cancellationToken);

            return Ok(tableDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadTableDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> GetTableAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadTableDTO tableDTO =
                await _mediator.Send(new GetTableByIdQuery { Id = id }, cancellationToken);

            return Ok(tableDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReadTableDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> InsertTableAsync([FromBody] InsertTableDTO tableDTO,
            CancellationToken cancellationToken)
        {
            var insertTableCommand = _mapper.Map<InsertTableCommand>(tableDTO);

            ReadTableDTO readTableDTO = await _mediator.Send(insertTableCommand, cancellationToken);

            return CreatedAtAction(nameof(GetTableAsync),
                                   new { id = readTableDTO }, tableDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadTableDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> UpdateTableAsync([FromRoute] int id,
            [FromBody] UpdateTableDTO updateTableDTO,
            CancellationToken cancellationToken)
        {
            var updateTableCommand = _mapper.Map<UpdateTableCommand>(updateTableDTO);
            updateTableCommand.Id = id;

            ReadTableDTO tableDTO = await _mediator.Send(updateTableCommand, cancellationToken);

            return Ok(tableDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> DeleteTableAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteTableCommand { Id = id }, cancellationToken);

            return NoContent();
        }
    }
}
