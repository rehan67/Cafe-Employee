using Cafe_Employee.Data.Models;

namespace Cafe_Employee.Data_Layer.CafeDL
{
    /// <summary>
    /// Interface for Cafe repository operations.
    /// </summary>
    public interface ICafeRepository
    {
        /// <summary>
        /// Retrieves all cafes.
        /// </summary>
        /// <returns>A task representing an enumerable collection of Cafe objects.</returns>
        Task<IEnumerable<Cafe>> GetCafes();

        /// <summary>
        /// Retrieves cafes filtered by location.
        /// </summary>
        /// <param name="location">The location to filter cafes by.</param>
        /// <returns>A task representing an enumerable collection of Cafe objects.</returns>
        Task<IEnumerable<Cafe>> GetCafesByLocation(string location);

        /// <summary>
        /// Retrieves a cafe by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the cafe.</param>
        /// <returns>A task representing a Cafe object.</returns>
        Task<Cafe> GetCafeById(Guid id);

        /// <summary>
        /// Adds a new cafe.
        /// </summary>
        /// <param name="cafe">The cafe to be added.</param>
        Task AddCafe(Cafe cafe);

        /// <summary>
        /// Updates an existing cafe.
        /// </summary>
        /// <param name="cafe">The cafe with updated information.</param>
        Task UpdateCafe(Cafe cafe);

        /// <summary>
        /// Deletes a cafe by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the cafe to be deleted.</param>
        Task DeleteCafe(Guid id);
    }
}
