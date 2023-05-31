using BirdClone.Domain.Accounts;
using Npgsql;

namespace BirdClone.Domain;

public interface IAccountRepository
{
    int LoginHandler(string username, string password);

    void RegisterHandler(AccountDto accountDto);

    Account GetAccountDataById(int userId);
    void EditAccount(AccountDto account);
    bool AccountExist(AccountDto account);

    bool EmailExist(string email);

    void UpdateLastLogin(int userId);
}