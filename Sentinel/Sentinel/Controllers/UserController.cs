using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SentinelBLL.Clients;
using SentinelBLL.Models;

namespace Sentinel.Controllers;

[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserClient _userClient;

    public UserController(IUserClient userClient)
    {
        _userClient = userClient;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto)
    {
        if (userDto == null)
        {
            return BadRequest("User data cannot be null.");
        }

        var userGuid = await _userClient.CreateUserAsync(userDto);

        return Ok(userGuid);
    }
}
