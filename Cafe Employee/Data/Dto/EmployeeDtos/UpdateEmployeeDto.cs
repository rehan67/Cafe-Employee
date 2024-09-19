namespace Cafe_Employee.Data.Dto.EmployeeDtos
{
    /// <summary>
    /// Data Transfer Object (DTO) for updating an existing employee.
    /// </summary>
    public class UpdateEmployeeDto
    {
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
        /// Gets or sets the gender of the employee. Possible values: Male or Female.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the cafe to which the employee will be reassigned.
        /// </summary>
        public Guid CafeId { get; set; }

        // Optionally, you can include additional properties if needed for the update
        //public DateTime StartDate { get; set; } // Start date for the employee in the cafe
    }
}
