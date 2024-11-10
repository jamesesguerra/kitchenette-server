using kitchenette_server.Interfaces.Users;
using kitchenette_server.Models;
using Microsoft.AspNetCore.Mvc;

namespace kitchenette_server.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchUser(string id, User user)
    {
        if (id != user.Id) return BadRequest("User IDs don't match");
        
        await _userService.PatchUser(user);
        return Ok();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _userService.GetUsersAsync(id);
        return Ok(user);
    }

    [HttpPost("")]
    public async Task<IActionResult> AddUser(User user)
    {
        await _userService.AddUser(user);
        return Ok();
    }
}