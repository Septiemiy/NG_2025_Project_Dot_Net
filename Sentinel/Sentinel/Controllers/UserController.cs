using System.Diagnostics;
using System.Text.Json;
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
    public async Task<IActionResult> CreateUserAsync([FromBody] UserRegisterDTO userRegisterDto)
    {
        var json = JsonSerializer.Serialize(userRegisterDto, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        Console.WriteLine(json);

        var userToken = await _userService.CreateUserAsync(userRegisterDto);

        return Ok(userToken);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] UserLoginDTO userLoginDto)
    {
        var userToken = await _userService.LoginUserAsync(userLoginDto);
                       
        return Ok(userToken);
    }
}
