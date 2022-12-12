using Microsoft.AspNetCore.Mvc;
using projeto_final.Models;
using projeto_final.Repository;

namespace projeto_final.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ITryBlogRepository _repository;

    public UserController(ITryBlogRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_repository.GetAllUsers());
    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(Guid id)
    {
        var user = _repository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] User user)
    {
        _repository.CreateUser(user);
        if(user == null){
            return BadRequest();
        }
        return CreatedAtAction("CreateUser", user);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser(Guid id, [FromBody] User user)
    {
        var userToUpdate = _repository.GetUserById(id);
        if(user == null){
            return BadRequest();
        }
        if (userToUpdate == null)
        {
            return NotFound();
        }
        _repository.UpdateUser(user);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        var userToDelete = _repository.GetUserById(id);
        if (userToDelete == null)
        {
            return NotFound();
        }
        _repository.DeleteUser(userToDelete);
        return NoContent();
    }
}
