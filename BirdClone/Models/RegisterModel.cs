using System.ComponentModel.DataAnnotations;

namespace BirdClone.Models;

public class RegisterModel
{
    public UInt32 Id { get; set; }
    [Required] public string Username { get; set; } = "";
    [Required] public string Password { get; set; } = "";
}