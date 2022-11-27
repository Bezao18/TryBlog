using Microsoft.AspNetCore.Mvc;

namespace projeto_final.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<User> GetAll()
    {
        return Enumerable.Range(1, 5).Select(index => new User()
       )
        .ToArray();
    }
}
