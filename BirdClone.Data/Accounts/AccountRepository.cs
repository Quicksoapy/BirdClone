using BirdClone.Domain;
using BirdClone.Domain.Accounts;
using Npgsql;

namespace BirdClone.Data.Accounts;

public class AccountRepository : IAccountRepository
{
    private readonly string _connectionString;

    public AccountRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<int> LoginHandler(string username, string password, NpgsqlConnection conn)
    {
        throw new NotImplementedException();
    }

    public Task RegisterHandler(Account account)
    {
        throw new NotImplementedException();
    }

    public Task<AccountDto> GetAccountDataById(int userId)
    {
        throw new NotImplementedException();
    }

    public Task EditAccount(Account account)
    {
        throw new NotImplementedException();
    }

    public void UpdateLastLogin(int userId)
    {
        throw new NotImplementedException();
    }
}