using Microsoft.AspNetCore.Mvc;
using SentinelBLL.Models;
using SentinelBLL.Service.Interface;

namespace Sentinel.Controllers;

[Route("api/trigger")]
[ApiController]
public class TriggerController : ControllerBase
{
    private readonly ITriggerService _triggerService;

    public TriggerController(ITriggerService triggerService)
    {
        _triggerService = triggerService;
    }

    [HttpPost("createTrigger")]
    public async Task<IActionResult> CreateTrigger([FromBody] TriggerDTO triggerDTO)
    {
        var createdTrigger = await _triggerService.CreateTriggerAsync(triggerDTO);
        
        return Ok(createdTrigger);
    }
}
