using Microsoft.AspNetCore.Mvc;
using projeto_final.Models;
using projeto_final.Repository;
using projeto_final.Services;

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
    public IActionResult Login([FromBody] User user)
    {
        var userToLogin = _repository.GetUserById(user.Email);
        if (userToLogin == null)
        {
            return NotFound("User not Found");
        }
        if (userToLogin.Password != user.Password || userToLogin.Username != user.Username)
        {
            return BadRequest("Invalid credentials");
        }
        userToLogin.Password = null;
        var token = new TokenGenerator().Generate(userToLogin);
        return Ok(token);
    }
   

    [HttpGet("signup")]
    public IActionResult Signup([FromBody] User user)
    {
        var userToSignup = _repository.GetUserById(user.UserId);
        if (userToSignup != null)
        {
            return BadRequest("User already exists");
        }
        _repository.CreateUser(user);
        user.Password = null;
        var token = new TokenGenerator().Generate(user);
        return CreatedAtAction("Signup", token);
    }
  
}
