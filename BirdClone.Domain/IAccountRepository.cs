using BirdClone.Domain.Accounts; //TODO Why do i have to add this here but not in IMessageRepository???
using Npgsql;
//TODO the repositories don't seem to care if they have the things the interface tells them to have. 
//TODO my codebase worked differently as i use static async funcs
namespace BirdClone.Domain;

public interface IAccountRepository
{
    public static async Task<int> LoginHandler(string username, string password, NpgsqlConnection conn)
    {
        await using var cmd =
            new NpgsqlCommand(
                "SELECT Id FROM accounts WHERE username='" + username + "' AND password='" + password + "';", conn);
        var result = await cmd.ExecuteScalarAsync();
        
        var resultInt32 = Convert.ToInt32(result);
        if (resultInt32 > 0)
        {
            UpdateLastLogin(resultInt32);
        }

        return resultInt32;
    }

    public static async void RegisterHandler(AccountDto registerModel, NpgsqlConnection conn)
    {
        await using var cmd = new NpgsqlCommand("INSERT INTO accounts " +
                                                "(username, password, email, country, created_on, last_login) VALUES " +
                                                "($1, $2, $3, $4, $5, $6);", conn)
        {
            Parameters =
            {
                new NpgsqlParameter { Value = registerModel.Username },
                new NpgsqlParameter { Value = registerModel.Password },
                new NpgsqlParameter { Value = registerModel.Email },
                new NpgsqlParameter { Value = "" },
                new NpgsqlParameter { Value = DateTime.UtcNow },
                new NpgsqlParameter { Value = DateTime.UtcNow }
            }
        };
        var result = await cmd.ExecuteNonQueryAsync();
        Console.WriteLine(result);
    }

    public static async Task<AccountDto> GetAccountDataById(int userId)
    {
        var conn = Globals.GetDatabaseConnection().Result;
        var account = new AccountDto();
        
        await using var cmd = new NpgsqlCommand("SELECT * FROM accounts WHERE id = " + userId, conn);
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

    public static void EditAccount(AccountDto settingsModel)
    {
        var conn = Globals.GetDatabaseConnection().Result;
        using var cmd = new NpgsqlCommand("UPDATE accounts SET username = $1, password = $2, " +
                                          "email = $3, country = $4 WHERE id = $5;", conn)
        {
           Parameters =
           {
               new NpgsqlParameter { Value = settingsModel.Username },
               new NpgsqlParameter { Value = settingsModel.Password },
               new NpgsqlParameter { Value = settingsModel.Email },
               new NpgsqlParameter { Value = settingsModel.Country },
               new NpgsqlParameter { Value = settingsModel.Id}
           }
        };
        var result = cmd.ExecuteNonQueryAsync().Result;
        Console.WriteLine(result);
    }

    private static void UpdateLastLogin(int userId)
    {
        var conn = Globals.GetDatabaseConnection().Result;
        using var cmd = new NpgsqlCommand("UPDATE accounts SET last_login = $1 WHERE id = $2;", conn)
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