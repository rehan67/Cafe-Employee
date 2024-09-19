using Cafe_Employee.Data;
using Cafe_Employee.Data.Models;
using Cafe_Employee.Data_Layer.CafeDL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CafeEmployee.Tests.Repositories
{
    public class CafeRepositoryTests
    {
        private readonly ApplicationDbContext _context;
        private readonly CafeRepository _cafeRepository;

        public CafeRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestCafeDb")
                .Options;

            _context = new ApplicationDbContext(options);
            _cafeRepository = new CafeRepository(_context);

            // Seed data for each test
            SeedDatabase();
        }

        private void SeedDatabase()
        {
            // Clear the database before seeding
            _context.Cafes.RemoveRange(_context.Cafes);
            _context.SaveChanges();

            // Seed data
            _context.Cafes.AddRange(
                new Cafe { Id = Guid.NewGuid(), Name = "Cafe 1", Description = "Description 1", Location = "Location 1" },
                new Cafe { Id = Guid.NewGuid(), Name = "Cafe 2", Description = "Description 2", Location = "Location 2" }
            );
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetCafes_ReturnsCafesList()
        {
            // Act
            var result = await _cafeRepository.GetCafes();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetCafeById_ReturnsCafe()
        {
            // Arrange
            var cafe = await _context.Cafes.FirstOrDefaultAsync();
            if (cafe == null)
                throw new InvalidOperationException("No cafes found in the database.");

            // Act
            var result = await _cafeRepository.GetCafeById(cafe.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cafe.Name, result.Name);
        }

        [Fact]
        public async Task AddCafe_AddsCafeToDatabase()
        {
            // Arrange
            var cafe = new Cafe { Id = Guid.NewGuid(), Name = "Cafe 3", Description = "Description 3", Location = "Location 3" };

            // Act
            await _cafeRepository.AddCafe(cafe);

            // Assert
            var result = await _context.Cafes.FindAsync(cafe.Id);
            Assert.NotNull(result);
            Assert.Equal(cafe.Name, result.Name);
        }

        [Fact]
        public async Task UpdateCafe_UpdatesCafeInDatabase()
        {
            // Arrange
            var cafe = await _context.Cafes.FirstOrDefaultAsync();
            if (cafe == null)
                throw new InvalidOperationException("No cafes found in the database.");

            cafe.Description = "Updated Description";

            // Act
            await _cafeRepository.UpdateCafe(cafe);

            // Assert
            var result = await _context.Cafes.FindAsync(cafe.Id);
            Assert.Equal("Updated Description", result.Description);
        }

        [Fact]
        public async Task DeleteCafe_DeletesCafeFromDatabase()
        {
            // Arrange
            var cafe = await _context.Cafes.FirstOrDefaultAsync();
            if (cafe == null)
                throw new InvalidOperationException("No cafes found in the database.");

            // Act
            await _cafeRepository.DeleteCafe(cafe.Id);

            // Assert
            var result = await _context.Cafes.FindAsync(cafe.Id);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetCafesByLocation_ReturnsFilteredCafes()
        {
            // Arrange
            var cafe = new Cafe { Id = Guid.NewGuid(), Name = "Cafe A", Description = "Description A", Location = "Location X" };
            _context.Cafes.Add(cafe);
            await _context.SaveChangesAsync();

            // Act
            var result = await _cafeRepository.GetCafesByLocation("Location X");

            // Assert
            Assert.Single(result);
            Assert.Equal("Cafe A", result.First().Name);
        }
    }
}
