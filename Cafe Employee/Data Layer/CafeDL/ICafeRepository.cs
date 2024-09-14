using Cafe_Employee.Data.Models;

namespace Cafe_Employee.Data_Layer.CafeDL
{
    public interface ICafeRepository
    {
        Task<IEnumerable<Cafe>> GetCafes();

        Task<IEnumerable<Cafe>> GetCafesByLocation(string location);
        Task<Cafe> GetCafeById(Guid id);
        Task AddCafe(Cafe cafe);
        Task UpdateCafe(Cafe cafe);
        Task DeleteCafe(Guid id);
        
    }
}
