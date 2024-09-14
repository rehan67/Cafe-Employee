using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Cafe_Employee.Data.Models
{
    public class Cafe
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

        [AllowNull]
        public string? Logo { get; set; } // Optional

        [Required]
        [MaxLength(100)]
        public string Location { get; set; }

        public ICollection<EmployeeCafe> EmployeeCafes { get; set; }
    }
}
