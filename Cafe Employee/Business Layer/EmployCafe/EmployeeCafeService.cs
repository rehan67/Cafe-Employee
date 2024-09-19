using Cafe_Employee.Data.Models;
using Cafe_Employee.Data_Layer.EmployCafe;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Employee.Business_Layer.EmployCafe
{
    /// <summary>
    /// Service for managing employee-cafe relationships.
    /// </summary>
    public class EmployeeCafeService : IEmployeeCafeService
    {
        private readonly IEmployeeCafeRepository _employeeCafeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeCafeService"/> class.
        /// </summary>
        /// <param name="employeeCafeRepository">The repository for employee-cafe relationships.</param>
        public EmployeeCafeService(IEmployeeCafeRepository employeeCafeRepository)
        {
            _employeeCafeRepository = employeeCafeRepository;
        }

        /// <inheritdoc/>
        public async Task<EmployeeCafe> GetEmployeeCafeByIdAsync(string employeeId)
        {
            return await _employeeCafeRepository.GetByIdAsync(employeeId);
        }

        /// <inheritdoc/>
        public async Task AddEmployeeCafeAsync(EmployeeCafe employeeCafe)
        {
            // Add any additional business logic or validation here if needed

            await _employeeCafeRepository.AddAsync(employeeCafe);
            await _employeeCafeRepository.SaveAsync();
        }

        /// <inheritdoc/>
        public Task SaveChangesAsync()
        {
            return _employeeCafeRepository.SaveAsync();
        }

        /// <inheritdoc/>
        public IQueryable<EmployeeCafe> GetAllEmployeeCafes()
        {
            return _employeeCafeRepository.GetAll();
        }

        /// <inheritdoc/>
        public async Task DeleteByEmployeeIdAsync(string employeeId)
        {
            await _employeeCafeRepository.DeleteByEmployeeIdAsync(employeeId);
        }
    }
}
