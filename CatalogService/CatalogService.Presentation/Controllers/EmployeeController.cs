using CatalogService.Application.DTOs.Employee;
using CatalogService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            ICollection<EmployeeDTO> employeeDTOs = await _employeeService.GetAllAsync();

            return Ok(employeeDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            EmployeeDTO employeeDTO = await _employeeService.GetByIdAsync(id);

            return Ok(employeeDTO);
        }

        [HttpPost]
        public async Task<IActionResult> InsertEmployee(EmployeeDTO employeeDTO)
        {
            employeeDTO = await _employeeService.InsertAsync(employeeDTO);

            return CreatedAtAction(nameof(GetEmployee), employeeDTO, employeeDTO.Id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id,
            UpdateEmployeeDTO updateEmployeeDTO)
        {
            EmployeeDTO employeeDTO = await _employeeService.UpdateAsync(id, updateEmployeeDTO);

            return Ok(employeeDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteAsync(id);

            return NoContent();
        }
    }
}
