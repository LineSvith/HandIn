using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;
    
    
    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreationDto dto)
    {
        
        if (dto.Username == null)
            throw new Exception("You need to Login first!");
        
        AuthenticationUser? ownerUsername = await userDao.GetByUsernameAsync(dto.Username);
        
        if (ownerUsername == null)
            throw new Exception("Username does not exist");

        Post toCreate = new Post(ownerUsername.Id, dto.Title, dto.body);

        Post Created = await postDao.CreateAsync(toCreate);

        return Created;
    }

    public Task<IEnumerable<Post>> GetAsync(PostSearchParametersDto dto)
    {
        return postDao.GetAsync(dto);
    }
}