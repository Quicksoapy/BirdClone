namespace BirdClone.Domain.Accounts;

public class Account
{
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Account other = (Account)obj;

        return Id == other.Id &&
               Bio == other.Bio &&
               Country == other.Country &&
               Email == other.Email &&
               Password == other.Password &&
               Username == other.Username &&
               CreatedOn == other.CreatedOn &&
               LastLogin == other.LastLogin &&
               ProfilePicture == other.ProfilePicture;
    }

    public int Id { get; private set; }
    public string Username { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }
    public string Country { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime LastLogin { get; private set; }
    public string Bio { get; private set; }
    public string ProfilePicture { get; private set; }

    public Account()
    {
        Id = 0;
        Username = string.Empty;
        Password = string.Empty;
        Email = string.Empty;
        Country = string.Empty;
        CreatedOn = DateTime.MinValue;
        LastLogin = DateTime.MinValue;
        Bio = string.Empty;
        ProfilePicture = string.Empty;
    }
    
    public Account(int id)
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

    public Account WithUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("The username can't be empty.", nameof(username));

        Username = username;
        return this;
    }
    
    public Account WithPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("The password can't be empty.", nameof(password));

        Password = password;
        return this;
    }
    
    public Account WithEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("The email can't be empty.", nameof(email));

        Email = email;
        return this;
    }
    
    public Account WithCountry(string country)
    {
        //if (string.IsNullOrWhiteSpace(country))
        //    throw new ArgumentException("The country can't be empty.", nameof(country));

        Country = country;
        return this;
    }
    
    public Account WithCreatedOn(DateTime createdOn)
    {
        if (createdOn == DateTime.MinValue)
            throw new ArgumentException("The creation date can't be that old.", nameof(createdOn));

        CreatedOn = createdOn;
        return this;
    }
    
    public Account? WithLastLogin(DateTime lastLogin)
    {
        if (lastLogin == DateTime.MinValue)
            throw new ArgumentException("The last login can't be that old.", nameof(lastLogin));

        LastLogin = lastLogin;
        return this;
    }
    
    public Account WithBio(string bio)
    {
        //if (string.IsNullOrWhiteSpace(bio))
        //    throw new ArgumentException("The bio can't be empty.", nameof(bio));

        Bio = bio;
        return this;
    }
    
    public Account WithProfilePicture(string profilePicture)
    {
        //if (string.IsNullOrWhiteSpace(profilePicture))
        //    throw new ArgumentException("The profilePicture can't be empty.", nameof(profilePicture));

        ProfilePicture = profilePicture;
        return this;
    }
}