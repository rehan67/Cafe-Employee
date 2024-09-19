using Cafe_Employee.Data.Models;

namespace Cafe_Employee.Data_Layer.EmployeeDL
{
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Retrieves all employees.
        /// </summary>
        /// <returns>A task representing an enumerable collection of Employee objects.</returns>
        Task<IEnumerable<Employee>> GetAllEmployees();

        /// <summary>
        /// Retrieves employees for a specific cafe.
        /// </summary>
        /// <param name="cafe">The cafe name or identifier to filter employees by.</param>
        /// <returns>A task representing an enumerable collection of Employee objects.</returns>
        Task<IEnumerable<Employee>> GetEmployees(string cafe);

        /// <summary>
        /// Retrieves an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee.</param>
        /// <returns>A task representing an Employee object.</returns>
        Task<Employee> GetEmployeeById(string id);

        /// <summary>
        /// Adds a new employee along with their association to a cafe.
        /// </summary>
        /// <param name="employee">The employee to be added.</param>
        /// <param name="employeeCafe">The EmployeeCafe object representing the employee-cafe association.</param>
        Task AddEmployeeAsync(Employee employee, EmployeeCafe employeeCafe);

        /// <summary>
        /// Updates an existing employee and their association with a cafe.
        /// </summary>
        /// <param name="employee">The employee to be updated.</param>
        /// <param name="employeeCafe">The EmployeeCafe object representing the updated employee-cafe association.</param>
        Task UpdateEmployeeAsync(Employee employee, EmployeeCafe employeeCafe);

        /// <summary>
        /// Deletes an employee by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the employee to be deleted.</param>
        Task DeleteEmployee(string id);

        /// <summary>
        /// Checks if an employee exists by their unique identifier.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee.</param>
        /// <returns>A task representing a boolean indicating if the employee exists.</returns>
        Task<bool> EmployeeExistsById(string employeeId);

        /// <summary>
        /// Checks if a cafe exists by its unique identifier.
        /// </summary>
        /// <param name="cafeId">The unique identifier of the cafe.</param>
        /// <returns>A task representing a boolean indicating if the cafe exists.</returns>
        Task<bool> CafeExistsById(Guid cafeId);
    }
}
