using Cafe_Employee.Business_Layer.CafeBL;
using Cafe_Employee.Data.Dto.CafeDtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
namespace CafeEmployee.Tests.Controllers;
public class CafesControllerTests
{
    private readonly Mock<ICafeService> _cafeServiceMock;
    private readonly CafesController _cafeController;

    public CafesControllerTests()
    {
        _cafeServiceMock = new Mock<ICafeService>();
        _cafeController = new CafesController(_cafeServiceMock.Object);
    }

    [Fact]
    public async Task GetCafes_ReturnsOkResult_WithListOfCafeDtos()
    {
        // Arrange
        var cafes = new List<CafeDto>
        {
            new CafeDto { Id = Guid.NewGuid(), Name = "Cafe 1", Description = "Description 1", Location = "Location 1", Employees = 10 },
            new CafeDto { Id = Guid.NewGuid(), Name = "Cafe 2", Description = "Description 2", Location = "Location 2", Employees = 5 }
        };

        _cafeServiceMock.Setup(service => service.GetCafes()).ReturnsAsync(cafes);

        // Act
        var result = await _cafeController.GetCafes() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        var resultValue = Assert.IsAssignableFrom<IEnumerable<CafeDto>>(result.Value);
        Assert.Equal(2, resultValue.Count());
    }

    [Fact]
    public async Task GetCafesById_ReturnsOkResult_WithCafeDto()
    {
        // Arrange
        var cafeId = Guid.NewGuid();
        var cafe = new CafeDto { Id = cafeId, Name = "Cafe 1", Description = "Description 1", Location = "Location 1", Employees = 10 };

        _cafeServiceMock.Setup(service => service.GetCafeById(cafeId)).ReturnsAsync(cafe);

        // Act
        var result = await _cafeController.GetCafeById(cafeId) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        var resultValue = Assert.IsAssignableFrom<CafeDto>(result.Value);
        Assert.Equal(cafeId, resultValue.Id);
    }

    [Fact]
    public async Task GetCafesByLocation_ReturnsOkResult_WithFilteredCafeDtos()
    {
        // Arrange
        var location = "Location X";
        var cafes = new List<CafeDto>
        {
            new CafeDto { Id = Guid.NewGuid(), Name = "Cafe A", Description = "Description A", Location = location, Employees = 8 }
        };

        _cafeServiceMock.Setup(service => service.GetCafesByLocation(location)).ReturnsAsync(cafes);

        // Act
        var result = await _cafeController.GetCafesByLocation(location) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
        var resultValue = Assert.IsAssignableFrom<IEnumerable<CafeDto>>(result.Value);
        Assert.Single(resultValue);
    }

    [Fact]
    public async Task AddCafe_ReturnsCreatedAtActionResult()
    {
        // Arrange
        var cafeDto = new CreateCafeDto { Name = "Cafe 3", Description = "Description 3", Location = "Location 3" };

        // Act
        var result = await _cafeController.AddCafe(cafeDto) as CreatedAtActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(201, result.StatusCode);
        Assert.Equal("GetCafes", result.ActionName);
        Assert.Equal(cafeDto, result.Value);
    }

    [Fact]
    public async Task UpdateCafe_ReturnsNoContentResult()
    {
        // Arrange
        var cafeId = Guid.NewGuid();
        var cafeDto = new UpdateCafeDto { Name = "Updated Cafe", Description = "Updated Description", Location = "Updated Location" };

        // Act
        var result = await _cafeController.UpdateCafe(cafeId, cafeDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteCafe_ReturnsNoContentResult()
    {
        // Arrange
        var cafeId = Guid.NewGuid();

        // Act
        var result = await _cafeController.DeleteCafe(cafeId);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
