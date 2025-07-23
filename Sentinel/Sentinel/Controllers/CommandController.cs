using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SentinelBLL.Models;
using SentinelBLL.Service.Interface;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sentinel.Controllers;

[Route("api/command")]
[ApiController]
public class CommandController : ControllerBase
{
    private readonly ICommandService _commandService;
    private readonly ILogger<CommandController> _logger;

    public CommandController(ICommandService commandService, ILogger<CommandController> logger)
    {
        _commandService = commandService;
        _logger = logger;
    }

    [HttpPost("sendCommand")]
    public async Task<IActionResult> SendCommand([FromBody] CommandDTO commandDTO)
    {
        var createdCommand = await _commandService.SendCommandAsync(commandDTO);

        if (createdCommand == null)
        {
            return BadRequest(new { message = "Failed to send command" });
        }

        _logger.LogInformation("Command sent successfully: {Command}", JsonSerializer.Serialize(createdCommand));

        return Ok(createdCommand);
    }
}
