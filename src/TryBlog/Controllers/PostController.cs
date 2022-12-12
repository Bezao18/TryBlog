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

    [HttpGet("{postId}")]
    public IActionResult GetPost(Guid postId)
    {
        var post = _repository.GetPost(postId);
        if (post == null)
        {
            return NotFound();
        }
        return Ok(post);
    }

    [HttpGet("{userId}")]
    public IActionResult GetAll(Guid userId)
    {
       return Ok(_repository.GetPostsByUser(userId));
    }

    [HttpPost]
    public IActionResult CreatePost([FromBody] Post post)
    {
        if(post == null){
            return BadRequest();
        }
        _repository.CreatePost(post);
        return CreatedAtAction("CreatePost", post);
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePost(Guid id, [FromBody] Post post)
    {
        var postToUpdate = _repository.GetPost(id);
        if(post == null){
            return BadRequest();
        }
        if (postToUpdate == null)
        {
            return NotFound();
        }
        _repository.UpdatePost(id, post);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletePost(Guid id)
    {
        var postToDelete = _repository.GetPost(id);
        if (postToDelete == null)
        {
            return NotFound();
        }
        _repository.DeletePost(id);
        return NoContent();
    }
}
