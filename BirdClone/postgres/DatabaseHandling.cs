using BirdClone.Models;
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
    
    public async Task<List<MessageModel>> GetMessagesHandler()
    {
        var conn = GetConnection().Result;
        var messageModels = new List<MessageModel>();
        
        await using var cmd = new NpgsqlCommand("SELECT messages.id, messages.content, messages.user_id, messages.created_on, accounts.username FROM messages JOIN accounts ON messages.user_id = accounts.id", conn);
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
    
}