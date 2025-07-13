using DeviceGatewayBLL.Models;
using DeviceGatewayBLL.Services.Interfaces;
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
            return Ok(deviceDTO.DeviceId);
        }

        return StatusCode((int)response.StatusCode, response.ReasonPhrase);
    }
}
