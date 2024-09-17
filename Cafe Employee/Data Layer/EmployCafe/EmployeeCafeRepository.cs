using Cafe_Employee.Data;
using Cafe_Employee.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Employee.Data_Layer.EmployCafe
{
    public class EmployeeCafeRepository : IEmployeeCafeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeCafeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeCafe> GetByIdAsync(string employeeId)
        {
            return await _context.EmployeeCafes
                .FirstOrDefaultAsync(ec => ec.EmployeeId == employeeId);
        }

        public async Task AddAsync(EmployeeCafe employeeCafe)
        {
            await _context.EmployeeCafes.AddAsync(employeeCafe);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IQueryable<EmployeeCafe> GetAll()
        {
            return _context.EmployeeCafes.AsQueryable();
        }

        public async Task DeleteByEmployeeIdAsync(string employeeId)
        {
            var employeeCafes = _context.EmployeeCafes.Where(ec => ec.EmployeeId == employeeId);
            _context.EmployeeCafes.RemoveRange(employeeCafes);
            await _context.SaveChangesAsync();
        }
    }
}
