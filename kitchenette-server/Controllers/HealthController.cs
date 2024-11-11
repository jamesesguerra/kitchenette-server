using Dapper;
using kitchenette_server.Interfaces.DbContext;
using Microsoft.AspNetCore.Mvc;

namespace kitchenette_server.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    private readonly IDbContext _context;
    
    public HealthController(IDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("db")]
    public async Task<IActionResult> GetDbHealth()
    {
        using var connection = _context.CreateConnection();
        return Ok(await connection.QueryAsync(" SELECT 1 "));
    }
}