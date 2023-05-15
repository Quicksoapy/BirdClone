using System.Security.Cryptography;

namespace BirdClone.Domain.Messages;

public class Message
{
    public UInt32 Id { get; private set; }
    public int UserId { get; private set; }
    public string Username { get; private set; } = "";
    public string Content { get; private set; } = "";
    public DateTime CreatedOn { get; private set; } = DateTime.UtcNow;

    public Message(uint id)
    {
        if (id == 0) throw new ArgumentException("The id can't be 0.");
        Id = id;
        UserId = 0;
        Username = string.Empty;
        Content = string.Empty;
        CreatedOn = DateTime.MinValue;
    }

    public Message WithUserId(int userId)
    {
        if (userId == 0)
            throw new ArgumentException("The userid can't be 0.");
        UserId = userId;
        return this;
    }

    public Message WithUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("The username can't be empty.");
        Username = username;
        return this;
    }
    
    public Message WithContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("The username can't be empty.");
        Content = content;
        return this;
    }

    public Message WithCreatedOn(DateTime createdOn)
    {
        if (createdOn == DateTime.MinValue)
            throw new ArgumentException("The creation date can't be that old.", nameof(createdOn));
        CreatedOn = createdOn;
        return this;
    }

}