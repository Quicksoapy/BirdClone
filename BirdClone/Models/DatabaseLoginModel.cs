namespace BirdClone.Models;

public class DatabaseLoginModel
{
    public string server { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public string database { get; set; }

    public string port { get; set; }
}