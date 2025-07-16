using Microsoft.AspNetCore.Authorization;
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

    [HttpPost("registerDevice")]
    public async Task<IActionResult> RegisterdeviceAsync([FromBody] DeviceDTO deviceDTO)
    {
        var result = await _deviceService.RegisterDeviceAsync(deviceDTO);

        return Ok(result);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllDevicesAsync()
    {
        var devices = await _deviceService.GetAllDevicesAsync();
        
        if (devices == null)
        {
            return NotFound(new { message = "No devices found" });
        }
        
        return Ok(devices);
    }

    [HttpGet("get/{deviceId}")]
    public async Task<IActionResult> GetDeviceByIdAsync(Guid deviceId)
    {
        var device = await _deviceService.GetDeviceByIdAsync(deviceId);

        if (device == null)
        {
            return NotFound(new { message = "Device not found" });
        }

        return Ok(device);
    }
}
