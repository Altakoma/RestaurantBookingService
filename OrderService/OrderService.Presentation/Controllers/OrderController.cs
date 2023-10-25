using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.DTOs.Exception;
using OrderService.Application.DTOs.Order;
using OrderService.Application.MediatR.Order.Commands;
using OrderService.Application.MediatR.Order.Queries;

namespace OrderService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrderController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadOrderDTO>))]
        public async Task<IActionResult> GetAllOrderAsync(CancellationToken cancellationToken)
        {
            ICollection<ReadOrderDTO> orderDTOs =
                await _mediator.Send(new GetAllOrdersQuery(), cancellationToken);

            return Ok(orderDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadOrderDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> GetOrderAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadOrderDTO orderDTO =
                await _mediator.Send(new GetOrderByIdQuery { Id = id }, cancellationToken);

            return Ok(orderDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReadOrderDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> InsertOrderAsync([FromBody] InsertOrderDTO orderDTO,
            CancellationToken cancellationToken)
        {
            var insertOrderCommand = _mapper.Map<InsertOrderCommand>(orderDTO);

            ReadOrderDTO readOrderDTO = await _mediator.Send(insertOrderCommand, cancellationToken);

            return CreatedAtAction(nameof(GetOrderAsync),
                                   new { id = readOrderDTO }, orderDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadOrderDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> UpdateOrderAsync([FromRoute] int id,
            [FromBody] UpdateOrderDTO updateOrderDTO,
            CancellationToken cancellationToken)
        {
            var updateOrderCommand = _mapper.Map<UpdateOrderCommand>(updateOrderDTO);
            updateOrderCommand.Id = id;

            ReadOrderDTO orderDTO = await _mediator.Send(updateOrderCommand, cancellationToken);

            return Ok(orderDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> DeleteOrderAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteOrderCommand { Id = id }, cancellationToken);

            return NoContent();
        }
    }
}
