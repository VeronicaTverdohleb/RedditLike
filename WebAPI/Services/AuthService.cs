using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.DaoInterfaces;
using FileData;
using FileData.DAOs;
using Microsoft.OpenApi;
using Newtonsoft.Json;
using Shared.DTOs;
using Shared.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebAPI.Services;

public class AuthService: IAuthService
{
    /* private readonly HttpClient client;
     //private readonly IList<User> users1;
     private static string filePath = Path.GetFullPath(@"data.json");
     private static string json = File.ReadAllText(filePath);
     //FileData instance iuserDAO acces users 
     
 
    // private readonly List<User> users = JsonConvert.DeserializeObject(json);*/
    //private const string filePath = "data.json";
    private IUserDao iuser = new UserFileDao(new FileContext());
    //private UserCreationDto dto;
    
    //private FileContext fileContext;

    //private UserFileDao userFileDao;
      /* private readonly IList<User> users  = new List<User>
     {
         new User
         {
             UserName = "Angela",
             Domain="Redditor",
             Password = "onetwo3FOUR"
             
         },
         new User
         {
             UserName = "Steeve",
             Domain="Redditor",
             Password = "password"
            
         }
     };*/
      
      
    
    public async Task<User> GetUser(string username, string password)
    {
        User? existingUser = await iuser.GetByUsernameAsync(username);
            
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }
        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }

   

    public async Task<User> RegisterUser(UserCreationDto dto)
    {
        User? existing = await iuser.GetByUsernameAsync(dto.UserName);
        if (existing != null)
            throw new Exception("Username already taken");
        User user = new User
        {
            UserName = dto.UserName,
            Password = dto.Password
        };

        if (string.IsNullOrEmpty(user.UserName))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here
        
        // save to persistence instead of list

        User created = await iuser.CreateAsync(user);
        
        
        return created;
    }  
    
    
}