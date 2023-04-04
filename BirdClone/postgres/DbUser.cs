using BirdClone.Models;
using Npgsql;

namespace BirdClone.postgres;

public class DbUser
{
    private static readonly NpgsqlConnection Conn = DbGlobals.GetDatabaseConnection().Result;
    
    public static async Task<int> LoginHandler(string username, string password)
    {
        await using var cmd =
            new NpgsqlCommand(
                "SELECT Id FROM accounts WHERE username='" + username + "' AND password='" + password + "';", Conn);
        var result = await cmd.ExecuteScalarAsync();
        
        var resultInt32 = Convert.ToInt32(result);
        if (resultInt32 > 0)
        {
            UpdateLastLogin(resultInt32);
        }

        return resultInt32;
    }

    public static async void RegisterHandler(RegisterModel registerModel)
    {
        var conn = DbGlobals.GetDatabaseConnection().Result;

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

    public static async Task<AccountModel> GetAccountDataById(int userId)
    {
        var conn = DbGlobals.GetDatabaseConnection().Result;
        var account = new AccountModel();
        
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

    public static void EditAccount(AccountModel settingsModel)
    {
        var conn = DbGlobals.GetDatabaseConnection().Result;
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
        var conn = DbGlobals.GetDatabaseConnection().Result;
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