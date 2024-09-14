using System.ComponentModel.DataAnnotations;

namespace Cafe_Employee.Data.Models
{
    public class Employee
    {
        [Key]
        [Required]
        [RegularExpression(@"^UI[0-9A-Za-z]{7}$")]
        public string Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        [RegularExpression(@"^[89]\d{7}$")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Gender { get; set; }

        public ICollection<EmployeeCafe> EmployeeCafes { get; set; }
    }
}
