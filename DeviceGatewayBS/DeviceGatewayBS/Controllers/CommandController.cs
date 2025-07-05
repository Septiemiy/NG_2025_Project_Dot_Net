using System.Diagnostics;
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

    public CommandController(ICommandService commandService)
    {
        _commandService = commandService;
    }

    [HttpPost("sendToAll")]
    public async Task<IActionResult> CreateCommandAsync([FromBody] CommandDTO commandDTO)
    {
        if (commandDTO == null)
        {
            return BadRequest("Command data is null.");
        }

        if (await _commandService.SendCommandToAll(commandDTO.CommandName))
        {
            commandDTO.Status = CommandStatus.Success;
            var commandId = await _commandService.AddCommandAsync(commandDTO);

            if (commandId == Guid.Empty)
            {
                return StatusCode(500, "An error occurred while saving the command.");
            }

            return Ok(commandDTO);
        }
        else
        {
            commandDTO.Status = CommandStatus.Failed;
            var commandId = await _commandService.AddCommandAsync(commandDTO);

            if (commandId == Guid.Empty)
            {
                return StatusCode(500, "An error occurred while saving the command." +
                                       "Failed to send command to all devices");
            }

            return StatusCode(500, "Failed to send command to all devices.");
        }
    }
}
