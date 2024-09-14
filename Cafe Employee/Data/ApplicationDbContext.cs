using Cafe_Employee.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Cafe_Employee.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Cafe> Cafes { get; set; }
        public DbSet<EmployeeCafe> EmployeeCafes { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeCafe>()
                .HasIndex(ec => ec.EmployeeId)
                .IsUnique();

            modelBuilder.Entity<EmployeeCafe>()
                .HasOne(ec => ec.Employee)
                .WithMany(e => e.EmployeeCafes)
                .HasForeignKey(ec => ec.EmployeeId);

            modelBuilder.Entity<EmployeeCafe>()
                .HasOne(ec => ec.Cafe)
                .WithMany(c => c.EmployeeCafes)
                .HasForeignKey(ec => ec.CafeId);

            // Seed data
            var cafeMochaId = Guid.NewGuid();
            var cafeLatteId = Guid.NewGuid();

            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = "UI0000001", Name = "John Doe", EmailAddress = "john.doe@example.com", PhoneNumber = "91234567", Gender = "Male" },
                new Employee { Id = "UI0000002", Name = "Jane Smith", EmailAddress = "jane.smith@example.com", PhoneNumber = "81234567", Gender = "Female" }
            );

            modelBuilder.Entity<Cafe>().HasData(
                new Cafe { Id = cafeMochaId, Name = "Cafe Mocha", Description = "A cozy place for coffee lovers", Logo="", Location = "Downtown" },
                new Cafe { Id = cafeLatteId, Name = "Cafe Latte", Description = "Best lattes in town", Logo = "", Location = "Uptown" }
            );

            modelBuilder.Entity<EmployeeCafe>().HasData(
                new EmployeeCafe { Id = 1, EmployeeId = "UI0000001", CafeId = cafeMochaId, StartDate = new DateTime(2023, 1, 1) },
                new EmployeeCafe { Id = 2, EmployeeId = "UI0000002", CafeId = cafeLatteId, StartDate = new DateTime(2023, 2, 1) }
            );
        }
    }
}
