using Cafe_Employee.Data.Dto.CafeDtos;

namespace Cafe_Employee.Business_Layer.CafeBL
{
    public interface ICafeService
    {
        /// <summary>
        /// Retrieves a collection of all cafes.
        /// </summary>
        /// <returns>A task representing an enumerable collection of CafeDto.</returns>
        Task<IEnumerable<CafeDto>> GetCafes();

        /// <summary>
        /// Retrieves a specific cafe by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the cafe.</param>
        /// <returns>A task representing a CafeDto object.</returns>
        Task<CafeDto> GetCafeById(Guid id);

        /// <summary>
        /// Retrieves a collection of cafes based on their location.
        /// </summary>
        /// <param name="location">The location of the cafes to retrieve.</param>
        /// <returns>A task representing an enumerable collection of CafeDto.</returns>
        Task<IEnumerable<CafeDto>> GetCafesByLocation(string location);

        /// <summary>
        /// Adds a new cafe.
        /// </summary>
        /// <param name="cafeDto">The DTO containing details of the cafe to be added.</param>
        Task AddCafe(CreateCafeDto cafeDto);

        /// <summary>
        /// Updates an existing cafe's details.
        /// </summary>
        /// <param name="id">The unique identifier of the cafe to update.</param>
        /// <param name="cafeDto">The DTO containing the updated cafe details.</param>
        Task UpdateCafe(Guid id, UpdateCafeDto cafeDto);

        /// <summary>
        /// Deletes a cafe by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the cafe to delete.</param>
        Task DeleteCafe(Guid id);
    }

}
