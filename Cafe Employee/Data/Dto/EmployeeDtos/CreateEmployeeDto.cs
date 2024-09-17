namespace Cafe_Employee.Data.Dto.EmployeeDtos
{
    public class CreateEmployeeDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public Guid CafeId { get; set; } // The cafe the employee is assigned to
    }
}
