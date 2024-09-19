namespace Cafe_Employee.Data.Dto.EmployeeDtos
{
    /// <summary>
    /// Data Transfer Object (DTO) for representing an employee.
    /// </summary>
    public class EmployeeDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the employee.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the employee.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the email address of the employee.
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the employee.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the gender of the employee.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the number of days the employee has worked.
        /// </summary>
        public int DaysWorked { get; set; }

        /// <summary>
        /// Gets or sets the cafe to which the employee is assigned.
        /// </summary>
        public CafeDropdown Cafe { get; set; }
    }
}
