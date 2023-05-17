namespace BirdClone.Domain.Messages;

public class MessageModel
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Content { get; set; }
    public DateTime CreatedOn { get; set; }
}