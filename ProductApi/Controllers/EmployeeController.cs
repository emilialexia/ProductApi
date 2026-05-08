using Microsoft.AspNetCore.Mvc;
using ProductApi.Core.Services;
using ProductApi.Models.Requests;
using ProductApi.Models.Responses;

namespace ProductApi.Controllers
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

        // GET: api/employee
        [HttpGet]
        public async Task<ActionResult<GetAllEmployeesResponse>> Get()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();

            var response = new GetAllEmployeesResponse
            {
                Employees = employees.Select(e => new EmployeeResponse
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Position = e.Position,
                    Salary = e.Salary
                }).ToList()
            };

            return Ok(response);
        }

        // GET: api/employee/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GetEmployeeResponse>> GetById(Guid id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
                return NotFound($"Employee with id {id} not found");

            var response = new GetEmployeeResponse
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Position = employee.Position,
                Salary = employee.Salary
            };

            return Ok(response);
        }

        // GET: api/employee/position/{position}
        [HttpGet("position/{position}")]
        public async Task<ActionResult<GetEmployeesByPositionResponse>> GetByPosition(string position)
        {
            var employees = await _employeeService.GetEmployeesByPositionAsync(position);

            var response = new GetEmployeesByPositionResponse
            {
                PositionsEmployees = employees.Select(e => new EmployeePositionResponse
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Position = e.Position,
                    Salary = e.Salary
                }).ToList()
            };

            return Ok(response);
        }

        // POST: api/employee
        [HttpPost]
        public async Task<ActionResult<AddEmployeeResponse>> Add([FromBody] AddEmployeeRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.LastName) ||
                string.IsNullOrWhiteSpace(request.Position) ||
                request.Salary <= 0)
            {
                return BadRequest("Invalid employee data");
            }

            var addedEmployee = await _employeeService.AddEmployeeAsync(
                request.FirstName,
                request.LastName,
                request.Position,
                request.Salary);

            var response = new AddEmployeeResponse
            {
                Id = addedEmployee.Id,
                FirstName = addedEmployee.FirstName,
                LastName = addedEmployee.LastName,
                Position = addedEmployee.Position,
                Salary = addedEmployee.Salary
            };

            return CreatedAtAction(nameof(GetById), new { id = addedEmployee.Id }, response);
        }

        // PUT: api/employee/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateEmployeeResponse>> Update(Guid id, [FromBody] UpdateEmployeeRequest request)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.FirstName) ||
                string.IsNullOrWhiteSpace(request.LastName) ||
                string.IsNullOrWhiteSpace(request.Position) ||
                request.Salary <= 0)
            {
                return BadRequest("Invalid employee data");
            }

            try
            {
                await _employeeService.UpdateEmployeeAsync(
                    id,
                    request.FirstName,
                    request.LastName,
                    request.Position,
                    request.Salary);

                var response = new UpdateEmployeeResponse
                {
                    Message = "Employee updated successfully",
                    Success = true
                };

                return Ok(response);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Employee with id {id} not found");
            }
        }

        // DELETE: api/employee/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeleteEmployeeResponse>> Delete(Guid id)
        {
            var deleted = await _employeeService.DeleteEmployeeAsync(id);

            if (!deleted)
                return NotFound($"Employee with id {id} not found");

            var response = new DeleteEmployeeResponse
            {
                Message = "Employee deleted successfully",
                Success = true
            };

            return Ok(response);
        }
    }
}