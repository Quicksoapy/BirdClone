namespace BirdClone.Domain.Accounts;

public class Account
{
    public int Id { get; set; }
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string Email { get; set; } = "";
    public string Country { get; set; } = "";
    public DateTime CreatedOn { get; set; }
    public DateTime LastLogin { get; set; }
    public string Bio { get; set; } = "";
    public string ProfilePicture { get; set; } = "";
}