using Microsoft.AspNetCore.Mvc;
using projeto_final.Models;
using projeto_final.Repository;

namespace projeto_final.Controllers;

[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ITryBlogRepository _repository;

    public AuthenticationController(ITryBlogRepository repository)
    {
        _repository = repository;
    }

    [HttpPost("login")]
    public IActionResult Login()
    {
      var user = _repository.GetUserById(id);
      if (user == null)
      {
          return NotFound();
      }
      return Ok();
    }

    [HttpGet("signup")]
    public IActionResult Signup(Guid id)
    {
        var user = _repository.GetUserById(id);
        if (user == null)
        {
            return Ok();
        }
        return BadRequest("User already exists");
    }
}
