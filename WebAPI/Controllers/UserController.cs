using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{

    private readonly IUserLogic _userLogic;

    public UsersController(IUserLogic userLogic)
    {
        this._userLogic = userLogic;
    }
    
    
    [HttpPost]
    [Route("CreateUser")]
    public async Task<ActionResult<AuthenticationUser>> CreateAsync(UserCreationDto dto)
    {
        try
        {
            AuthenticationUser user = await _userLogic.CreateAsync(dto);
            return Created($"/users/{user.Username}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

   
    
}