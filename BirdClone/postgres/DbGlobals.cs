using BirdClone.Models;
using Newtonsoft.Json;
using Npgsql;

namespace BirdClone.postgres;

public class DbGlobals
{
    public async Task<NpgsqlConnection> GetDatabaseConnection()
    {
        var databaseLogin =
            JsonConvert.DeserializeObject<DatabaseLoginModel>(await File.ReadAllTextAsync("DatabaseLogin.json"));

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
}