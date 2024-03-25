
using WebApplication1.Models;

namespace WebAPI_ASPNET_Core.HttpControllers;

[ApiController]
[Route("api/{controller}")]
public class HttpGetController:ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    
    public HttpGetController(ApplicationDbContext context)
    {
        _context = context;
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
    
    [HttpGet("get-product-by-property/{propertyName}&{propertyValue}")]
    public async Task<IActionResult> GetUsersByProperty(string propertyName, object propertyValue)
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