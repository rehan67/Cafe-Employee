namespace Cafe_Employee.Data.Dto.EmployeeDtos
{
    /// <summary>
    /// Data Transfer Object (DTO) for creating a new employee.
    /// </summary>
    public class CreateEmployeeDto
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
        /// Gets or sets the unique identifier of the cafe the employee is assigned to.
        /// </summary>
        public Guid CafeId { get; set; }
    }
}
