using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTOs.Exception;
using OrderService.Application.DTOs.Menu;
using OrderService.Application.MediatR.Menu.Commands;
using OrderService.Application.MediatR.Menu.Queries;

namespace OrderService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MenuController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadMenuDTO>))]
        public async Task<IActionResult> GetAllMenuAsync(CancellationToken cancellationToken)
        {
            ICollection<ReadMenuDTO> menuDTOs =
                await _mediator.Send(new GetAllMenuQuery(), cancellationToken);

            return Ok(menuDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadMenuDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> GetMenuAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadMenuDTO menuDTO =
                await _mediator.Send(new GetMenuByIdQuery { Id = id }, cancellationToken);

            return Ok(menuDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReadMenuDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> InsertMenuAsync([FromBody] InsertMenuDTO menuDTO,
            CancellationToken cancellationToken)
        {
            var insertMenuCommand = _mapper.Map<InsertMenuCommand>(menuDTO);

            ReadMenuDTO readMenuDTO = await _mediator.Send(insertMenuCommand, cancellationToken);

            return CreatedAtAction(nameof(GetMenuAsync),
                                   new { id = readMenuDTO }, menuDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadMenuDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> UpdateMenuAsync([FromRoute] int id,
            [FromBody] UpdateMenuDTO updateMenuDTO,
            CancellationToken cancellationToken)
        {
            var updateMenuCommand = _mapper.Map<UpdateMenuCommand>(updateMenuDTO);
            updateMenuCommand.Id = id;

            ReadMenuDTO menuDTO = await _mediator.Send(updateMenuCommand, cancellationToken);

            return Ok(menuDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> DeleteMenuAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteMenuCommand { Id = id }, cancellationToken);

            return NoContent();
        }
    }
}
