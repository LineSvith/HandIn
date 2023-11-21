using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterface;

public interface IUserLogic
{
    public Task<AuthenticationUser> CreateAsync(UserCreationDto dto);

    public Task<AuthenticationUser> ValidateLogin(AuthUserLoginDto dto);
}