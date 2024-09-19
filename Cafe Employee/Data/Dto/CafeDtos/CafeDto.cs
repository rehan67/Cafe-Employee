namespace Cafe_Employee.Data.Dto.CafeDtos
{
    /// <summary>
    /// Data Transfer Object (DTO) for representing a cafe.
    /// </summary>
    public class CafeDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the cafe.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the cafe.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the cafe.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the location of the cafe.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the number of employees associated with the cafe.
        /// </summary>
        public int Employees { get; set; }

        /// <summary>
        /// Gets or sets the logo URL or path for the cafe.
        /// </summary>
        public string Logo { get; set; }
    }
}
