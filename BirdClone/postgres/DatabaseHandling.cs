using BirdClone.Models;
using BirdClone.Pages;
using Newtonsoft.Json;
using Npgsql;

namespace BirdClone.postgres;

public class DatabaseHandling
{
    private static async Task<NpgsqlConnection> GetConnection()
    {
        var databaseLogin =
            JsonConvert.DeserializeObject<DatabaseLogin>(await File.ReadAllTextAsync("DatabaseLogin.json"));

        string server = databaseLogin.server;
        string username = databaseLogin.username;
        string password = databaseLogin.password;
        string database = databaseLogin.database;
        string port = databaseLogin.port;
        var conn =
            new NpgsqlConnection(
                "Host=" + server + ";Username=" + username + ";Password=" + password + ";Database=" + database +
                ";Port=" + port);
        await conn.OpenAsync();
        return conn;
    }

    public class DatabaseLogin
    {
        public string server { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string database { get; set; }

        public string port { get; set; }
    }

    public async Task<int> LoginHandler(string username, string password)
    {
        var conn = GetConnection().Result;
        //"Cannot access a disposed object"

        await using var cmd =
            new NpgsqlCommand(
                "SELECT Id FROM accounts WHERE username='" + username + "' AND password='" + password + "';", conn);
        var result = await cmd.ExecuteScalarAsync();
        Console.WriteLine(result);
        return Convert.ToInt32(result);
    }

    public async void RegisterHandler(RegisterModel registerModel)
    {
        var conn = GetConnection().Result;

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

    public async void PostMessageHandler(MessageModel messageModel)
    {
        var conn = GetConnection().Result;

        await using var cmd = new NpgsqlCommand("INSERT INTO messages " +
                                                "(user_id, content, created_on) VALUES " +
                                                "($1, $2, $3);", conn)
        {
            Parameters =
            {
                new NpgsqlParameter { Value = messageModel.UserId },
                new NpgsqlParameter { Value = messageModel.Content },
                new NpgsqlParameter { Value = messageModel.CreatedOn },
            }
        };
        var result = await cmd.ExecuteNonQueryAsync();
        Console.WriteLine(result);
    }
    
    public static async Task<List<MessageModel>> GetMessagesHandler()
    {
        var conn = GetConnection().Result;
        var messageModels = new List<MessageModel>();
        
        await using var cmd = new NpgsqlCommand("SELECT messages.id, messages.content, messages.user_id, messages.created_on, accounts.username FROM messages JOIN accounts ON messages.user_id = accounts.id ORDER BY messages.created_on DESC", conn);
        var dataReader = await cmd.ExecuteReaderAsync();
        while (dataReader.Read())
        {
            var model = new MessageModel();
            model.Id = (uint)dataReader.GetInt64(dataReader.GetOrdinal("id"));
            model.UserId = dataReader.GetInt32(dataReader.GetOrdinal("user_id"));
            model.Content = dataReader.GetString(dataReader.GetOrdinal("content"));
            model.CreatedOn = dataReader.GetDateTime(dataReader.GetOrdinal("created_on"));
            model.Username = dataReader.GetString(dataReader.GetOrdinal("username"));
            messageModels.Add(model);
        }

        return messageModels;
    }

    public async Task<List<MessageModel>> GetMessagesOfUserById(int userId)
    {
        var conn = GetConnection().Result;
        var messageModels = new List<MessageModel>();
        
        await using var cmd = new NpgsqlCommand("SELECT * FROM messages WHERE user_id = @userId ORDER BY created_on DESC", conn)
            {
                Parameters = {
                    new NpgsqlParameter { ParameterName = "userId", Value = userId }
                }
            };
        
        var dataReader = await cmd.ExecuteReaderAsync();
        while (dataReader.Read())
        {
            var model = new MessageModel();
            model.Id = (uint)dataReader.GetInt64(dataReader.GetOrdinal("id"));
            model.UserId = dataReader.GetInt32(dataReader.GetOrdinal("user_id"));
            model.Content = dataReader.GetString(dataReader.GetOrdinal("content"));
            model.CreatedOn = dataReader.GetDateTime(dataReader.GetOrdinal("created_on"));
            messageModels.Add(model);
        }

        return messageModels;
    }
    
    public async Task<AccountModel> GetAccountDataById(int userId)
    {
        var conn = GetConnection().Result;
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

    public void EditAccount(AccountModel settingsModel)
    {
        var conn = GetConnection().Result;
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

    public void UpdateLastLogin(int userId)
    {
        var conn = GetConnection().Result;
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