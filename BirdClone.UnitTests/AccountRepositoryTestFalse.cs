using BirdClone.Domain;
using BirdClone.Domain.Accounts;

namespace BirdClone.UnitTests;

public class AccountRepositoryTestFalse : IAccountRepository
{
    public int LoginHandler(string username, string password)
    {
        return 0;
    }

    public void RegisterHandler(AccountDto accountDto)
    {
        return;
    }

    public AccountDto GetAccountDataById(int userId)
    {
        var account = new AccountDto(1);
        return account;
    }

    public void EditAccount(AccountDto account)
    {
        return;
    }

    public bool AccountExist(AccountDto account)
    {
        return false;
    }

    public bool EmailExist(string email)
    {
        return false;
    }

    public void UpdateLastLogin(int userId)
    {
        return;
    }
}