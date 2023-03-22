namespace BirdClone.Models;

public class MessageModel
{
    public UInt32 Id { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; } = "";
    public string Content { get; set; } = "";
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow; 
}