using System.Diagnostics;

namespace WebAPI_ASPNET_Core.HttpControllers;

[ApiController]
[Route("api/[controller]")]
public class HttpDeleteController:ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public HttpDeleteController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpDelete("delete-user-by-id/{id:int:min(1)}")]
    public async Task<IActionResult> DeleteUserById(int id)
    {
        if (await _context.DeleteUserById(id))
        {
            return Ok($"User({{\"id\":{id}}}) deleted");
        }

        return BadRequest(new NullReferenceException($"User({{\"id\":{id}}}) is not found").Message);
    }
    
    [HttpDelete("delete-users-by-property/{propertyName}/{propertyValue}")]
    [HttpDelete("delete-users-by-property/{propertyName}&{propertyValue}")]
    public async Task<IActionResult> DeleteUserByProperty(string propertyName, string propertyValue)
    {
        try
        {
            int usersDeleted=await _context.DeleteUsersByProperty(propertyName, propertyValue);
            switch(int.TryParse(propertyValue, out int propertyValueInt))
            {
                case true:
                {
                    return Ok($"Users({{\"{propertyName}\":{propertyValueInt}}})({usersDeleted}) deleted");
                }
                case false:
                {
                    return Ok($"Users({{\"{propertyName}\":\"{propertyValue}\"}})({usersDeleted}) deleted");
                }
            }
            
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("delete-users-by-id-range/{startId:int:min(1)}/{endId:int:min(2)}")]
    [HttpDelete("delete-users-by-id-range/{startId:int:min(1)}&{endId:int:min(2)}")]
    [ValidateIdRange]
    public async Task<IActionResult> DeleteUsersByIdRange(int startId, int endId)
    {
        try
        {
            int count = await _context.DeleteUserByIdRange(startId, endId);
            return Ok($"Users({count}) deleted");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}