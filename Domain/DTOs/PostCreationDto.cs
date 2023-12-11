using Domain.Models;
namespace Domain.DTOs;

public class PostCreationDto
{
    public string Username { get; set; }
    public string Title { get; set; }
    public string body { get; set; }

    public PostCreationDto(string username, string title, string body)
    {
        this.Username = username;
        Title = title;
        this.body = body;
    }
}