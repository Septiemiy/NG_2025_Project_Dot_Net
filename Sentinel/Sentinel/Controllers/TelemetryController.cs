using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SentinelBLL.Models;
using SentinelBLL.Service.Interface;
using SentinelBLL.SignalRService;

namespace Sentinel.Controllers;

[Route("api/telemetry")]
[ApiController]
public class TelemetryController : ControllerBase
{
    private readonly ITelemetryService _telemetryService;
    private readonly IHubContext<SignalRHub> _hubContext;

    public TelemetryController(ITelemetryService telemetryService, IHubContext<SignalRHub> hubContext)
    {
        _telemetryService = telemetryService;
        _hubContext = hubContext;
    }

    [HttpPost("get-data")]
    public async Task<IActionResult> GetTelemetryData([FromBody] TelemetryDTO telemetryDTO)
    {
        var telemetryData = await _telemetryService.GetTelemetryDataAsync(telemetryDTO);
        
        await _hubContext.Clients.All.SendAsync("ReceiveTelemetryData", telemetryData);

        return Ok(telemetryData);
    }
}
