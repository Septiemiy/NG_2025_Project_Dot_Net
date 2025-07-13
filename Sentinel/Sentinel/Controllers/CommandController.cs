using Microsoft.AspNetCore.Mvc;
using SentinelBLL.Models;
using SentinelBLL.Service.Interface;

namespace Sentinel.Controllers;

[Route("api/command")]
[ApiController]
public class CommandController : ControllerBase
{
    private readonly ICommandService _commandService;

    public CommandController(ICommandService commandService)
    {
        _commandService = commandService;
    }

    [HttpPost("createCommand")]
    public async Task<IActionResult> CreateCommand([FromBody] CommandDTO commandDTO)
    {
        var createdCommand = await _commandService.CreateCommandAsync(commandDTO);

        return Ok(createdCommand);
    }
}
