using Cafe_Employee.Data;
using Cafe_Employee.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Employee.Data_Layer.EmployeeDL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployees(string cafeName)
        {
            var employees = _context.Employees
                .Include(e => e.EmployeeCafes)
                .ThenInclude(relation => relation.Cafe)
                .Where(e => string.IsNullOrEmpty(cafeName) || e.EmployeeCafes.Any(r => r.Cafe.Name == cafeName));

            return await employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(string id)
        {
            return await _context.Employees?
                .Include(e => e.EmployeeCafes)
                .ThenInclude(relation => relation.Cafe)
                .FirstOrDefaultAsync(e => e.Id == id)!;
        }

        public async Task AddEmployeeAsync(Employee employee, EmployeeCafe employeeCafe)
        {
            _context.Employees.Add(employee);
            _context.EmployeeCafes.Add(employeeCafe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEmployeeAsync(Employee employee, EmployeeCafe employeeCafe)
        {
            var existingRelation = await _context.EmployeeCafes
                .FirstOrDefaultAsync(r => r.EmployeeId == employee.Id);

            if (existingRelation != null)
            {
                existingRelation.CafeId = employeeCafe.CafeId;
                existingRelation.StartDate = employeeCafe.StartDate;
            }
            else
            {
                _context.EmployeeCafes.Add(employeeCafe);
            }

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployee(string id)
        {
            var employee = await _context.Employees
                .Include(e => e.EmployeeCafes)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee != null)
            {
                _context.EmployeeCafes.RemoveRange(employee.EmployeeCafes);
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<bool> EmployeeExistsById(string employeeId)
        {
            return await _context.Employees.AnyAsync(e => e.Id == employeeId);
        }

        public async Task<bool> CafeExistsById(Guid cafeId)
        {
            return await _context.Cafes.AnyAsync(c => c.Id == cafeId);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {             

            return await _context.Employees
                .Include(e => e.EmployeeCafes)
                .ThenInclude(relation => relation.Cafe).ToListAsync();
        }
    }
}
