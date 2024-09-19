using Cafe_Employee.Business_Layer.EmployeeBL;
using Cafe_Employee.Controllers;
using Cafe_Employee.CustomException;
using Cafe_Employee.Data.Dto.EmployeeDtos;
using Cafe_Employee.Data.ErrorModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace CafeEmployee.Tests.Controllers;
public class EmployeesControllerTests
{
    private readonly Mock<IEmployeeService> _mockEmployeeService;
    private readonly EmployeesController _controller;

    public EmployeesControllerTests()
    {
        _mockEmployeeService = new Mock<IEmployeeService>();
        _controller = new EmployeesController(_mockEmployeeService.Object);
    }

    [Fact]
    public async Task GetEmployees_ReturnsOkResult_WithEmployeeList()
    {
        // Arrange
        var employees = new List<EmployeeDto>
        {
            new EmployeeDto { Id = "1", Name = "John Doe" },
            new EmployeeDto { Id = "2", Name = "Jane Smith" }
        };
        _mockEmployeeService.Setup(service => service.GetAllEmployees()).ReturnsAsync(employees);

        // Act
        var result = await _controller.GetEmployees();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<EmployeeDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetEmployeesByCafe_ReturnsOkResult_WithEmployeeList()
    {
        // Arrange
        var cafeName = "Cafe 1";
        var employees = new List<EmployeeDto>
        {
            new EmployeeDto { Id = "1", Name = "John Doe", Cafe = new CafeDropdown { CafeId = Guid.NewGuid(), Cafe = cafeName } }
        };
        _mockEmployeeService.Setup(service => service.GetEmployees(cafeName)).ReturnsAsync(employees);

        // Act
        var result = await _controller.GetEmployeesByCafe(cafeName);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<EmployeeDto>>(okResult.Value);
        Assert.Single(returnValue);
        Assert.Equal(cafeName, returnValue.First().Cafe.Cafe);
    }

    [Fact]
    public async Task GetEmployeeById_ReturnsOkResult_WithEmployee()
    {
        // Arrange
        var employeeId = "1";
        var employee = new EmployeeDto { Id = employeeId, Name = "John Doe" };
        _mockEmployeeService.Setup(service => service.GetEmployeeById(employeeId)).ReturnsAsync(employee);

        // Act
        var result = await _controller.GetEmployeeById(employeeId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<EmployeeDto>(okResult.Value);
        Assert.Equal(employeeId, returnValue.Id);
    }

    [Fact]
    public async Task GetEmployeeById_ReturnsNotFoundResult_WhenEmployeeDoesNotExist()
    {
        // Arrange
        var employeeId = "1";
        _mockEmployeeService.Setup(service => service.GetEmployeeById(employeeId)).ReturnsAsync((EmployeeDto)null);

        // Act
        var result = await _controller.GetEmployeeById(employeeId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal($"Employee with ID {employeeId} not found.", notFoundResult.Value);
    }

    [Fact]
    public async Task AddEmployee_ReturnsCreatedAtActionResult_WithEmployee()
    {
        // Arrange
        var createEmployeeDto = new CreateEmployeeDto { Id = "3", Name = "Alice Johnson", CafeId = Guid.NewGuid() };
        var employee = new EmployeeDto { Id = "3", Name = "Alice Johnson" };
        _mockEmployeeService.Setup(service => service.AddEmployeeAsync(createEmployeeDto)).ReturnsAsync(employee);

        // Act
        var result = await _controller.AddEmployee(createEmployeeDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        var returnValue = Assert.IsType<EmployeeDto>(createdAtActionResult.Value);
        Assert.Equal("3", returnValue.Id);
    }

    [Fact]
    public async Task AddEmployee_ReturnsConflictResult_WhenEmployeeAlreadyExists()
    {
        // Arrange
        var createEmployeeDto = new CreateEmployeeDto { Id = "1", Name = "John Doe", CafeId = Guid.NewGuid() };
        _mockEmployeeService.Setup(service => service.AddEmployeeAsync(createEmployeeDto))
                            .ThrowsAsync(new EmployeeAlreadyExistsException("Employee already exists"));

        // Act
        var result = await _controller.AddEmployee(createEmployeeDto);

        // Assert
        var conflictResult = Assert.IsType<ConflictObjectResult>(result);
        var errorResponse = Assert.IsType<ErrorResponse>(conflictResult.Value);
        Assert.Equal(409, errorResponse.StatusCode);
    }

    [Fact]
    public async Task UpdateEmployee_ReturnsOkResult_WithUpdatedEmployee()
    {
        // Arrange
        var employeeId = "1";
        var updateEmployeeDto = new UpdateEmployeeDto { Name = "John Updated" };
        var updatedEmployee = new EmployeeDto { Id = employeeId, Name = "John Updated" };
        _mockEmployeeService.Setup(service => service.UpdateEmployeeAsync(employeeId, updateEmployeeDto)).ReturnsAsync(updatedEmployee);

        // Act
        var result = await _controller.UpdateEmployee(employeeId, updateEmployeeDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<EmployeeDto>(okResult.Value);
        Assert.Equal("John Updated", returnValue.Name);
    }

    [Fact]
    public async Task UpdateEmployee_ReturnsNotFoundResult_WhenEmployeeDoesNotExist()
    {
        // Arrange
        var employeeId = "1";
        var updateEmployeeDto = new UpdateEmployeeDto { Name = "John Updated" };
        _mockEmployeeService.Setup(service => service.UpdateEmployeeAsync(employeeId, updateEmployeeDto))
                            .ThrowsAsync(new ArgumentException("Employee not found"));

        // Act
        var result = await _controller.UpdateEmployee(employeeId, updateEmployeeDto);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal("Employee not found", notFoundResult.Value);
    }

    [Fact]
    public async Task DeleteEmployee_ReturnsNoContentResult()
    {
        // Arrange
        var employeeId = "1";

        // Act
        var result = await _controller.DeleteEmployee(employeeId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
