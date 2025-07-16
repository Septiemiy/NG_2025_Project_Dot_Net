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
            return BadRequest(new RegisterLoginResultDTO
            {
                IsSuccess = false,
                Message = "User registration data is null."
            });
        }

        var result = await _userService.CreateUserAsync(userRegisterDto);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> UserLoginAsync([FromBody] UserLoginDTO userLoginDto)
    {
        if (userLoginDto == null)
        {
            return BadRequest(new RegisterLoginResultDTO
            {
                IsSuccess = false,
                Message = "User login data is null."
            });
        }

        var result = await _userService.CheckUserLoginAsync(userLoginDto);

        if (!result.IsSuccess)
        {
            return Unauthorized(result);
        }

        return Ok(result);
    }
}
