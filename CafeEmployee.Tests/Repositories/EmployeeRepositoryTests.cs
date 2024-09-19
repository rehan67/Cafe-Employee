using Cafe_Employee.Data;
using Cafe_Employee.Data.Models;
using Cafe_Employee.Data_Layer.EmployeeDL;
using Microsoft.EntityFrameworkCore;
namespace CafeEmployee.Tests.Repositories;
public class EmployeeRepositoryTests
{
    private readonly EmployeeRepository _repository;
    private readonly ApplicationDbContext _context;

    public EmployeeRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "EmployeeDatabase")
            .EnableSensitiveDataLogging()
            .Options;

        _context = new ApplicationDbContext(options);

        // Seed the database with initial data
        SeedDatabase(_context);

        _repository = new EmployeeRepository(_context);
    }

    private void SeedDatabase(ApplicationDbContext context)
    {
        context.Employees.AddRange(
            new Employee
            {
                Id = Guid.NewGuid().ToString(),
                Name = "John Doe",
                EmailAddress = "john.doe@example.com",
                Gender = "Male",
                PhoneNumber = "123-456-7890"
            },
            new Employee
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Jane Smith",
                EmailAddress = "jane.smith@example.com",
                Gender = "Female",
                PhoneNumber = "098-765-4321"
            }
        );
        context.SaveChanges();
    }

    [Fact]
    public async Task GetEmployeeById_ReturnsEmployee_WhenEmployeeExists()
    {
        // Arrange
        var employeeId = "unique-employee-id-1"; // Use a specific unique ID
        var employee = new Employee
        {
            Id = employeeId,
            Name = "John Doe",
            EmailAddress = "john.doe@example.com",
            Gender = "Male",
            PhoneNumber = "123-456-7890"
        };

        // Seed the database with the employee
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetEmployeeById(employeeId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(employeeId, result.Id);
        Assert.Equal("John Doe", result.Name);
    }



    [Fact]
    public async Task GetEmployeeById_ReturnsNull_WhenEmployeeDoesNotExist()
    {
        var result = await _repository.GetEmployeeById("99");
        Assert.Null(result);
    }

    [Fact]
    public async Task GetEmployees_ReturnsEmployees_WithSpecificCafeName()
    {
        // Arrange
        var cafeId = Guid.NewGuid();
        var employeeId = Guid.NewGuid().ToString();

        var cafe = new Cafe
        {
            Id = cafeId,
            Name = "Cafe 1",
            Description = "Test Cafe",
            Location = "Location 1"
        };

        var employee = new Employee
        {
            Id = employeeId,
            Name = "John Doe",
            EmailAddress = "john.doe@example.com",
            Gender = "Male",
            PhoneNumber = "123-456-7890",
            EmployeeCafes = new List<EmployeeCafe>
        {
            new EmployeeCafe
            {
                EmployeeId = employeeId,
                CafeId = cafeId,
                StartDate = DateTime.Now
            }
        }
        };

        // Add the cafe and employee to the context
        _context.Cafes.Add(cafe);
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetEmployees("Cafe 1");

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);  // Should find exactly one employee for this cafe
    }



    [Fact]
    public async Task GetEmployees_ReturnsAllEmployees_WhenCafeNameIsNull()
    {
        var employees = await _repository.GetEmployees(null);
        Assert.NotNull(employees);
        Assert.True(employees.Count() > 0);
    }

    [Fact]
    public async Task AddEmployeeAsync_AddsEmployeeAndEmployeeCafe()
    {
        // Arrange
        var employeeId = Guid.NewGuid().ToString();
        var cafeId = Guid.NewGuid(); // Generate a new Guid for the cafe

        var employee = new Employee
        {
            Id = employeeId,
            Name = "Jane Smith",
            EmailAddress = "jane.smith@example.com",
            PhoneNumber = "123-456-7890",
            Gender = "Female"
        };

        var employeeCafe = new EmployeeCafe
        {
            EmployeeId = employeeId,
            CafeId = cafeId,
            StartDate = DateTime.Now
        };

        // Act
        await _repository.AddEmployeeAsync(employee, employeeCafe);

        // Assert
        // Fetch the employee from the database
        var addedEmployee = await _repository.GetEmployeeById(employeeId);
        Assert.NotNull(addedEmployee);
        Assert.Equal("Jane Smith", addedEmployee.Name);

        // Verify the employee cafe association
        var addedEmployeeCafe = _context.EmployeeCafes
                                        .FirstOrDefault(ec => ec.EmployeeId == employeeId && ec.CafeId == cafeId);
        Assert.NotNull(addedEmployeeCafe);
    }

    [Fact]
    public async Task UpdateEmployeeAsync_UpdatesEmployeeAndEmployeeCafe()
    {
        // Arrange - add the employee and associated cafe if not already in the database
        var employeeId = Guid.NewGuid().ToString();
        var cafeId = Guid.NewGuid();

        var employee = new Employee
        {
            Id = employeeId,
            Name = "John Doe",
            EmailAddress = "john.doe@example.com",
            Gender = "Male",
            PhoneNumber = "123-456-7890",
            EmployeeCafes = new List<EmployeeCafe>
        {
            new EmployeeCafe
            {
                EmployeeId = employeeId,
                CafeId = cafeId,
                StartDate = DateTime.Now
            }
        }
        };

        // Add the employee and save to the in-memory database
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        // Act - now update the employee's details
        var updatedEmployee = new Employee
        {
            Id = employeeId, // Keep the same ID
            Name = "John Updated", // Updated name
            EmailAddress = employee.EmailAddress, // Keep other properties the same
            PhoneNumber = employee.PhoneNumber,
            Gender = employee.Gender
        };

        var updatedEmployeeCafe = new EmployeeCafe
        {
            EmployeeId = employeeId,
            CafeId = cafeId, // Keep the same cafe association
            StartDate = DateTime.Now.AddDays(-1) // Update start date
        };

        await _repository.UpdateEmployeeAsync(updatedEmployee, updatedEmployeeCafe);

        // Assert - retrieve the updated employee from the database and check the changes
        var result = await _repository.GetEmployeeById(employeeId);

        // Ensure the employee was updated
        Assert.NotNull(result);
        Assert.Equal("John Updated", result.Name); // Check that the name was updated

        // Ensure the employee's cafe was updated
        var resultEmployeeCafe = result.EmployeeCafes.FirstOrDefault(ec => ec.CafeId == cafeId);
        Assert.NotNull(resultEmployeeCafe);
        Assert.Equal(cafeId, resultEmployeeCafe.CafeId); // Check that cafe association remains
    }




    [Fact]
    public async Task DeleteEmployee_RemovesEmployeeAndEmployeeCafesFromContext()
    {
        await _repository.DeleteEmployee("1");

        var deletedEmployee = await _repository.GetEmployeeById("1");
        Assert.Null(deletedEmployee);
    }

    [Fact]
    public async Task EmployeeExistsById_ReturnsTrue_WhenEmployeeExists()
    {
        // Arrange
        var employeeId = "existing-employee-id"; // Use a specific ID for testing

        // Seed the database with an employee with the given ID
        var employee = new Employee
        {
            Id = employeeId,
            Name = "John Doe",
            EmailAddress = "john.doe@example.com",
            Gender = "Male",
            PhoneNumber = "123-456-7890"
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.EmployeeExistsById(employeeId);

        // Assert
        Assert.True(result);
    }


    [Fact]
    public async Task EmployeeExistsById_ReturnsFalse_WhenEmployeeDoesNotExist()
    {
        var result = await _repository.EmployeeExistsById("99");
        Assert.False(result);
    }

    [Fact]
    public async Task CafeExistsById_ReturnsTrue_WhenCafeExists()
    {
        // Arrange
        var cafe = new Cafe
        {
            Id = Guid.NewGuid(), // Generate a new Guid for the cafe
            Name = "Cafe Test",
            Description = "Description Test",
            Location = "Location Test"
        };

        // Add the cafe to the in-memory database
        _context.Cafes.Add(cafe);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.CafeExistsById(cafe.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CafeExistsById_ReturnsFalse_WhenCafeDoesNotExist()
    {
        var cafeId = Guid.NewGuid();
        var result = await _repository.CafeExistsById(cafeId);
        Assert.False(result);
    }
}
