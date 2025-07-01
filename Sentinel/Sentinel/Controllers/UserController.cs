using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SentinelBLL.Clients;
using SentinelBLL.Models;

namespace Sentinel.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserClient _userClient;

    public UserController(IUserClient userClient)
    {
        _userClient = userClient;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] UserRegistrationDTO userDto)
    {
        if (userDto == null)
        {
            return BadRequest("User data cannot be null.");
        }

        var userToken = await _userClient.CreateUserAsync(userDto);

        return Ok(userToken);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserLoginDTO userLoginDto)
    {
        if (userLoginDto == null)
        {
            return BadRequest("Login data cannot be null.");
        }
        
        var userToken = await _userClient.LoginUserAsync(userLoginDto);
        
        if (string.IsNullOrEmpty(userToken))
        {
            return Unauthorized("Invalid username or password.");
        }
        
        return Ok(userToken);
    }
}
