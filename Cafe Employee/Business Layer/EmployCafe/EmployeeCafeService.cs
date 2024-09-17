using Cafe_Employee.Data.Models;
using Cafe_Employee.Data_Layer.EmployCafe;

namespace Cafe_Employee.Business_Layer.EmployCafe
{
    public class EmployeeCafeService : IEmployeeCafeService
    {
        private readonly IEmployeeCafeRepository _employeeCafeRepository;

        public EmployeeCafeService(IEmployeeCafeRepository employeeCafeRepository)
        {
            _employeeCafeRepository = employeeCafeRepository;
        }

        public async Task<EmployeeCafe> GetEmployeeCafeByIdAsync(string employeeId)
        {
            return await _employeeCafeRepository.GetByIdAsync(employeeId);
        }

        public async Task AddEmployeeCafeAsync(EmployeeCafe employeeCafe)
        {
            // You can add any additional business logic or validation here if needed

            await _employeeCafeRepository.AddAsync(employeeCafe);
            await _employeeCafeRepository.SaveAsync();
        }
      

        public Task SaveChangesAsync()
        {
            return _employeeCafeRepository.SaveAsync();
        }

        public IQueryable<EmployeeCafe> GetAllEmployeeCafes()
        {
            return _employeeCafeRepository.GetAll();
        }
        public async Task DeleteByEmployeeIdAsync(string employeeId)
        {
            await _employeeCafeRepository.DeleteByEmployeeIdAsync(employeeId);
        }

    }
}
