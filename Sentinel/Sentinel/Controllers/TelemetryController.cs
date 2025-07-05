using Microsoft.AspNetCore.Mvc;
using SentinelBLL.Models;
using SentinelBLL.Service.Interface;

namespace Sentinel.Controllers;

[Route("api/telemetry")]
[ApiController]
public class TelemetryController : ControllerBase
{
    private readonly ITelemetryService _telemetryService;

    public TelemetryController(ITelemetryService telemetryService)
    {
        _telemetryService = telemetryService;
    }

    [HttpPost("get-data")]
    public async Task<IActionResult> GetTelemetryData([FromBody] TelemetryDTO telemetryDTO)
    {
        var telemetryData = await _telemetryService.GetTelemetryDataAsync(telemetryDTO);
        
        return Ok(telemetryData);
    }
}
