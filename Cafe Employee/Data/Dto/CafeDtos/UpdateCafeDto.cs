﻿namespace Cafe_Employee.Data.Dto.CafeDtos
{
    /// <summary>
    /// Data Transfer Object (DTO) for updating an existing cafe.
    /// </summary>
    public class UpdateCafeDto
    {
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
    }
}
