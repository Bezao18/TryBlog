using Microsoft.AspNetCore.Mvc;
using projeto_final.Models;
using projeto_final.Repository;

namespace projeto_final.Controllers;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly ITryBlogRepository _repository;

    public PostController(ITryBlogRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
       return Ok(_repository.GetAllUsers());
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
       _repository.CreateUser(user);
       return CreatedAtAction("CreateUser", user);
    }
}
