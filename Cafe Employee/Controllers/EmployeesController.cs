using Cafe_Employee.Business_Layer.EmployeeBL;
using Cafe_Employee.CustomException;
using Cafe_Employee.Data.Dto.EmployeeDtos;
using Cafe_Employee.Data.ErrorModel;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    // Get All Employee
    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _employeeService.GetAllEmployees();
        return Ok(employees);
    }

    // Get Employee By cafe
    [HttpGet("by-cafe")]
    public async Task<IActionResult> GetEmployeesByCafe([FromQuery] string cafe)
    {
        var employees = await _employeeService.GetEmployees(cafe);
        return Ok(employees);
    }

    // Get Employee by Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeById(string id)
    {
        try
        {
            var employee = await _employeeService.GetEmployeeById(id);

            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            return Ok(employee);
        }
        catch (Exception ex)
        {
            // Log the exception if necessary
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // Add Employee
    [HttpPost]
    public async Task<IActionResult> AddEmployee([FromBody] CreateEmployeeDto createEmployeeDto)
    {
        if (createEmployeeDto == null)
        {
            return BadRequest("Employee data is required.");
        }

        try
        {
            var createdEmployee = await _employeeService.AddEmployeeAsync(createEmployeeDto);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id }, createdEmployee);
        }
        catch (EmployeeAlreadyExistsException ex)
        {
            // Return 409 Conflict with detailed error response
            return Conflict(new ErrorResponse
            {
                StatusCode = 409,
                Message = ex.Message
            });
        }       
        catch (ArgumentException ex)
        {
            // Handle cases where the input is not valid
            return BadRequest(new ErrorResponse
            {
                StatusCode = 400,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            // Handle unexpected errors
            return StatusCode(500, new ErrorResponse
            {
                StatusCode = 500,
                Message = "Internal server error.",
                Details = ex.Message
            });
        }
    }

    // Update Employee
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(string id, [FromBody] UpdateEmployeeDto employeeDto)
    {
        if (employeeDto == null)
        {
            return BadRequest("Employee data is required.");
        }

        try
        {
            var updatedEmployee = await _employeeService.UpdateEmployeeAsync(id, employeeDto);
            return Ok(updatedEmployee);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    // Delete Employee
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(string id)
    {
        await _employeeService.DeleteEmployee(id);
        return NoContent();
    }
}

