namespace BirdClone.Domain.Messages;

public class MessageDto
{
    public UInt32 Id { get; private set; }
    public int UserId { get; private set; }
    public string Username { get; private set; } = "";
    public string Content { get; private set; } = "";
    public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;

    public MessageDto(uint id)
    {
        Id = id;
        UserId = 0;
        Username = string.Empty;
        Content = string.Empty;
        CreatedOn = DateTime.MinValue;
    }

    public MessageDto WithUserId(int userId)
    {
        UserId = userId;
        return this;
    }

    public MessageDto WithUsername(string username)
    {
        Username = username;
        return this;
    }
    
    public MessageDto WithContent(string content)
    {
        Content = content;
        return this;
    }

    public MessageDto WithCreatedOn(DateTime createdOn)
    {
        CreatedOn = createdOn;
        return this;
    }
}