using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NeoTestController : ControllerBase
{
    private readonly INeoService _neoService;

    public NeoTestController(INeoService neoService)
    {
        _neoService = neoService;
    }

    [HttpGet("raw")]
    public async Task<IActionResult> GetRawData(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate
    )
    {
        var start = startDate ?? DateTime.UtcNow;
        var end = endDate ?? start.AddDays(3); // Keep range under NASA's 7-day limit

        var result = await _neoService.GetRawFeedAsync(start, end);
        if (result == null)
            return NotFound("Unable to retrieve NASA data.");

        return Ok(result);
    }
}
