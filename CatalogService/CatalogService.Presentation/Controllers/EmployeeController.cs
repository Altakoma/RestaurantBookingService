using CatalogService.Application.DTOs.Employee;
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
        public async Task<IActionResult> GetAllEmployeesAsync(
            CancellationToken cancellationToken)
        {
            ICollection<ReadEmployeeDTO> employeeDTOs =
                await _employeeService.GetAllAsync(cancellationToken);

            return Ok(employeeDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            ReadEmployeeDTO employeeDTO = await _employeeService
                .GetByIdAsync(id, cancellationToken);

            return Ok(employeeDTO);
        }

        [HttpPost]
        public async Task<IActionResult> InsertEmployeeAsync(
            [FromBody] InsertEmployeeDTO employeeDTO,
            CancellationToken cancellationToken)
        {
            ReadEmployeeDTO readEmployeeDTO =
                await _employeeService.InsertAsync(employeeDTO, cancellationToken);

            return CreatedAtAction(nameof(GetEmployeeAsync),
                                   new { employeeDTO.Id },employeeDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeAsync([FromRoute] int id,
            [FromBody] UpdateEmployeeDTO updateEmployeeDTO,
            CancellationToken cancellationToken)
        {
            ReadEmployeeDTO employeeDTO = await _employeeService.UpdateAsync(id,
                                           updateEmployeeDTO, cancellationToken);

            return Ok(employeeDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            await _employeeService.DeleteAsync(id, cancellationToken);

            return NoContent();
        }
    }
}
