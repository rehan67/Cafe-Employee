using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Employee.Data.Models
{
    public class EmployeeCafe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Ensures EF treats this as an identity column
        public int Id { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public Guid CafeId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public Employee Employee { get; set; }
        public Cafe Cafe { get; set; }
    }
}
