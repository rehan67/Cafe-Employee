using Cafe_Employee.Data.Dto.EmployeeDtos;
using Cafe_Employee.Data.Models;

namespace Cafe_Employee.Business_Layer.EmployeeBL
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployees();
        Task<IEnumerable<EmployeeDto>> GetEmployees(string cafeName);
        Task<EmployeeDto> GetEmployeeById(string id);
        Task<EmployeeDto> AddEmployeeAsync(CreateEmployeeDto employeeDto);
        Task<EmployeeDto> UpdateEmployeeAsync(string employeeId, UpdateEmployeeDto employeeDto);
        Task DeleteEmployee(string id);
        Task<int> CalculateDaysWorked(string employeeId);
    }
}
