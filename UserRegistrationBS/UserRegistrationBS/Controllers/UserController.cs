using Microsoft.AspNetCore.Mvc;
using UserRegistrationBL.Models;
using UserRegistrationBL.Services.Interfaces;

namespace UserRegistrationBS.Controllers;

[Route("api/user")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto)
    {
        if (userDto == null)
        {
            return BadRequest("User data is null.");
        }

        var userGuid = await _userService.CreateUserAsync(userDto);

        if (userGuid == Guid.Empty)
        {
            return StatusCode(500, "An error occurred while creating the user.");
        }

        return View();
    }
}
