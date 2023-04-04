using BirdClone.Models;
using Npgsql;

namespace BirdClone.postgres;

public class DbMessages
{
    private static readonly NpgsqlConnection Conn = DbGlobals.GetDatabaseConnection().Result;
    
    public static async void PostMessageHandler(MessageModel messageModel)
    {
        await using var cmd = new NpgsqlCommand("INSERT INTO messages " +
                                                "(user_id, content, created_on) VALUES " +
                                                "($1, $2, $3);", Conn)
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
        var messageModels = new List<MessageModel>();
        
        await using var cmd = new NpgsqlCommand("SELECT messages.id, messages.content, messages.user_id, messages.created_on, accounts.username FROM messages JOIN accounts ON messages.user_id = accounts.id ORDER BY messages.created_on DESC", Conn);
        var dataReader = await cmd.ExecuteReaderAsync();
        while (dataReader.Read())
        {
            var model = new MessageModel
            {
                Id = (uint)dataReader.GetInt64(dataReader.GetOrdinal("id")),
                UserId = dataReader.GetInt32(dataReader.GetOrdinal("user_id")),
                Content = dataReader.GetString(dataReader.GetOrdinal("content")),
                CreatedOn = dataReader.GetDateTime(dataReader.GetOrdinal("created_on")),
                Username = dataReader.GetString(dataReader.GetOrdinal("username"))
            };
            messageModels.Add(model);
        }

        return messageModels;
    }

    public static async Task<List<MessageModel>> GetMessagesOfUserById(int userId)
    {
        var messageModels = new List<MessageModel>();
        
        await using var cmd = new NpgsqlCommand("SELECT * FROM messages WHERE user_id = @userId ORDER BY created_on DESC", Conn)
            {
                Parameters = {
                    new NpgsqlParameter { ParameterName = "userId", Value = userId }
                }
            };
        
        var dataReader = await cmd.ExecuteReaderAsync();
        while (dataReader.Read())
        {
            var model = new MessageModel
            {
                Id = (uint)dataReader.GetInt64(dataReader.GetOrdinal("id")),
                UserId = dataReader.GetInt32(dataReader.GetOrdinal("user_id")),
                Content = dataReader.GetString(dataReader.GetOrdinal("content")),
                CreatedOn = dataReader.GetDateTime(dataReader.GetOrdinal("created_on"))
            };
            messageModels.Add(model);
        }

        return messageModels;
    }
}