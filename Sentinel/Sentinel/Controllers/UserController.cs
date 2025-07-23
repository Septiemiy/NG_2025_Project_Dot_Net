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
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserRegisterDTO userRegisterDto)
    {
        _logger.LogInformation("[INFO][BFF]: Registering new user with username: {Username}", userRegisterDto.Username);
        var result = await _userService.CreateUserAsync(userRegisterDto);

        _logger.LogInformation("[INFO][BFF]: User registered successfully with username: {Username}", userRegisterDto.Username);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] UserLoginDTO userLoginDto)
    {
        _logger.LogInformation("[INFO][BFF]: Registering new user with username: {Username}", userLoginDto.Username);
        var result = await _userService.LoginUserAsync(userLoginDto);
                       
        _logger.LogInformation("[INFO][BFF]: User logged in successfully with username: {Username}", userLoginDto.Username);
        return Ok(result);
    }
}
