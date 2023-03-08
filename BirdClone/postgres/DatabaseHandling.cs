using Newtonsoft.Json;
using Npgsql;

namespace BirdClone.postgres;

public class DatabaseHandling
{
    public static async Task<NpgsqlConnection> GetConnection()
    {
        var databaseLogin = JsonConvert.DeserializeObject<DatabaseLogin>(File.ReadAllText("DatabaseLogin.json"));
        
        string server = databaseLogin.server;
        string username = databaseLogin.username;
        string password = databaseLogin.password;
        string database = databaseLogin.database;
        string port = databaseLogin.port;
        await using var conn =
            new NpgsqlConnection(
                "Host=" + server + ";Username=" + username+";Password=" + password+";Database=" + database+";Port="+port);
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
    
    public async void RegisterHandler(string username, string hashedPassword, string email)
    {
        //var conn = GetConnection().Result;
        //await conn.OpenAsync();
        //"Cannot access a disposed object"
        
        var databaseLogin = JsonConvert.DeserializeObject<DatabaseLogin>(File.ReadAllText("DatabaseLogin.json"));
        
        string server = databaseLogin.server;
        string usernameDb = databaseLogin.username;
        string password = databaseLogin.password;
        string database = databaseLogin.database;
        string port = databaseLogin.port;
        await using var conn =
            new NpgsqlConnection(
                "Host=" + server + ";Username=" + usernameDb+";Password=" + password+";Database=" + database+";Port="+port);
        await conn.OpenAsync();
        
        await using var cmd = new NpgsqlCommand("INSERT INTO accounts " +
                                                "(username, password, email, country, created_on, last_login) VALUES " +
                                                "($1, $2, $3, $4, $5, $6);", conn){
            Parameters =
            {
                new NpgsqlParameter {Value = username},
                new NpgsqlParameter {Value = hashedPassword},
                new NpgsqlParameter {Value = email},
                new NpgsqlParameter {Value = ""},
                new NpgsqlParameter {Value = DateTime.UtcNow},
                new NpgsqlParameter {Value = DateTime.UtcNow}
            }
        };
        var result = await cmd.ExecuteNonQueryAsync();
        Console.WriteLine(result);
    }
}