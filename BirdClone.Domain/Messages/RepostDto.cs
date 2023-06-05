namespace BirdClone.Domain.Messages;

public class RepostDto
{
    public UInt32 Id { get; private set; }
    public int UserIdOp { get; private set; }
    public string UsernameOp { get; private set; }
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Content { get; private set; }
    public DateTime CreatedOn { get; private set; }

    public RepostDto()
    {
        Id = 0;
        UserIdOp = 0;
        UserId = 0;
        UsernameOp = string.Empty;
        Username = string.Empty;
        Content = string.Empty;
        CreatedOn = DateTime.MinValue;
    }
    
    public RepostDto(uint id)
    {
        Id = id;
        UserId = 0;
        UserId = 0;
        UsernameOp = string.Empty;
        Username = string.Empty;
        Content = string.Empty;
        CreatedOn = DateTime.MinValue;
    }
    
    public RepostDto WithUserIdOp(int userIdOp)
    {
        if (userIdOp == 0)
            throw new ArgumentException("The userid can't be 0.");
        UserIdOp = userIdOp;
        return this;
    }

    public RepostDto WithUsernameOp(string usernameOp)
    {
        if (string.IsNullOrWhiteSpace(usernameOp))
            throw new ArgumentException("The username can't be empty.");
        UsernameOp = usernameOp;
        return this;
    }

    public RepostDto WithUserId(int userId)
    {
        if (userId == 0)
            throw new ArgumentException("The userid can't be 0.");
        UserId = userId;
        return this;
    }

    public RepostDto WithUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("The username can't be empty.");
        Username = username;
        return this;
    }
    
    public RepostDto WithContent(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("The username can't be empty.");
        Content = content;
        return this;
    }

    public RepostDto WithCreatedOn(DateTime createdOn)
    {
        if (createdOn == DateTime.MinValue)
            throw new ArgumentException("The creation date can't be that old.", nameof(createdOn));
        CreatedOn = createdOn;
        return this;
    }
}