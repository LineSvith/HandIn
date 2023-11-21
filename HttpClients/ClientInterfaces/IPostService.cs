using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task<Post> Create(PostCreationDto dto);
    Task<ICollection<Post>> GetAsync(string? userName, int? postId, string? titleContains, string? bodyContains);
}
