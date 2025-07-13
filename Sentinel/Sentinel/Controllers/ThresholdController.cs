using Microsoft.AspNetCore.Mvc;
using SentinelBLL.Models;
using SentinelBLL.Service.Interface;

namespace Sentinel.Controllers;

[Route("api/threshold")]
[ApiController]
public class ThresholdController : ControllerBase
{
    private readonly IThresholdService _thresholdService;

    public ThresholdController(IThresholdService thresholdService)
    {
        _thresholdService = thresholdService;
    }

    [HttpPost("createThreshold")]
    public async Task<IActionResult> CreateThreshold([FromBody] ThresholdDTO thresholdDTO)
    {
        var createdThreshold = await _thresholdService.CreateThresholdAsync(thresholdDTO);

        return Ok(createdThreshold);
    }
}
