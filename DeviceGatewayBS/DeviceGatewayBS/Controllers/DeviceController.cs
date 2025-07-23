using DeviceGatewayBLL.Models;
using DeviceGatewayBLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeviceGatewayBS.Controllers;

[Route("api/device")]
[ApiController]
public class DeviceController : Controller
{
    private readonly IDeviceService _deviceService;
    private readonly ILogger<DeviceController> _logger;

    public DeviceController(IDeviceService deviceService, ILogger<DeviceController> logger)
    {
        _deviceService = deviceService;
        _logger = logger;
    }

    [HttpPost("registerDevice")]
    public async Task<IActionResult> RegisterDevice([FromBody] DeviceDTO deviceDTO)
    {
        _logger.LogInformation("[INFO][DeviceGateway]Registering device with ID: {DeviceId}", deviceDTO.DeviceId);
        var response = await _deviceService.RegisterDeviceAsync(deviceDTO);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("[INFO][DeviceGateway]Device registered successfully with ID: {DeviceId}", deviceDTO.DeviceId);
            return Ok(new RegisterDeviceResultDTO
            {
                IsSuccess = true,
                DeviceId = deviceDTO.DeviceId,
                Message = "Device registered successfully"
            });
        }

        _logger.LogError("[ERROR][DeviceGateway]Failed to register device with ID: {DeviceId}", deviceDTO.DeviceId);
        return BadRequest(new RegisterDeviceResultDTO
        {
            IsSuccess = false,
            Message = "Failed to register device"
        });
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllDevices()
    {
        _logger.LogInformation("[INFO][DeviceGateway]Fetching all devices");
        var response = await _deviceService.GetDevicesAsync();
        
        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("[INFO][DeviceGateway]Successfully fetched devices");
            var devices = await response.Content.ReadFromJsonAsync<ICollection<DeviceDTO>>();
            return Ok(devices);
        }

        _logger.LogError("[ERROR][DeviceGateway]Failed to fetch devices");
        return BadRequest(new { Message = "Failed to get devices" });
    }

    [HttpGet("get/{deviceId}")]
    public async Task<IActionResult> GetDeviceById(Guid deviceId)
    {
        _logger.LogInformation("[INFO][DeviceGateway]Fetching device with ID: {DeviceId}", deviceId);
        var response = await _deviceService.GetDeviceByIdAsync(deviceId);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("[INFO][DeviceGateway]Successfully fetched device with ID: {DeviceId}", deviceId);
            var device = await response.Content.ReadFromJsonAsync<DeviceDTO>();
            return Ok(device);
        }

        _logger.LogError("[ERROR][DeviceGateway]Failed to fetch device with ID: {DeviceId}", deviceId);
        return NotFound(new { Message = "Device not found" });
    }
}
