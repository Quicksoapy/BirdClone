namespace BirdClone.Domain.Messages;

public class Message
{
    public UInt32 Id { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; } = "";
    public string Content { get; set; } = "";
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow; 
}