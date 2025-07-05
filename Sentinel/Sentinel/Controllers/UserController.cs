using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SentinelBLL.Clients;
using SentinelBLL.Models;
using SentinelBLL.Service.Interface;

namespace Sentinel.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] UserRegistrationDTO userRegistrationDto)
    {
        var userToken = await _userService.CreateUserAsync(userRegistrationDto);

        return Ok(userToken);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserLoginDTO userLoginDto)
    {
        var userToken = await _userService.LoginUserAsync(userLoginDto);
                       
        return Ok(userToken);
    }
}
