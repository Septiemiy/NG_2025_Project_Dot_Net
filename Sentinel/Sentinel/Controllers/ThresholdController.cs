using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SentinelBLL.Models;
using SentinelBLL.Service.Interface;

namespace Sentinel.Controllers;

[Route("api/threshold")]
[ApiController]
[Authorize(Roles = "Admin")]
public class ThresholdController : ControllerBase
{
    private readonly IThresholdService _thresholdService;

    public ThresholdController(IThresholdService thresholdService)
    {
        _thresholdService = thresholdService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("createThreshold")]
    public async Task<IActionResult> CreateThreshold([FromBody] ThresholdDTO thresholdDTO)
    {
        var createdThreshold = await _thresholdService.CreateThresholdAsync(thresholdDTO);

        if (createdThreshold == null)
        {
            return BadRequest(new { message = "Failed to create threshold. Maybe already exists" });
        }

        return Ok(createdThreshold);
    }
}
