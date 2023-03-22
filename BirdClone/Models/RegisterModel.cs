namespace BirdClone.Models;

public class RegisterModel
{
    public UInt32 Id { get; set; }
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string Email { get; set; } = "";
}