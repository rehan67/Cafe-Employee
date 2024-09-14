using Cafe_Employee.Data;
using Cafe_Employee.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Employee.Data_Layer.CafeDL
{
    public class CafeRepository : ICafeRepository
    {
        private readonly ApplicationDbContext _context;

        public CafeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cafe>> GetCafes()
        {
            return await _context.Cafes
                .Include(c => c.EmployeeCafes)
                .OrderByDescending(c => c.EmployeeCafes.Count)
                .ToListAsync();
        }

        public async Task<Cafe> GetCafeById(Guid id)
        {
            return await _context.Cafes.Include(c => c.EmployeeCafes)
                .FirstOrDefaultAsync(e => e.Id == id)!;
        }

        public async Task AddCafe(Cafe cafe)
        {
            _context.Cafes.Add(cafe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCafe(Cafe cafe)
        {
            _context.Cafes.Update(cafe);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCafe(Guid id)
        {
            // Find the cafe along with its related employee-cafe relationships
            var cafe = await _context.Cafes
                .Include(c => c.EmployeeCafes)
                .ThenInclude(relation => relation.Employee)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cafe != null)
            {
                // Remove employee-cafe relations, not the employees themselves
                _context.EmployeeCafes.RemoveRange(cafe.EmployeeCafes);

                // Remove the cafe itself
                _context.Cafes.Remove(cafe);

                // Save the changes to the database
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Cafe>> GetCafesByLocation(string location)
        {
            return await _context.Cafes
               .Include(c => c.EmployeeCafes)
               .Where(c => string.IsNullOrEmpty(location) || c.Location == location)
               .OrderByDescending(c => c.EmployeeCafes.Count)
               .ToListAsync();
        }
    }
}
