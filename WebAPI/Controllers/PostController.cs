using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostLogic _postLogic;

    public PostController(IPostLogic postLogic)
    {
        this._postLogic = postLogic;
    }
  
    [HttpPost]
    public async Task<ActionResult<Post>> CreateAsync([FromBody]PostCreationDto dto)
    {
        try
        {
            Post post = await _postLogic.CreateAsync(dto);
            return Created($"/posts/{post.Id}", post);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAsync([FromBody] PostSearchParametersDto dto)
    {
        try
        {
            PostSearchParametersDto parameters = new(dto.UserName,dto.PostId,dto.Title,dto.Body);
            var posts = await _postLogic.GetAsync(parameters);
            return Ok(posts);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}