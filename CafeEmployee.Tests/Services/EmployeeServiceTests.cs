using Cafe_Employee.Business_Layer.EmployeeBL;
using Cafe_Employee.CustomException;
using Cafe_Employee.Data.Dto.EmployeeDtos;
using Cafe_Employee.Data.Models;
using Cafe_Employee.Data_Layer.EmployCafe;
using Cafe_Employee.Data_Layer.EmployeeDL;
using Moq;

namespace CafeEmployee.Tests.Services;

public class EmployeeServiceTests
{
    private readonly EmployeeService _service;
    private readonly Mock<IEmployeeRepository> _mockEmployeeRepository;
    private readonly Mock<IEmployeeCafeRepository> _mockEmployeeCafeRepository;

    public EmployeeServiceTests()
    {
        _mockEmployeeRepository = new Mock<IEmployeeRepository>();
        _mockEmployeeCafeRepository = new Mock<IEmployeeCafeRepository>();
        _service = new EmployeeService(_mockEmployeeRepository.Object, _mockEmployeeCafeRepository.Object);
    }


    [Fact]
    public async Task GetAllEmployees_ReturnsAllEmployees()
    {
        var employees = new List<Employee>
    {
        new Employee
        {
            Id = "1",
            Name = "John Doe",
            EmailAddress = "john.doe@example.com",
            PhoneNumber = "123-456-7890",
            Gender = "Male"
        }
    };

        _mockEmployeeRepository.Setup(r => r.GetAllEmployees()).ReturnsAsync(employees);

        var result = await _service.GetAllEmployees();

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("John Doe", result.First().Name);
    }


    [Fact]
    public async Task GetEmployeeById_ReturnsEmployee_WhenEmployeeExists()
    {
        var employee = new Employee
        {
            Id = "1",
            Name = "John Doe",
            EmailAddress = "john.doe@example.com",
            PhoneNumber = "123-456-7890",
            Gender = "Male",
            EmployeeCafes = new List<EmployeeCafe>
                {
                    new EmployeeCafe
                    {
                        Cafe = new Cafe { Id = Guid.NewGuid(), Name = "Cafe 1" }
                    }
                }
        };

        _mockEmployeeRepository.Setup(r => r.GetEmployeeById("1")).ReturnsAsync(employee);

        var result = await _service.GetEmployeeById("1");

        Assert.NotNull(result);
        Assert.Equal("John Doe", result.Name);
        Assert.Equal("Cafe 1", result.Cafe.Cafe);
    }

    [Fact]
    public async Task AddEmployeeAsync_ThrowsException_WhenEmployeeAlreadyExists()
    {
        var employeeDto = new CreateEmployeeDto
        {
            Id = "1",
            Name = "John Doe",
            EmailAddress = "john.doe@example.com",
            PhoneNumber = "123-456-7890",
            Gender = "Male",
            CafeId = Guid.NewGuid()
        };

        _mockEmployeeRepository.Setup(r => r.GetEmployeeById("1")).ReturnsAsync(new Employee());

        await Assert.ThrowsAsync<EmployeeAlreadyExistsException>(() => _service.AddEmployeeAsync(employeeDto));
    }

    [Fact]
    public async Task AddEmployeeAsync_ThrowsException_WhenEmployeeCafeAlreadyExists()
    {
        var employeeDto = new CreateEmployeeDto
        {
            Id = "2",
            Name = "Jane Smith",
            EmailAddress = "jane.smith@example.com",
            PhoneNumber = "123-456-7890",
            Gender = "Female",
            CafeId = Guid.NewGuid()
        };

        _mockEmployeeRepository.Setup(r => r.GetEmployeeById("2")).ReturnsAsync((Employee)null);
        _mockEmployeeCafeRepository.Setup(r => r.GetByIdAsync("2")).ReturnsAsync(new EmployeeCafe());

        await Assert.ThrowsAsync<EmployeeAlreadyExistsException>(() => _service.AddEmployeeAsync(employeeDto));
    }

    [Fact]
    public async Task UpdateEmployeeAsync_ThrowsException_WhenEmployeeDoesNotExist()
    {
        var employeeDto = new UpdateEmployeeDto
        {
            Name = "Jane Smith",
            EmailAddress = "jane.smith@example.com",
            PhoneNumber = "123-456-7890",
            Gender = "Female",
            CafeId = Guid.NewGuid()
        };

        _mockEmployeeRepository.Setup(r => r.GetEmployeeById("1")).ReturnsAsync((Employee)null);

        await Assert.ThrowsAsync<ArgumentException>(() => _service.UpdateEmployeeAsync("1", employeeDto));
    }

    [Fact]
    public async Task DeleteEmployee_CallsRepositoryDeleteMethod()
    {
        _mockEmployeeRepository.Setup(r => r.DeleteEmployee("1")).Returns(Task.CompletedTask);

        await _service.DeleteEmployee("1");

        _mockEmployeeRepository.Verify(r => r.DeleteEmployee("1"), Times.Once());
    }

    [Fact]
    public async Task CalculateDaysWorked_ReturnsCorrectNumberOfDays()
    {
        var employee = new Employee
        {
            Id = "1",
            EmployeeCafes = new List<EmployeeCafe>
                {
                    new EmployeeCafe
                    {
                        StartDate = DateTime.Now.AddDays(-10)
                    }
                }
        };

        _mockEmployeeRepository.Setup(r => r.GetEmployeeById("1")).ReturnsAsync(employee);

        var result = await _service.CalculateDaysWorked("1");

        Assert.Equal(10, result);
    }
}

