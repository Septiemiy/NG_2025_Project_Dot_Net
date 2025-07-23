using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.Json;
using DAL_Core.Enums;
using DeviceGatewayBLL.Models;
using DeviceGatewayBLL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeviceGatewayBS.Controllers;

[Route("api/command")]
[ApiController]
public class CommandController : Controller
{
    private readonly ICommandService _commandService;
    private readonly ILogger<CommandController> _logger;

    public CommandController(ICommandService commandService, ILogger<CommandController> logger)
    {
        _commandService = commandService;
        _logger = logger;
    }

    [HttpPost("sendCommand")]
    public async Task<IActionResult> CreateCommandAsync([FromBody] CommandDTO commandDTO)
    {
        if (commandDTO == null)
        {
            _logger.LogError("[ERROR][DeviceGateway]Command data is null.");
            return BadRequest("Command data is null.");
        }

        if (await _commandService.SendCommandAsync(commandDTO))
        {
            commandDTO.Status = CommandStatus.Success;
            var commandId = await _commandService.AddCommandAsync(commandDTO);
            
            if (commandId == Guid.Empty)
            {
                _logger.LogError("[ERROR][DeviceGateway]An error occurred while saving the command.");
                return StatusCode(500, "An error occurred while saving the command.");
            }

            _logger.LogInformation("[INFO][DeviceGateway]Command stored in database with ID: {DeviceId}", commandId);

            _logger.LogInformation("[INFO][DeviceGateway]Command sent successfully to device.");
            return Ok(commandDTO);
        }
        else
        {
            commandDTO.Status = CommandStatus.Failed;
            var commandId = await _commandService.AddCommandAsync(commandDTO);

            if (commandId == Guid.Empty)
            {
                _logger.LogError("[ERROR][DeviceGateway]An error occurred while saving the command." + "Failed to send command to device");
                return StatusCode(500, "An error occurred while saving the command." +
                                       "Failed to send command to device");
            }

            _logger.LogInformation("[INFO][DeviceGateway]Command stored in database with ID: {DeviceId}", commandId);

            _logger.LogError("[ERROR][DeviceGateway]Failed to send command to device.");
            return StatusCode(500, "Failed to send command to device.");
        }
    }
}
