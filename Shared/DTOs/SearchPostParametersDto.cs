namespace Shared.DTOs;

public class SearchPostParametersDto
{
    public string? Username { get;}
    public int? UserId { get;}
    
    public string? TitleContains { get;}

    public SearchPostParametersDto(string? username, int? userId, string? titleContains)
    {
        Username = username;
        UserId = userId;
        TitleContains = titleContains;
    }  
}