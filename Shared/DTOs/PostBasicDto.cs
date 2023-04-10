using System.Reflection.Metadata.Ecma335;

namespace Shared.DTOs;

public class PostBasicDto
{
    public int Id { get; }
    public string OwnerName { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    

    public PostBasicDto(int id, string ownerName, string title, string body)
    {
        Id = id;
        OwnerName = ownerName;
        Title = title;
        Body = body;

    }

 
}