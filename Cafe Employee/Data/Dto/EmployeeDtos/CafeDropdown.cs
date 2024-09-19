namespace Cafe_Employee.Data.Dto.EmployeeDtos
{
    /// <summary>
    /// Data Transfer Object (DTO) for representing a cafe in a dropdown list.
    /// </summary>
    public class CafeDropdown
    {
        /// <summary>
        /// Gets or sets the unique identifier of the cafe.
        /// </summary>
        public Guid CafeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the cafe.
        /// </summary>
        public string Cafe { get; set; }
    }
}
