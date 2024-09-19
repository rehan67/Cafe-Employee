using Cafe_Employee.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Employee.Business_Layer.EmployCafe
{
    /// <summary>
    /// Interface for employee-cafe service operations.
    /// </summary>
    public interface IEmployeeCafeService
    {
        /// <summary>
        /// Gets an employee-cafe relationship by employee ID.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>The employee-cafe relationship.</returns>
        Task<EmployeeCafe> GetEmployeeCafeByIdAsync(string employeeId);

        /// <summary>
        /// Adds a new employee-cafe relationship.
        /// </summary>
        /// <param name="employeeCafe">The employee-cafe relationship to add.</param>
        Task AddEmployeeCafeAsync(EmployeeCafe employeeCafe);

        /// <summary>
        /// Saves changes to the data store.
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// Gets all employee-cafe relationships.
        /// </summary>
        /// <returns>A queryable collection of employee-cafe relationships.</returns>
        IQueryable<EmployeeCafe> GetAllEmployeeCafes();

        /// <summary>
        /// Deletes employee-cafe relationships by employee ID.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        Task DeleteByEmployeeIdAsync(string employeeId);
    }
}
