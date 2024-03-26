using Microsoft.AspNetCore.Mvc;
using WebAPI_ASPNET_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_ASPNET_Core.HttpControllers;

[ApiController]
[Route("api/[controller]")]
public class HttpGetController:ControllerBase
{
    private readonly ApplicationDbContext _context;
    public readonly IConfiguration _configuration; 
    public HttpGetController(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
        
    [HttpGet("get-all-users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users= await _context.GetAllUsers();

        if (users.Count() != 0)
        {
            return Ok(users);
        }

        return NoContent();
    }
    
    [HttpGet("get-user-by-id/{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _context.GetUserById(id);
        if (user != null)
        {
            return Ok(user);
        }

        return NoContent();


    }
    
    [HttpGet("get-product-by-property")]
    public async Task<IActionResult> GetUsersByProperty([FromQuery]string propertyName, [FromQuery]string propertyValue)
    {
        try
        {
            var usersByProperty = await _context.GetUsersByProperty(propertyName, propertyValue);
            return Ok(usersByProperty);
        }
        catch(ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }
    
}

