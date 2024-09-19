using Cafe_Employee.Business_Layer.CafeBL;
using Cafe_Employee.Data.Dto.CafeDtos;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class CafesController : ControllerBase
{
    private readonly ICafeService _cafeService;

    public CafesController(ICafeService cafeService)
    {
        _cafeService = cafeService;
    }

    // Get All Cafe
    [HttpGet]
    public async Task<IActionResult> GetCafes()
    {
        var cafes = await _cafeService.GetCafes();
        return Ok(cafes);
    }

    // Get Cafe by Id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCafeById(Guid id)
    {
        var cafes = await _cafeService.GetCafeById(id);
        return Ok(cafes);
    }

    // Get Cafe by Location
    [HttpGet("by-location")]
    public async Task<IActionResult> GetCafesByLocation([FromQuery] string location)
    {
        var cafes = await _cafeService.GetCafesByLocation(location);
        return Ok(cafes);
    }

    // Add Cafe
    [HttpPost("add-cafes")]
    public async Task<IActionResult> AddCafe([FromBody] CreateCafeDto cafeDto)
    {
        try
        {
            await _cafeService.AddCafe(cafeDto);
            return CreatedAtAction(nameof(GetCafes), new { location = cafeDto.Location }, cafeDto);
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            // _logger.LogError(ex, "An error occurred while adding a cafe.");

            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    // Update Cafe
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCafe(Guid id, [FromBody] UpdateCafeDto cafeDto)
    {
        await _cafeService.UpdateCafe(id, cafeDto);
        return NoContent();
    }

    // Delete Cafe
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCafe(Guid id)
    {
        await _cafeService.DeleteCafe(id);
        return NoContent();
    }
}