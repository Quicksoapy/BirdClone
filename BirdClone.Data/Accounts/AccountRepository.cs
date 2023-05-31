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

    public int LoginHandler(string username, string password)
    {
        var conn = new NpgsqlConnection(_connectionString);
        conn.Open();
        
        using var cmd =
            new NpgsqlCommand(
                "SELECT Id FROM accounts WHERE username='" + username + "' AND password='" + password + "';", conn);
        var result = cmd.ExecuteScalar();
        
        var resultInt32 = Convert.ToInt32(result);
        if (resultInt32 > 0)
        {
            UpdateLastLogin(resultInt32);
        }

        return resultInt32;
    }

    public void RegisterHandler(AccountDto accountDto)
    {
        var conn = new NpgsqlConnection(_connectionString);
        conn.Open();
        
        using var cmd = new NpgsqlCommand("INSERT INTO accounts " +
                                                "(username, password, email, country, created_on, last_login) VALUES " +
                                                "($1, $2, $3, $4, $5, $6);", conn)
        {
            Parameters =
            {
                new NpgsqlParameter { Value = accountDto.Username },
                new NpgsqlParameter { Value = accountDto.Password },
                new NpgsqlParameter { Value = accountDto.Email },
                new NpgsqlParameter { Value = "" },
                new NpgsqlParameter { Value = DateTime.UtcNow },
                new NpgsqlParameter { Value = DateTime.UtcNow }
            }
        };
        var result = cmd.ExecuteNonQuery();
        Console.WriteLine(result);
    }

    public Account GetAccountDataById(int userId)
    {
        Account account = null;
        var conn = new NpgsqlConnection(_connectionString);
        conn.Open();
        
        using var cmd = new NpgsqlCommand("SELECT * FROM accounts WHERE id = " + userId, conn);
        var dataReader = cmd.ExecuteReader();
        while (dataReader.Read())
        {
            account = new Account(dataReader.GetInt32(dataReader.GetOrdinal("id")))
                .WithUsername(dataReader.GetString(dataReader.GetOrdinal("username")))
                .WithEmail(dataReader.GetString(dataReader.GetOrdinal("email")))
                .WithCountry(dataReader.GetString(dataReader.GetOrdinal("country")))
                .WithCreatedOn(dataReader.GetDateTime(dataReader.GetOrdinal("created_on")))
                .WithLastLogin(dataReader.GetDateTime(dataReader.GetOrdinal("last_login")));
        }

        return account;
    }

    public void EditAccount(AccountDto account)
    {
        var conn = new NpgsqlConnection(_connectionString);
        conn.Open();
        
        var cmd = new NpgsqlCommand("UPDATE accounts SET username = $1, password = $2, " +
                                          "email = $3, country = $4 WHERE id = $5;", conn)
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
        var result = cmd.ExecuteNonQuery();
        Console.WriteLine(result);
    }

    public bool AccountExist(AccountDto account)
    {
        var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("select exists(select id from accounts where id= $1);", conn)
        {
            Parameters =
            {
                new NpgsqlParameter { Value = account.Id }
            }
        };
        var result = cmd.ExecuteScalar();

        if (result is not bool x) return false;
        if ((bool?)x == true)
        {
            return true;
        }
        return false;
    }

    public bool EmailExist(string email)
    {
        var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("select exists(select id from accounts where email= $1);", conn)
        {
            Parameters =
            {
                new NpgsqlParameter { Value = email }
            }
        };
        var result = cmd.ExecuteScalar();

        if (result is not bool x) return false;
        if ((bool?)x == true)
        {
            return true;
        }
        return false;
    }

    public void UpdateLastLogin(int userId)
    {
        var conn = new NpgsqlConnection(_connectionString);
        conn.Open();
        
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