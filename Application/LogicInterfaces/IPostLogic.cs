using Domain.DTOs;
using Domain.Models;
namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDto dto);
    Task<IEnumerable<Post>> GetAsync(PostSearchParametersDto dto);
}