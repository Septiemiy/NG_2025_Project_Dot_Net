using DeviceGatewayBLL.Models;
using DeviceGatewayBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeviceGatewayBS.Controllers;

[Route("api/telemetry")]
[ApiController]
public class TelemetryController : Controller
{
    private readonly ITelemetryService _telemetryService;
    private readonly ILogger<TelemetryController> _logger;

    public TelemetryController(ITelemetryService telemetryService, ILogger<TelemetryController> logger)
    {
        _telemetryService = telemetryService;
        _logger = logger;
    }

    [HttpPost("get-data")]
    public async Task<IActionResult> SaveTelemetryDataAsync([FromBody] TelemetryDTO telemetryDTO)
    {
        if (telemetryDTO == null)
        {
            _logger.LogError("[ERROR][DeviceGateway]Telemetry data is null.");
            return BadRequest("Telemetry data is null.");
        }

        _logger.LogInformation("[INFO][DeviceGateway]Saving telemetry data for device ID: {DeviceId}", telemetryDTO.DeviceId);
        var telemetryGuid = await _telemetryService.AddTelemetryAsync(telemetryDTO);

        if (telemetryGuid == Guid.Empty)
        {
            _logger.LogError("[ERROR][DeviceGateway]An error occurred while saving telemetry data for device ID: {DeviceId}", telemetryDTO.DeviceId);
            return StatusCode(500, "An error occurred while saving telemetry data.");
        }

        _logger.LogInformation("[INFO][DeviceGateway]Telemetry data saved successfully for device ID: {DeviceId} with Telemetry ID: {TelemetryId}", telemetryDTO.DeviceId, telemetryGuid);
        return Ok(telemetryGuid);
    }
}
