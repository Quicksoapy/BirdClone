namespace BirdClone.Domain.Accounts;

public class AccountDto
{
    public int Id { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }
    public string Country { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime LastLogin { get; private set; }
    public string Bio { get; private set; }
    public string ProfilePicture { get; private set; }

    public AccountDto(int id)
    {
        Id = id;
        Username = string.Empty;
        Password = string.Empty;
        Email = string.Empty;
        Country = string.Empty;
        CreatedOn = DateTime.MinValue;
        LastLogin = DateTime.MinValue;
        Bio = string.Empty;
        ProfilePicture = string.Empty;
    }

    public AccountDto WithUsername(string username)
    {
        Username = username;
        return this;
    }
    
    public AccountDto WithPassword(string password)
    {
        Password = password;
        return this;
    }
    
    public AccountDto WithEmail(string email)
    {
        Email = email;
        return this;
    }
    
    public AccountDto WithCountry(string country)
    {
        Country = country;
        return this;
    }
    
    public AccountDto WithCreatedOn(DateTime createdOn)
    {
        CreatedOn = createdOn;
        return this;
    }
    
    public AccountDto? WithLastLogin(DateTime lastLogin)
    {
        LastLogin = lastLogin;
        return this;
    }
    
    public AccountDto WithBio(string bio)
    {
        Bio = bio;
        return this;
    }
    
    public AccountDto WithProfilePicture(string profilePicture)
    {
        ProfilePicture = profilePicture;
        return this;
    }
}