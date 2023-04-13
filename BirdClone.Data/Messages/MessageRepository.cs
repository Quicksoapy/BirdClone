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
    
    public async void PostMessageHandler(MessageDto messageModel)
    {
        await using var cmd = new NpgsqlCommand("INSERT INTO messages " +
                                                "(user_id, content, created_on) VALUES " +
                                                "($1, $2, $3);", new NpgsqlConnection(_connectionString))
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

    public async Task<IEnumerable<MessageDto>> GetMessagesHandler()
    {
        var messageModels = new List<MessageDto>();
        
        await using var cmd = new NpgsqlCommand("SELECT messages.id, messages.content, messages.user_id, messages.created_on, accounts.username FROM messages JOIN accounts ON messages.user_id = accounts.id ORDER BY messages.created_on DESC", new NpgsqlConnection(_connectionString));
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

    public async Task<IEnumerable<MessageDto>> GetMessagesOfUserById(int userId)
    {
        var messageModels = new List<MessageDto>();
        
        await using var cmd = new NpgsqlCommand("SELECT * FROM messages WHERE user_id = @userId ORDER BY created_on DESC", new NpgsqlConnection(_connectionString))
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
}