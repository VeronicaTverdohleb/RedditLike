using Newtonsoft.Json;

namespace Shared.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Domain { get; set; }
    
    public int SecurityLevel { get; set; }  

    public User(string userName, string domain, string password)
    {
        UserName = userName;
        Domain = domain;
        Password = password;
     
    }

    

    public User()
    {
        
    }
}