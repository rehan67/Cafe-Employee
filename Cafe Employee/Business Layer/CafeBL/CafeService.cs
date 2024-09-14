
using Cafe_Employee.Data.Dto.CafeDtos;
using Cafe_Employee.Data.Models;
using Cafe_Employee.Data_Layer.CafeDL;


namespace Cafe_Employee.Business_Layer.CafeBL
{
    public class CafeService : ICafeService
    {

        private readonly ICafeRepository _cafeRepo;

        public CafeService(ICafeRepository cafeRepo)
        {
            _cafeRepo = cafeRepo;
        }

        // Get All Cafe
        public async Task<IEnumerable<CafeDto>> GetCafes()
        {
            var cafes = await _cafeRepo.GetCafes();
            return cafes.Select(c => new CafeDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Location = c.Location,
                Employees = c.EmployeeCafes.Count
            }).ToList();
        }

        // Add Cafe
        public async Task AddCafe(CreateCafeDto cafeDto)
        {
            var cafe = new Cafe
            {
                Id = Guid.NewGuid(),
                Name = cafeDto.Name,
                Description = cafeDto.Description,
                Location = cafeDto.Location,
            };

            await _cafeRepo.AddCafe(cafe);
        }

        // Update Cafe
        public async Task UpdateCafe(Guid id, UpdateCafeDto cafeDto)
        {
            var cafe = await _cafeRepo.GetCafeById(id);
            if (cafe != null)
            {
                cafe.Name = cafeDto.Name;
                cafe.Description = cafeDto.Description;
                cafe.Location = cafeDto.Location;

                await _cafeRepo.UpdateCafe(cafe);
            }
        }

        public async Task DeleteCafe(Guid id)
        {
            await _cafeRepo.DeleteCafe(id);
        }

        // Get Cafe by location
        public async Task<IEnumerable<CafeDto>> GetCafesByLocation(string location)
        {
            var cafes = await _cafeRepo.GetCafesByLocation(location);
            return cafes.Select(c => new CafeDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Location = c.Location,
                Employees = c.EmployeeCafes.Count
            }).ToList();
        }

        // Get Cafe by Id
        public async Task<CafeDto> GetCafesById(Guid id)
        {
            var cafes = await _cafeRepo.GetCafeById(id);
            return new CafeDto
            {
                Id = cafes.Id,
                Name = cafes.Name,
                Description = cafes.Description,
                Location = cafes.Location,
                Employees = cafes.EmployeeCafes.Count
            };
        }
    }

}
