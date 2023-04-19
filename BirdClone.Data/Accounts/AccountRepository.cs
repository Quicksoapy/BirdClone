using BirdClone.Domain;
using BirdClone.Domain.Accounts;
using Npgsql;

namespace BirdClone.Data.Accounts;

public class AccountRepository : IAccountRepository
{
    private readonly string _connectionString;

    public AccountRepository()
    {
        var globals = new Globals();
        _connectionString = globals.GetDatabaseConnectionString();
    }

    public async Task<int> LoginHandler(string username, string password)
    {
        await using var cmd =
            new NpgsqlCommand(
                "SELECT Id FROM accounts WHERE username='" + username + "' AND password='" + password + "';", new NpgsqlConnection(_connectionString));
        var result = await cmd.ExecuteScalarAsync();
        
        var resultInt32 = Convert.ToInt32(result);
        if (resultInt32 > 0)
        {
            UpdateLastLogin(resultInt32);
        }

        return resultInt32;
    }

    public Task RegisterHandler(AccountDto accountDto)
    {
        throw new NotImplementedException();
    }

    public async Task<Account> GetAccountDataById(int userId)
    {
        var account = new Account();
        
        await using var cmd = new NpgsqlCommand("SELECT * FROM accounts WHERE id = " + userId, new NpgsqlConnection(_connectionString));
        var dataReader = await cmd.ExecuteReaderAsync();
        while (dataReader.Read())
        {
            account.Id = dataReader.GetInt32(dataReader.GetOrdinal("id"));
            account.Username = dataReader.GetString(dataReader.GetOrdinal("username"));
            account.Email = dataReader.GetString(dataReader.GetOrdinal("email"));
            account.Country = dataReader.GetString(dataReader.GetOrdinal("country"));
            account.CreatedOn = dataReader.GetDateTime(dataReader.GetOrdinal("created_on"));
            account.LastLogin = dataReader.GetDateTime(dataReader.GetOrdinal("last_login"));
        }

        return account;
    }

    public Task EditAccount(AccountDto account)
    {
        using var cmd = new NpgsqlCommand("UPDATE accounts SET username = $1, password = $2, " +
                                          "email = $3, country = $4 WHERE id = $5;", new NpgsqlConnection(_connectionString))
        {
            Parameters =
            {
                new NpgsqlParameter { Value = account.Username },
                new NpgsqlParameter { Value = account.Password },
                new NpgsqlParameter { Value = account.Email },
                new NpgsqlParameter { Value = account.Country },
                new NpgsqlParameter { Value = account.Id}
            }
        };
        var result = cmd.ExecuteNonQueryAsync().Result;
        Console.WriteLine(result);
        return Task.CompletedTask;
    }

    public void UpdateLastLogin(int userId)
    {
        using var cmd = new NpgsqlCommand("UPDATE accounts SET last_login = $1 WHERE id = $2;", new NpgsqlConnection(_connectionString))
        {
            Parameters =
            {
                new NpgsqlParameter { Value = DateTime.UtcNow },
                new NpgsqlParameter { Value = userId }
            }
        };
        var result = cmd.ExecuteNonQueryAsync().Result;
        Console.WriteLine(result);
    }
}