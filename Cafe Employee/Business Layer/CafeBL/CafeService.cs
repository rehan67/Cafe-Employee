using Cafe_Employee.Data.Dto.CafeDtos;
using Cafe_Employee.Data.Models;
using Cafe_Employee.Data_Layer.CafeDL;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Cafe_Employee.Business_Layer.CafeBL
{
    public class CafeService : ICafeService
    {
        private readonly ICafeRepository _cafeRepository;

        public CafeService(ICafeRepository cafeRepository)
        {
            _cafeRepository = cafeRepository;
        }

        // Get all cafes
        public async Task<IEnumerable<CafeDto>> GetCafes()
        {
            var cafes = await _cafeRepository.GetCafes();
            return cafes.Select(MapToCafeDto).ToList();
        }

        // Add a new cafe
        public async Task AddCafe(CreateCafeDto cafeDto)
        {
            var cafe = new Cafe
            {
                Id = Guid.NewGuid(),
                Name = cafeDto.Name,
                Description = cafeDto.Description,
                Location = cafeDto.Location
            };

            await _cafeRepository.AddCafe(cafe);
        }

        // Update existing cafe
        public async Task UpdateCafe(Guid id, UpdateCafeDto cafeDto)
        {
            var cafe = await _cafeRepository.GetCafeById(id);
            if (cafe == null) return;

            cafe.Name = cafeDto.Name;
            cafe.Description = cafeDto.Description;
            cafe.Location = cafeDto.Location;

            await _cafeRepository.UpdateCafe(cafe);
        }

        // Delete cafe by id
        public async Task DeleteCafe(Guid id)
        {
            await _cafeRepository.DeleteCafe(id);
        }

        // Get cafes by location
        public async Task<IEnumerable<CafeDto>> GetCafesByLocation(string location)
        {
            var cafes = await _cafeRepository.GetCafesByLocation(location);
            return cafes.Select(MapToCafeDto).ToList();
        }

        // Get cafe by id
        public async Task<CafeDto> GetCafeById(Guid id)
        {
            var cafe = await _cafeRepository.GetCafeById(id);
            return cafe != null ? MapToCafeDto(cafe) : null;
        }

        // Private helper method to map Cafe to CafeDto
        private static CafeDto MapToCafeDto(Cafe cafe)
        {
            return new CafeDto
            {
                Id = cafe.Id,
                Name = cafe.Name,
                Description = cafe.Description,
                Location = cafe.Location,
                Employees = cafe.EmployeeCafes.Count
            };
        }
    }
}
