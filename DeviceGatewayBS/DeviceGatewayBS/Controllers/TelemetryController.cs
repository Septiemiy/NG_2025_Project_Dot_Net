using DeviceGatewayBLL.Models;
using DeviceGatewayBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeviceGatewayBS.Controllers;

[Route("api/telemetry")]
[ApiController]
public class TelemetryController : Controller
{
    private readonly ITelemetryService _telemetryService;

    public TelemetryController(ITelemetryService telemetryService)
    {
        _telemetryService = telemetryService;
    }

    [HttpPost("get-data")]
    public async Task<IActionResult> SaveTelemetryDataAsync([FromBody] TelemetryDTO telemetryDTO)
    {
        if (telemetryDTO == null)
        {
            return BadRequest("Telemetry data is null.");
        }

        var telemetryGuid = await _telemetryService.AddTelemetryAsync(telemetryDTO);

        if (telemetryGuid == Guid.Empty)
        {
            return StatusCode(500, "An error occurred while saving telemetry data.");
        }

        return Ok(telemetryGuid);
    }
}
