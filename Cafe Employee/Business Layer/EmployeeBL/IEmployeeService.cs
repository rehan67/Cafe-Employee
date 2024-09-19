using Cafe_Employee.Data.Dto.EmployeeDtos;
using Cafe_Employee.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cafe_Employee.Business_Layer.EmployeeBL
{
    /// <summary>
    /// Interface for employee services.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns>A list of employee DTOs.</returns>
        Task<IEnumerable<EmployeeDto>> GetAllEmployees();

        /// <summary>
        /// Gets employees by cafe name.
        /// </summary>
        /// <param name="cafeName">The name of the cafe.</param>
        /// <returns>A list of employee DTOs.</returns>
        Task<IEnumerable<EmployeeDto>> GetEmployees(string cafeName);

        /// <summary>
        /// Gets an employee by ID.
        /// </summary>
        /// <param name="id">The employee ID.</param>
        /// <returns>An employee DTO.</returns>
        Task<EmployeeDto> GetEmployeeById(string id);

        /// <summary>
        /// Adds a new employee.
        /// </summary>
        /// <param name="employeeDto">The employee data.</param>
        /// <returns>The created employee DTO.</returns>
        Task<EmployeeDto> AddEmployeeAsync(CreateEmployeeDto employeeDto);

        /// <summary>
        /// Updates an existing employee.
        /// </summary>
        /// <param name="employeeId">The ID of the employee to update.</param>
        /// <param name="employeeDto">The updated employee data.</param>
        /// <returns>The updated employee DTO.</returns>
        Task<EmployeeDto> UpdateEmployeeAsync(string employeeId, UpdateEmployeeDto employeeDto);

        /// <summary>
        /// Deletes an employee by ID.
        /// </summary>
        /// <param name="id">The employee ID.</param>
        Task DeleteEmployee(string id);

        /// <summary>
        /// Calculates the number of days worked by an employee.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>The number of days worked.</returns>
        Task<int> CalculateDaysWorked(string employeeId);
    }
}
