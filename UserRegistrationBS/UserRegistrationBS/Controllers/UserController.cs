using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserRegistrationBL.Models;
using UserRegistrationBL.Services.Interfaces;

namespace UserRegistrationBS.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : Controller
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

        if (userRegisterDto == null)
        {
            _logger.LogWarning("[WARNING][UserRegistration]User registration data is null.");
            return BadRequest(new RegisterLoginResultDTO
            {
                IsSuccess = false,
                Message = "User registration data is null."
            });
        }

        var result = await _userService.CreateUserAsync(userRegisterDto);

        if (!result.IsSuccess)
        {
            _logger.LogWarning("[WARNING][UserRegistration]User registration failed: {Message}", result.Message);
            return BadRequest(result);
        }

        _logger.LogInformation("[INFO][UserRegistration]User registered successfully: {Username}", userRegisterDto.Username);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> UserLoginAsync([FromBody] UserLoginDTO userLoginDto)
    {
        if (userLoginDto == null)
        {
            _logger.LogWarning("[WARNING][UserLogin]User login data is null.");
            return BadRequest(new RegisterLoginResultDTO
            {
                IsSuccess = false,
                Message = "User login data is null."
            });
        }

        var result = await _userService.CheckUserLoginAsync(userLoginDto);

        if (!result.IsSuccess)
        {
            _logger.LogWarning("[WARNING][UserLogin]User login failed: {Message}", result.Message);
            return Unauthorized(result);
        }

        _logger.LogInformation("[INFO][UserLogin]User logged in successfully: {Username}", userLoginDto.Username);
        return Ok(result);
    }
}
