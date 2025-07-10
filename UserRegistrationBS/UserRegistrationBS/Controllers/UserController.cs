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

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserRegisterDTO userRegisterDto)
    {
        
        if (userRegisterDto == null)
        {
            return BadRequest("User data is null.");
        }

        var userToken = await _userService.CreateUserAsync(userRegisterDto);

        if (string.IsNullOrEmpty(userToken))
        {
            return BadRequest("User registration failed.");
        }

        return Ok(userToken);
    }

    [HttpPost("login")]
    public async Task<IActionResult> UserLoginAsync([FromBody] UserLoginDTO userLoginDto)
    {
        if (userLoginDto == null)
        {
            return BadRequest("User login data is null.");
        }

        var userToken = await _userService.CheckUserLogin(userLoginDto);

        if (string.IsNullOrEmpty(userToken))
        {
            return Unauthorized("Invalid username or password.");
        }

        return Ok(userToken);
    }
}
