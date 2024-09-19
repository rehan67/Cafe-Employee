using Cafe_Employee.Data;
using Cafe_Employee.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe_Employee.Data_Layer.CafeDL
{
    public class CafeRepository : ICafeRepository
    {
        private readonly ApplicationDbContext _context;

        public CafeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all cafes, ordered by the number of employee-cafe relationships in descending order.
        /// </summary>
        /// <returns>A task representing an enumerable collection of Cafe objects.</returns>
        public async Task<IEnumerable<Cafe>> GetCafes()
        {
            return await _context.Cafes
                .Include(c => c.EmployeeCafes)
                .OrderByDescending(c => c.EmployeeCafes.Count)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a cafe by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the cafe.</param>
        /// <returns>The Cafe object if found; otherwise, null.</returns>
        public async Task<Cafe> GetCafeById(Guid id)
        {
            return await _context.Cafes
                .Include(c => c.EmployeeCafes)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Adds a new cafe to the database.
        /// </summary>
        /// <param name="cafe">The cafe to be added.</param>
        public async Task AddCafe(Cafe cafe)
        {
            _context.Cafes.Add(cafe);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing cafe in the database.
        /// </summary>
        /// <param name="cafe">The cafe with updated information.</param>
        public async Task UpdateCafe(Cafe cafe)
        {
            _context.Cafes.Update(cafe);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a cafe by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the cafe to be deleted.</param>
        public async Task DeleteCafe(Guid id)
        {
            // Find the cafe along with its related employee-cafe relationships
            var cafe = await _context.Cafes
                .Include(c => c.EmployeeCafes)
                .ThenInclude(ec => ec.Employee)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cafe != null)
            {
                // Remove employee-cafe relationships
                _context.EmployeeCafes.RemoveRange(cafe.EmployeeCafes);

                // Remove the cafe itself
                _context.Cafes.Remove(cafe);

                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Cafe not found", nameof(id));
            }
        }

        /// <summary>
        /// Retrieves cafes filtered by location, ordered by the number of employee-cafe relationships in descending order.
        /// </summary>
        /// <param name="location">The location to filter cafes by.</param>
        /// <returns>A task representing an enumerable collection of Cafe objects.</returns>
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
