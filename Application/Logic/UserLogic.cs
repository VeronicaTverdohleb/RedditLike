using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class UserLogic :IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.UserName);
        if (existing != null)
            throw new Exception("Username already taken!");
        if (dto.Password.Length < 4)
            throw new Exception("Password is too short, must be at least 5 characters");

       
        ValidateData(dto);
        User toCreate = new User(dto.UserName,dto.Domain, dto.Password)
        {
            UserName = dto.UserName,
            Password=dto.Password
        };
        
        
        User created = await userDao.CreateAsync(toCreate);
        
        return created;
    }

    private static void ValidateData(UserCreationDto userToCreate)
    {
        string userName = userToCreate.UserName;
        string password = userToCreate.Password;
        if (userName.Length < 3)
            throw new Exception("Username must be at least 3 characters!");

        if (userName.Length > 15)
            throw new Exception("Username must be less than 16 characters!");
        if (password.Length < 4)
            throw new Exception("Password is too short, must be at least 5 characters");

    }
    
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        return userDao.GetAsync(searchParameters);
    }
}