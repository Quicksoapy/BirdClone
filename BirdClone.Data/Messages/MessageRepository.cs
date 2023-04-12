using BirdClone.Domain;
using BirdClone.Domain.Messages;
using Npgsql;

namespace BirdClone.Data.Messages;

public class MessageRepository : IMessageRepository
{
    private readonly string _connectionString;

    public MessageRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public static async void PostMessageHandler(MessageDto messageModel, NpgsqlConnection conn)
    {
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
    
    public static async Task<IEnumerable<MessageDto>> GetMessagesHandler()
    {
        var conn = BirdClone.Globals.GetDatabaseConnection().Result;
        var messageModels = new List<MessageDto>();
        
        await using var cmd = new NpgsqlCommand("SELECT messages.id, messages.content, messages.user_id, messages.created_on, accounts.username FROM messages JOIN accounts ON messages.user_id = accounts.id ORDER BY messages.created_on DESC", conn);
        var dataReader = await cmd.ExecuteReaderAsync();
        while (dataReader.Read())
        {
            var model = new MessageDto
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

    public static async Task<IEnumerable<MessageDto>> GetMessagesOfUserById(int userId, NpgsqlConnection conn)
    {
        var messageModels = new List<MessageDto>();
        
        await using var cmd = new NpgsqlCommand("SELECT * FROM messages WHERE user_id = @userId ORDER BY created_on DESC", conn)
            {
                Parameters = {
                    new NpgsqlParameter { ParameterName = "userId", Value = userId }
                }
            };
        
        var dataReader = await cmd.ExecuteReaderAsync();
        while (dataReader.Read())
        {
            var model = new MessageDto
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

    void IMessageRepository.PostMessageHandler(MessageDto messageModel, NpgsqlConnection conn)
    {
        PostMessageHandler(messageModel, conn);
    }

    public Task<IEnumerable<MessageDto>> GetMessagesHandler(NpgsqlConnection conn)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<MessageDto>> IMessageRepository.GetMessagesOfUserById(int userId, NpgsqlConnection conn)
    {
        return GetMessagesOfUserById(userId, conn);
    }
}