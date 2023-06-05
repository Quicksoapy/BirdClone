namespace BirdClone.Domain.Messages;

public class RepostDto
{
    public UInt32 Id { get; private set; }
    public int UserIdOp { get; private set; }
    public string UsernameOp { get; private set; }
    public string ContentOp { get; private set; }
    public DateTime CreatedOnOp { get; private set; }
    public int UserId { get; private set; }
    public string Username { get; private set; }
    public DateTime CreatedOn { get; private set; }
    

    public RepostDto()
    {
        Id = 0;
        UserIdOp = 0;
        ContentOp = string.Empty;
        CreatedOnOp = DateTime.MinValue;
        UserId = 0;
        Username = string.Empty;
        CreatedOn = DateTime.MinValue;
    }
    
    public RepostDto(uint id)
    {
        Id = id;
        UserIdOp = 0;
        ContentOp = string.Empty;
        CreatedOnOp = DateTime.MinValue;
        UserId = 0;
        Username = string.Empty;
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
            throw new ArgumentException("The userid can't be 0.");
        UsernameOp = usernameOp;
        return this;
    }

    public RepostDto WithContentOp(string contentOp)
    {
        if (string.IsNullOrWhiteSpace(contentOp))
            throw new ArgumentException("The content can't be empty.");
        ContentOp = contentOp;
        return this;
    }
    
    public RepostDto WithCreatedOnOp(DateTime createdOn)
    {
        if (createdOn == DateTime.MinValue)
            throw new ArgumentException("The creation date can't be that old.", nameof(createdOn));
        CreatedOnOp = createdOn;
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
    
    public RepostDto WithCreatedOn(DateTime createdOn)
    {
        if (createdOn == DateTime.MinValue)
            throw new ArgumentException("The creation date can't be that old.", nameof(createdOn));
        CreatedOn = createdOn;
        return this;
    }
}