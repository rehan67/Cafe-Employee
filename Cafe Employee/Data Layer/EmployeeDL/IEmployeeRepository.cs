using Cafe_Employee.Data.Models;

namespace Cafe_Employee.Data_Layer.EmployeeDL
{
    public interface IEmployeeRepository
    {

        Task<IEnumerable<Employee>> GetAllEmployees();

        Task<IEnumerable<Employee>> GetEmployees(string cafe);
        Task<Employee> GetEmployeeById(string id);
        Task AddEmployeeAsync(Employee employee, EmployeeCafe employeeCafe);
        Task UpdateEmployeeAsync(Employee employee, EmployeeCafe employeeCafe);

        Task DeleteEmployee(string id);
        Task<bool> EmployeeExistsById(string employeeId);
        Task<bool> CafeExistsById(Guid cafeId);
    }
}
