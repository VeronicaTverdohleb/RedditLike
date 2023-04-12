namespace Shared.DTOs;

public class PostCreationDto
{
    public int OwnerId { get; set; }
    public string Title { get; }
    public string Body { get; }

    public PostCreationDto(int ownerId, string title, string body)
    {
        OwnerId = ownerId;
        Title = title;
        Body = body;
    }
}