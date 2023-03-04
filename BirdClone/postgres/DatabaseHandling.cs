using Npgsql;

namespace BirdClone.postgres;

public class DatabaseHandling
{
    public void RegisterHandler(string username, string hashedPassword)
    {
        
        var connection = Globals.GetConnection().Result;
        var insertQuery =
            "INSERT INTO accounts (username, password, email, country, created_on, last_login) VALUES ('@name', '@password', '@email', '@country', '@created_on', '@last_login');";
        using NpgsqlCommand command = new NpgsqlCommand(insertQuery, connection);
        command.Parameters.AddWithValue("@name", username);
        command.Parameters.AddWithValue("@password", hashedPassword);
        command.Parameters.AddWithValue("@email", "");
        command.Parameters.AddWithValue("@country", "");
        command.Parameters.AddWithValue("@created_on", DateTime.UtcNow);
        command.Parameters.AddWithValue("@last_login", DateTime.UtcNow);
        Console.WriteLine(command.CommandText);
        var result = command.ExecuteNonQueryAsync();
        Console.WriteLine(result.Status);
        
    }
}