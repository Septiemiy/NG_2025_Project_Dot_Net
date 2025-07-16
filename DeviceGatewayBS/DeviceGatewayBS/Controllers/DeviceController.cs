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

    public DeviceController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpPost("registerDevice")]
    public async Task<IActionResult> RegisterDevice([FromBody] DeviceDTO deviceDTO)
    {
        var response = await _deviceService.RegisterDeviceAsync(deviceDTO);

        if (response.IsSuccessStatusCode)
        {
            return Ok(new RegisterDeviceResultDTO
            {
                IsSuccess = true,
                DeviceId = deviceDTO.DeviceId,
                Message = "Device registered successfully"
            });
        }

        return BadRequest(new RegisterDeviceResultDTO
        {
            IsSuccess = false,
            Message = "Failed to register device"
        });
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllDevices()
    {
        var response = await _deviceService.GetDevicesAsync();
        
        if (response.IsSuccessStatusCode)
        {
            var devices = await response.Content.ReadFromJsonAsync<ICollection<DeviceDTO>>();
            return Ok(devices);
        }
        
        return BadRequest(new { Message = "Failed to get devices" });
    }

    [HttpGet("get/{deviceId}")]
    public async Task<IActionResult> GetDeviceById(Guid deviceId)
    {
        var response = await _deviceService.GetDeviceByIdAsync(deviceId);

        if (response.IsSuccessStatusCode)
        {
            var device = await response.Content.ReadFromJsonAsync<DeviceDTO>();
            return Ok(device);
        }

        return NotFound(new { Message = "Device not found" });
    }
}
