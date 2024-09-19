using Cafe_Employee.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Employee.Data_Layer.EmployCafe
{
    /// <summary>
    /// Interface for operations related to the EmployeeCafe entity.
    /// </summary>
    public interface IEmployeeCafeRepository
    {
        /// <summary>
        /// Retrieves an EmployeeCafe by the employee's unique identifier.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee.</param>
        /// <returns>A task representing the EmployeeCafe object if found; otherwise, null.</returns>
        Task<EmployeeCafe> GetByIdAsync(string employeeId);

        /// <summary>
        /// Adds a new EmployeeCafe relationship to the database.
        /// </summary>
        /// <param name="employeeCafe">The EmployeeCafe relationship to be added.</param>
        Task AddAsync(EmployeeCafe employeeCafe);

        /// <summary>
        /// Saves changes made to the EmployeeCafe entity in the database.
        /// </summary>
        /// <returns>A task representing the asynchronous save operation.</returns>
        Task SaveAsync();

        /// <summary>
        /// Retrieves all EmployeeCafe relationships.
        /// </summary>
        /// <returns>An IQueryable collection of EmployeeCafe objects.</returns>
        IQueryable<EmployeeCafe> GetAll();

        /// <summary>
        /// Deletes EmployeeCafe relationships by the employee's unique identifier.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee whose relationships are to be deleted.</param>
        /// <returns>A task representing the asynchronous delete operation.</returns>
        Task DeleteByEmployeeIdAsync(string employeeId);
    }
}
