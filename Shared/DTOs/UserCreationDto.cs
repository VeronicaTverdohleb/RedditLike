namespace Shared.DTOs;

public class UserCreationDto
{
    public string UserName { get;}
    public string Password { get; }
    public string Domain { get; set; }

    public UserCreationDto(string userName,string domain, string password)
    {
        UserName = userName;
        Password = password;
        Domain = domain;
    }
}