using Cafe_Employee.Data.Models;

namespace Cafe_Employee.Data_Layer.EmployCafe
{
    public interface IEmployeeCafeRepository
    {
        Task<EmployeeCafe> GetByIdAsync(string employeeId);
        Task AddAsync(EmployeeCafe employeeCafe);
        Task SaveAsync();
        IQueryable<EmployeeCafe> GetAll();
        Task DeleteByEmployeeIdAsync(string employeeId);
    }
}
