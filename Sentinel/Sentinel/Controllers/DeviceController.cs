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
    private readonly ILogger<DeviceController> _logger;

    public DeviceController(IDeviceService deviceService, ILogger<DeviceController> logger)
    {
        _deviceService = deviceService;
        _logger = logger;
    }

    [HttpPost("registerDevice")]
    public async Task<IActionResult> RegisterdeviceAsync([FromBody] DeviceDTO deviceDTO)
    {
        _logger.LogInformation("[INFO]: Registering device with ID: {DeviceId}", deviceDTO.DeviceId);
        var result = await _deviceService.RegisterDeviceAsync(deviceDTO);

        _logger.LogInformation("[INFO]: Device registered successfully: {DeviceId}", deviceDTO.DeviceId);
        return Ok(result);
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllDevicesAsync()
    {
        _logger.LogInformation("[INFO]: Retrieving all devices");
        var devices = await _deviceService.GetAllDevicesAsync();
        
        if (devices == null)
        {
            _logger.LogWarning("[WARNING]: No devices found");
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
