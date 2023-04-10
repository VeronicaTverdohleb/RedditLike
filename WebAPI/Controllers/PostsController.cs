using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Models;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class PostsController : ControllerBase
{
  private readonly IPostLogic postLogic;
  
  public PostsController(IPostLogic postLogic)
  {
    this.postLogic = postLogic;
  }

  [HttpPost]
  public async Task<ActionResult<Post>> CreateAsync(PostCreationDto dto)
  {
    try
    {
      Post post = await postLogic.CreateAsync(dto);
      return Created($"/posts/{post.Id}", post);

    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      return StatusCode(500, e.Message);
    }
  }
   
  [HttpGet]
  public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromQuery] string? userName, [FromQuery] int? userId
   , [FromQuery] string? titleContains)
  {
    try
    {
      SearchPostParametersDto parameters = new(userName, userId, titleContains);
      var todos = await postLogic.GetAsync(parameters);
      return Ok(todos);
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      return StatusCode(500, e.Message);
    }
  }
  [HttpPatch]
  public async Task<ActionResult> UpdateAsync([FromBody] PostUpdateDto dto)
  {
    try
    {
      await postLogic.UpdateAsync(dto);
      return Ok();
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      return StatusCode(500, e.Message);
    }
  }
  [HttpDelete("{id:int}")]
  public async Task<ActionResult> DeleteAsync([FromRoute] int id)
  {
    try
    {
      await postLogic.DeleteAsync(id);
      return Ok();
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      return StatusCode(500, e.Message);
    }
  }
   
  [HttpGet("{id:int}")]
  public async Task<ActionResult<PostBasicDto>> GetById([FromRoute] int id)
  {
    try
    {
      PostBasicDto result = await postLogic.GetByIdAsync(id);
      return Ok(result);
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      return StatusCode(500, e.Message);
    }
  }
}