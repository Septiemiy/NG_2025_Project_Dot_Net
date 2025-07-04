using System.Diagnostics;
using DeviceGatewayBLL.Models;
using DeviceGatewayBLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeviceGatewayBS.Controllers;

[Route("api/command")]
[ApiController]
[Authorize]
public class CommandController : Controller
{
    private readonly ICommandService _commandService;

    public CommandController(ICommandService commandService)
    {
        _commandService = commandService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCommandAsync([FromBody] CommandDTO commandDTO)
    {
        if (commandDTO == null)
        {
            return BadRequest("Command data is null.");
        }


        
        var commandId = await _commandService.AddCommandAsync(commandDTO);

        if (commandId == Guid.Empty)
        {
            return StatusCode(500, "An error occurred while saving the command.");
        }

        return Ok(commandDTO);
    }
}
