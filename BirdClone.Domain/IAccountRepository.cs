using BirdClone.Domain.Accounts;
using Npgsql;

namespace BirdClone.Domain;

public interface IAccountRepository
{
    Task<int> LoginHandler(string username, string password);

    Task RegisterHandler(AccountDto accountDto);

    Task<Account> GetAccountDataById(int userId);
    Task EditAccount(AccountDto account);

    void UpdateLastLogin(int userId);
}