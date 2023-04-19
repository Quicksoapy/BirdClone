using Newtonsoft.Json;
using Npgsql;

namespace BirdClone.Data;

public class Globals
{
    public string GetDatabaseConnectionString()
    {
        var databaseLogin =
            JsonConvert.DeserializeObject<DatabaseLoginModel>(File.ReadAllText("./DatabaseLogin.json"));

        string server = databaseLogin.server;
        string username = databaseLogin.username;
        string password = databaseLogin.password;
        string database = databaseLogin.database;
        string port = databaseLogin.port;
        var conn =
            "Host=" + server + ";Username=" + username + ";Password=" + password + ";Database=" + database +
            ";Port=" + port;
        return conn;
    }
    
    public class DatabaseLoginModel
    {
        public string server { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string database { get; set; }

        public string port { get; set; }
    }
}