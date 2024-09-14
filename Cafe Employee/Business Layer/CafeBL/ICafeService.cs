using Cafe_Employee.Data.Dto.CafeDtos;

namespace Cafe_Employee.Business_Layer.CafeBL
{
    public interface ICafeService
    {
        Task<IEnumerable<CafeDto>> GetCafes();

        Task<CafeDto> GetCafesById(Guid id);

        Task<IEnumerable<CafeDto>> GetCafesByLocation(string location);

        Task AddCafe(CreateCafeDto cafeDto);
        Task UpdateCafe(Guid id, UpdateCafeDto cafeDto);
        Task DeleteCafe(Guid id);
    }
}
