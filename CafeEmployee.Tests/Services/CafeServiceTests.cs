using Cafe_Employee.Business_Layer.CafeBL;
using Cafe_Employee.Data.Dto.CafeDtos;
using Cafe_Employee.Data.Models;
using Cafe_Employee.Data_Layer.CafeDL;
using Moq;
namespace CafeEmployee.Tests.Services;
public class CafeServiceTests
{
    private readonly Mock<ICafeRepository> _cafeRepoMock;
    private readonly CafeService _cafeService;

    public CafeServiceTests()
    {
        _cafeRepoMock = new Mock<ICafeRepository>();
        _cafeService = new CafeService(_cafeRepoMock.Object);
    }

    [Fact]
    public async Task GetCafes_ReturnsCafeDtos()
    {
        // Arrange
        var cafes = new List<Cafe>
        {
            new Cafe { Id = Guid.NewGuid(), Name = "Cafe 1", Description = "Description 1", Location = "Location 1", EmployeeCafes = new List<EmployeeCafe>() },
            new Cafe { Id = Guid.NewGuid(), Name = "Cafe 2", Description = "Description 2", Location = "Location 2", EmployeeCafes = new List<EmployeeCafe>() }
        };

        _cafeRepoMock.Setup(repo => repo.GetCafes()).ReturnsAsync(cafes);

        // Act
        var result = await _cafeService.GetCafes();

        // Assert
        Assert.Equal(2, result.Count());
        Assert.All(result, cafeDto => Assert.NotNull(cafeDto.Id));
    }

    [Fact]
    public async Task GetCafesByLocation_ReturnsFilteredCafeDtos()
    {
        // Arrange
        var location = "Location X";
        var cafes = new List<Cafe>
        {
            new Cafe { Id = Guid.NewGuid(), Name = "Cafe A", Description = "Description A", Location = location, EmployeeCafes = new List<EmployeeCafe>() },
            new Cafe { Id = Guid.NewGuid(), Name = "Cafe B", Description = "Description B", Location = "Location Y", EmployeeCafes = new List<EmployeeCafe>() }
        };

        _cafeRepoMock.Setup(repo => repo.GetCafesByLocation(location)).ReturnsAsync(cafes.Where(c => c.Location == location));

        // Act
        var result = await _cafeService.GetCafesByLocation(location);

        // Assert
        Assert.Single(result);
        Assert.Equal("Cafe A", result.First().Name);
    }

    [Fact]
    public async Task GetCafesById_ReturnsCafeDto()
    {
        // Arrange
        var cafeId = Guid.NewGuid();
        var cafe = new Cafe { Id = cafeId, Name = "Cafe 3", Description = "Description 3", Location = "Location 3", EmployeeCafes = new List<EmployeeCafe>() };

        _cafeRepoMock.Setup(repo => repo.GetCafeById(cafeId)).ReturnsAsync(cafe);

        // Act
        var result = await _cafeService.GetCafeById(cafeId);

        // Assert
        Assert.Equal(cafeId, result.Id);
        Assert.Equal("Cafe 3", result.Name);
    }

    [Fact]
    public async Task AddCafe_CallsRepositoryAddCafe()
    {
        // Arrange
        var cafeDto = new CreateCafeDto { Name = "Cafe 4", Description = "Description 4", Location = "Location 4" };

        // Act
        await _cafeService.AddCafe(cafeDto);

        // Assert
        _cafeRepoMock.Verify(repo => repo.AddCafe(It.IsAny<Cafe>()), Times.Once);
    }

    [Fact]
    public async Task UpdateCafe_CallsRepositoryUpdateCafe()
    {
        // Arrange
        var cafeId = Guid.NewGuid();
        var updateDto = new UpdateCafeDto { Name = "Updated Cafe", Description = "Updated Description", Location = "Updated Location" };
        var cafe = new Cafe { Id = cafeId, Name = "Old Cafe", Description = "Old Description", Location = "Old Location" };

        _cafeRepoMock.Setup(repo => repo.GetCafeById(cafeId)).ReturnsAsync(cafe);

        // Act
        await _cafeService.UpdateCafe(cafeId, updateDto);

        // Assert
        _cafeRepoMock.Verify(repo => repo.UpdateCafe(It.Is<Cafe>(c => c.Id == cafeId && c.Name == "Updated Cafe")), Times.Once);
    }

    [Fact]
    public async Task DeleteCafe_CallsRepositoryDeleteCafe()
    {
        // Arrange
        var cafeId = Guid.NewGuid();

        // Act
        await _cafeService.DeleteCafe(cafeId);

        // Assert
        _cafeRepoMock.Verify(repo => repo.DeleteCafe(cafeId), Times.Once);
    }
}
