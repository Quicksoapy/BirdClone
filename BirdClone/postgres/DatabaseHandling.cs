using Npgsql;

namespace BirdClone.postgres;

public class DatabaseHandling
{
    public async void RegisterHandler(string username, string hashedPassword)
    {
        await using var cmd = new NpgsqlCommand("INSERT INTO accounts " +
                                                "(username, password, email, country, created_on, last_login) VALUES " +
                                                "($1, $2, $3, $4, $5, $6);", Globals.GetConnection().Result){
            Parameters =
            {
                new() {Value = username},
                new() {Value = hashedPassword},
                new() {Value = ""},
                new () {Value = ""},
                new() {Value = DateTime.UtcNow},
                new() {Value = DateTime.UtcNow}
            }
        };
        var result = await cmd.ExecuteNonQueryAsync();
    }
}