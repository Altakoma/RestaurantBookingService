using BookingService.Application.DTOs.Exception;
using BookingService.Application.DTOs.Table;
using BookingService.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadTableDTO>))]
        public async Task<IActionResult> GetAllTablesAsync(CancellationToken cancellationToken)
        {
            ICollection<ReadTableDTO> tableDTOs =
                await _tableService.GetAllAsync<ReadTableDTO>(cancellationToken);

            return Ok(tableDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadTableDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> GetTableAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadTableDTO tableDTO = await _tableService
                .GetByIdAsync<ReadTableDTO>(id, cancellationToken);

            return Ok(tableDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReadTableDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> InsertTableAsync([FromBody] InsertTableDTO tableDTO,
            CancellationToken cancellationToken)
        {
            ReadTableDTO readTableDTO = await _tableService
                .InsertAsync<ReadTableDTO>(tableDTO, cancellationToken);

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
            ReadTableDTO tableDTO = await _tableService
                .UpdateAsync<UpdateTableDTO, ReadTableDTO>(id, updateTableDTO, cancellationToken);

            return Ok(tableDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> DeleteTableAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _tableService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
