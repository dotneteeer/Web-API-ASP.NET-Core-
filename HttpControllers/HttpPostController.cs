
namespace WebAPI_ASPNET_Core.HttpControllers;


[ApiController]
[Route("api/[controller]")]
public class HttpPostController:ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public HttpPostController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser([FromBody]UserModel user)
    {
        try
        {
            await _context.Insert(new UserModel(){name = user.name, age = user.age});
            return Ok($"User({user}) added");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
    
    [HttpPost("create-users")]
    public async Task<IActionResult> CreateUsers([FromBody]IEnumerable<UserModel> users)
    {
        try
        {
            await _context.InsertSomeValues(users);
            return Ok($"Users({users.Count()}) added");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpPost("create-user-by-property/{name}/{age:int:range(0,115)}")]
    [HttpPost("create-user-by-property/{name}&{age:int:range(0,115)}")]
    public async Task<IActionResult> CreateUserByProperty(string name, int age)
    {
        try
        {
            UserModel user = new UserModel { name = name, age = age };
            await _context.Insert(user);
            return  Ok($"User({user}) added");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}