using Microsoft.AspNetCore.Mvc;
using projeto_final.Models;
using projeto_final.Repository;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;


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

    [Authorize]
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

    [Authorize]
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
