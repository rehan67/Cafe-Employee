namespace Cafe_Employee.Data.Dto.EmployeeDtos
{
    public class UpdateEmployeeDto
    {
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; } // Male or Female
        public Guid CafeId { get; set; }   // ID of the cafe to which the employee will be reassigned
        //public DateTime StartDate { get; set; } // Start date for the employee in the cafe
    }
}
