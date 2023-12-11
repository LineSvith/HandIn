using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    
    private readonly IConfiguration _config;
    private readonly IUserLogic _userLogic;

    public AuthController(IConfiguration config,IUserLogic userLogic)
    {
        this._config = config;
        this._userLogic = userLogic;
    }
    
    
    [HttpPost, Route("login")]
    public async Task<ActionResult> Login([FromBody] AuthUserLoginDto userLoginDto)
    {
        try
        {
            AuthenticationUser user = await _userLogic.ValidateLogin(userLoginDto);
            string token = GenerateJwt(user);
    
            return Ok(token);
        }
        catch (Exception e)
        {
            Console.WriteLine("Det virker ikke");
            return BadRequest(e.Message);
            
        }
    }
    
    
    private string GenerateJwt(AuthenticationUser user)
    {
        List<Claim> claims = GenerateClaims(user);
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    
        JwtHeader header = new JwtHeader(signIn);
    
        JwtPayload payload = new JwtPayload(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims, 
            null,
            DateTime.UtcNow.AddMinutes(60));
    
        JwtSecurityToken token = new JwtSecurityToken(header, payload);
    
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }
    
    private List<Claim> GenerateClaims(AuthenticationUser user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("DisplayName", user.Name),
            new Claim("Email", user.Email),
            new Claim("Age", user.Age.ToString()),
            new Claim("SecurityLevel", user.SecurityLevel.ToString())
        };
        return claims.ToList();
    }
    
}