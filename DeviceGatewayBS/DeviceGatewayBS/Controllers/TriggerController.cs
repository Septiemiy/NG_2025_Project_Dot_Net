using DeviceGatewayBLL.Models;
using DeviceGatewayBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeviceGatewayBS.Controllers;

[Route("api/trigger")]
[ApiController]
public class TriggerController : Controller
{
    private readonly ITriggerService _triggerService;

    public TriggerController(ITriggerService triggerService)
    {
        _triggerService = triggerService;
    }

    [HttpPost("createTrigger")]
    public async Task<IActionResult> CreateTriggerAsync([FromBody] TriggerDTO triggerDTO)
    {
        if (triggerDTO == null)
        {
            return BadRequest("Trigger data is null.");
        }

        var triggerId = await _triggerService.AddTriggerAsync(triggerDTO);

        if (triggerId == Guid.Empty)
        {
            return StatusCode(500, "An error occurred while saving the trigger.");
        }

        return Ok(triggerId);
    }
}
