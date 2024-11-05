using kitchenette_server.Interfaces.Users;
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
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _userService.GetUsersAsync(id);
        return Ok(user);
    }
}