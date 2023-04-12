using System.Collections;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Npgsql;

namespace BirdClone;

public class Globals
{
    public static string GetSha512(string input)
    {
        var hash = "";
        var alg = SHA512.Create();
        var result = alg.ComputeHash(Encoding.UTF8.GetBytes(input));
        hash = Convert.ToBase64String(result);
        return hash;
    }
    
    public static async Task<NpgsqlConnection> GetDatabaseConnection()
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
}