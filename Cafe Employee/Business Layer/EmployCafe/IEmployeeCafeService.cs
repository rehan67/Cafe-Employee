using Cafe_Employee.Data.Models;

namespace Cafe_Employee.Business_Layer.EmployCafe
{
    public interface IEmployeeCafeService
    {
        Task<EmployeeCafe> GetEmployeeCafeByIdAsync(string employeeId);
        Task AddEmployeeCafeAsync(EmployeeCafe employeeCafe);

        Task SaveChangesAsync();
        IQueryable<EmployeeCafe> GetAllEmployeeCafes();
        Task DeleteByEmployeeIdAsync(string employeeId);

    }
}
