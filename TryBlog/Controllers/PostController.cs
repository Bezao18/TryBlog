using Microsoft.AspNetCore.Mvc;

namespace projeto_final.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly ILogger<PostController> _logger;

    public PostController(ILogger<PostController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Post> GetAll()
    {
        return Enumerable.Range(1, 5).Select(index => new Post()
       )
        .ToArray();
    }

    [HttpPost]
    public IEnumerable<Post> AddPost()
    {
        return Enumerable.Range(1, 5).Select(index => new Post()
       )
        .ToArray();
    }
}
