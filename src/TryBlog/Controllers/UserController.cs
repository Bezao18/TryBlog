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
        var token = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
        var userId = new Guid(token.Payload["UserId"].ToString()!);
        if(user == null){
            return BadRequest();
        }
        if (userToUpdate == null)
        {
            return NotFound();
        }
        if(userToUpdate.UserId != userId){
            return Unauthorized();
        }
        _repository.UpdateUser(user);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        var token = new JwtSecurityToken(HttpContext.Request.Headers["Authorization"].ToString().Substring(7));
        var userId = new Guid(token.Payload["UserId"].ToString()!);
        var userToDelete = _repository.GetUserById(id);
        if (userToDelete == null)
        {
            return NotFound();
        }
        if(userToDelete.UserId != userId){
            return Unauthorized();
        }
        _repository.DeleteUser(userToDelete);
        return NoContent();
    }
}
