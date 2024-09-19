using Cafe_Employee.Data;
using Cafe_Employee.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Employee.Data_Layer.EmployCafe
{
    /// <summary>
    /// Repository class for handling EmployeeCafe entities.
    /// </summary>
    public class EmployeeCafeRepository : IEmployeeCafeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeCafeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves an EmployeeCafe by the employee's unique identifier.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee.</param>
        /// <returns>A task representing the EmployeeCafe object if found; otherwise, null.</returns>
        public async Task<EmployeeCafe> GetByIdAsync(string employeeId)
        {
            return await _context.EmployeeCafes
                .FirstOrDefaultAsync(ec => ec.EmployeeId == employeeId);
        }

        /// <summary>
        /// Adds a new EmployeeCafe relationship to the database.
        /// </summary>
        /// <param name="employeeCafe">The EmployeeCafe relationship to be added.</param>
        /// <returns>A task representing the asynchronous add operation.</returns>
        public async Task AddAsync(EmployeeCafe employeeCafe)
        {
            await _context.EmployeeCafes.AddAsync(employeeCafe);
        }

        /// <summary>
        /// Saves changes made to the EmployeeCafe entity in the database.
        /// </summary>
        /// <returns>A task representing the asynchronous save operation.</returns>
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Retrieves all EmployeeCafe relationships.
        /// </summary>
        /// <returns>An IQueryable collection of EmployeeCafe objects.</returns>
        public IQueryable<EmployeeCafe> GetAll()
        {
            return _context.EmployeeCafes.AsQueryable();
        }

        /// <summary>
        /// Deletes EmployeeCafe relationships by the employee's unique identifier.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee whose relationships are to be deleted.</param>
        /// <returns>A task representing the asynchronous delete operation.</returns>
        public async Task DeleteByEmployeeIdAsync(string employeeId)
        {
            // Find the EmployeeCafe relationships to delete
            var employeeCafes = _context.EmployeeCafes
                .Where(ec => ec.EmployeeId == employeeId);

            // Remove the found EmployeeCafe relationships
            _context.EmployeeCafes.RemoveRange(employeeCafes);

            // Save changes to the database
            await _context.SaveChangesAsync();
        }
    }
}
