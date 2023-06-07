using BirdClone.Domain;
using BirdClone.Domain.Accounts;

namespace BirdClone.UnitTests;

public class AccountRepositoryTestTrue : IAccountRepository
{
    public int LoginHandler(string username, string password)
    {
        return 5;
    }

    public void RegisterHandler(AccountDto accountDto)
    {
        return;
    }

    public AccountDto GetAccountDataById(int userId)
    {
        var account = new AccountDto(userId)
            .WithBio("Bio")
            .WithCountry("Country")
            .WithEmail("Email")
            .WithPassword("Password")
            .WithUsername("Username")
            .WithCreatedOn(DateTime.UnixEpoch)
            .WithLastLogin(DateTime.UnixEpoch)
            .WithProfilePicture("UrlToProfilePicture");

        return account;
    }

    public void EditAccount(AccountDto account)
    {
        return;
    }

    public bool AccountExist(AccountDto account)
    {
        return true;
    }

    public bool EmailExist(string email)
    {
        return true;
    }

    public void UpdateLastLogin(int userId)
    {
        return;
    }
}