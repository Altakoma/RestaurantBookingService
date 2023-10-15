using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.DTOs.Exception;
using CatalogService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ReadEmployeeDTO>))]
        public async Task<IActionResult> GetAllEmployeesAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadEmployeeDTO> employeeDTOs =
                await _employeeService.GetAllAsync(cancellationToken);

            return Ok(employeeDTOs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadEmployeeDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> GetEmployeeAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadEmployeeDTO employeeDTO = await _employeeService
                .GetByIdAsync(id, cancellationToken);

            return Ok(employeeDTO);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ReadEmployeeDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> InsertEmployeeAsync(
            [FromBody] InsertEmployeeDTO employeeDTO,
            CancellationToken cancellationToken)
        {
            ReadEmployeeDTO readEmployeeDTO =
                await _employeeService.InsertAsync(employeeDTO, cancellationToken);

            return CreatedAtAction(nameof(GetEmployeeAsync),
                                   new { employeeDTO.Id }, employeeDTO);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadEmployeeDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> UpdateEmployeeAsync([FromRoute] int id,
            [FromBody] UpdateEmployeeDTO updateEmployeeDTO,
            CancellationToken cancellationToken)
        {
            ReadEmployeeDTO employeeDTO = await _employeeService.UpdateAsync(id,
                                           updateEmployeeDTO, cancellationToken);

            return Ok(employeeDTO);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ExceptionDTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ExceptionDTO))]
        public async Task<IActionResult> DeleteEmployeeAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _employeeService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
