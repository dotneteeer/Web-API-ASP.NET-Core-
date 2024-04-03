namespace WebAPI_ASPNET_Core.HttpControllers;

[ApiController]
[Route("api/[controller]")]
public class HttpPutController:ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public HttpPutController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpPut("update-user-by-id/{id:int:min(1)}")]
    public async Task<IActionResult> UpdateUserById(int id, [FromBody]UserModel newUser)
    {
        try
        {
            if (await _context.UpdateUserById(id, new UserModel{name=newUser.name, age = newUser.age}))
            {
                return Ok($"User({{\"id\":{id}, \"Name\":\"{newUser.name}\",\"age\":{newUser.age}}}) updated");
            }

            return BadRequest(new NullReferenceException($"User({{\"id\":{id}, {newUser}}}) is not found").Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}