using Shared.DTOs;
using Shared.Models;

namespace WebAPI.Services;

public interface IAuthService
{
    Task<User> GetUser(string username, string password);
    Task<User> RegisterUser(UserCreationDto dto); 
}