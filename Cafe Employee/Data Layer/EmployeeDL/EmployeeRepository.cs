using Cafe_Employee.Data;
using Cafe_Employee.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Employee.Data_Layer.EmployeeDL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves employees for a specific cafe.
        /// </summary>
        /// <param name="cafeName">The name of the cafe to filter employees by.</param>
        /// <returns>A collection of Employee objects.</returns>
        public async Task<IEnumerable<Employee>> GetEmployees(string cafeName)
        {
            var employees = _context.Employees
                .Include(e => e.EmployeeCafes)
                .ThenInclude(ec => ec.Cafe)
                .Where(e => string.IsNullOrEmpty(cafeName) || e.EmployeeCafes.Any(ec => ec.Cafe.Name == cafeName));

            return await employees.ToListAsync();
        }

        /// <summary>
        /// Retrieves an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>The Employee object if found; otherwise, null.</returns>
        public async Task<Employee> GetEmployeeById(string id)
        {
            return await _context.Employees
                .Include(e => e.EmployeeCafes)
                .ThenInclude(ec => ec.Cafe)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        /// <summary>
        /// Adds a new employee and their association with a cafe.
        /// </summary>
        /// <param name="employee">The employee to add.</param>
        /// <param name="employeeCafe">The EmployeeCafe object representing the association.</param>
        public async Task AddEmployeeAsync(Employee employee, EmployeeCafe employeeCafe)
        {
            try
            {
                _context.Employees.Add(employee);
                _context.EmployeeCafes.Add(employeeCafe);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                // _logger.LogError(ex, "An error occurred while adding the employee.");
                throw new InvalidOperationException("An error occurred while adding the employee.", ex);
            }
        }

        /// <summary>
        /// Updates an existing employee and their association with a cafe.
        /// </summary>
        /// <param name="employee">The updated employee details.</param>
        /// <param name="employeeCafe">The EmployeeCafe object representing the updated association.</param>
        public async Task UpdateEmployeeAsync(Employee employee, EmployeeCafe employeeCafe)
        {
            var existingEmployee = await _context.Employees
                .Include(e => e.EmployeeCafes)
                .FirstOrDefaultAsync(e => e.Id == employee.Id);

            if (existingEmployee == null)
                throw new ArgumentException("Employee not found");

            // Update employee details
            existingEmployee.Name = employee.Name;
            existingEmployee.EmailAddress = employee.EmailAddress;
            existingEmployee.PhoneNumber = employee.PhoneNumber;
            existingEmployee.Gender = employee.Gender;

            // Update or add employee-cafe association
            var existingCafe = existingEmployee.EmployeeCafes
                .FirstOrDefault(ec => ec.CafeId == employeeCafe.CafeId);

            if (existingCafe != null)
            {
                existingCafe.StartDate = employeeCafe.StartDate;
            }
            else
            {
                _context.EmployeeCafes.Add(employeeCafe);
            }

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to delete.</param>
        public async Task DeleteEmployee(string id)
        {
            var employee = await _context.Employees
                .Include(e => e.EmployeeCafes)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee != null)
            {
                _context.EmployeeCafes.RemoveRange(employee.EmployeeCafes);
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Checks if an employee exists by their unique identifier.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee.</param>
        /// <returns>True if the employee exists; otherwise, false.</returns>
        public async Task<bool> EmployeeExistsById(string employeeId)
        {
            return await _context.Employees.AnyAsync(e => e.Id == employeeId);
        }

        /// <summary>
        /// Checks if a cafe exists by its unique identifier.
        /// </summary>
        /// <param name="cafeId">The unique identifier of the cafe.</param>
        /// <returns>True if the cafe exists; otherwise, false.</returns>
        public async Task<bool> CafeExistsById(Guid cafeId)
        {
            return await _context.Cafes.AnyAsync(c => c.Id == cafeId);
        }

        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>A collection of Employee objects.</returns>
        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await _context.Employees
                .Include(e => e.EmployeeCafes)
                .ThenInclude(ec => ec.Cafe)
                .ToListAsync();
        }
    }
}
