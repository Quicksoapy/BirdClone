using BirdClone.Domain.Accounts;
using Npgsql;

namespace BirdClone.Domain;

public interface IAccountRepository
{
    Task<int> LoginHandler(string username, string password, NpgsqlConnection conn);

    Task RegisterHandler(Account account);

    Task<AccountDto> GetAccountDataById(int userId);
    Task EditAccount(Account account);

    void UpdateLastLogin(int userId);
}