using Microsoft.AspNetCore.Mvc;
using SentinelBLL.Models;
using SentinelBLL.Service.Interface;

namespace Sentinel.Controllers;

[Route("api/device")]
[ApiController]
public class DeviceController : ControllerBase
{
    private readonly IDeviceService _deviceService;

    public DeviceController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterdeviceAsync([FromBody] DeviceDTO deviceDTO)
    {
        var deviceData = await _deviceService.RegisterDeviceAsync(deviceDTO);

        return Ok(deviceData);
    }
}
