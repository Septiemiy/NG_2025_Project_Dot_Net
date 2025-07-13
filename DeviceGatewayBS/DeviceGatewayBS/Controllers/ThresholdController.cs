using DeviceGatewayBLL.Models;
using DeviceGatewayBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeviceGatewayBS.Controllers;

[Route("api/threshold")]
[ApiController]
public class ThresholdController : Controller
{
    private readonly IThresholdService _thresholdService;

    public ThresholdController(IThresholdService thresholdService)
    {
        _thresholdService = thresholdService;
    }

    [HttpPost("createThreshold")]
    public async Task<IActionResult> CreateThresholdAsync([FromBody] ThresholdDTO thresholdDTO)
    {
        if(thresholdDTO == null)
        {
            return BadRequest("Threshold data is null.");
        }

        var thresholdId = await _thresholdService.AddThresholdAsync(thresholdDTO);

        if (thresholdId == Guid.Empty)
        {
            return StatusCode(500, "An error occurred while saving the threshold.");
        }

        return Ok(thresholdId);
    }
}
