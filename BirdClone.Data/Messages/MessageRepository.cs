using BirdClone.Domain;
using BirdClone.Domain.Messages;
using Npgsql;

namespace BirdClone.Data.Messages;

public class MessageRepository : IMessageRepository
{
    private readonly string _connectionString;

    public MessageRepository()
    {
        var globals = new Globals();
        _connectionString = globals.GetDatabaseConnectionString();
    }

    public int PostMessageHandler(MessageDto messageModel)
    {
        var conn = new NpgsqlConnection(_connectionString);
        conn.Open();
        
        using var cmd = new NpgsqlCommand("INSERT INTO messages " +
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
        var result = cmd.ExecuteNonQuery();
        return result;
    }

    public IEnumerable<MessageDto> GetMessagesHandler()
    {
        var messageModels = new List<MessageDto>();
        var conn = new NpgsqlConnection(_connectionString);
        conn.Open();
        
        using var cmd = new NpgsqlCommand("SELECT messages.id, messages.content, messages.user_id, messages.created_on, accounts.username FROM messages JOIN accounts ON messages.user_id = accounts.id ORDER BY messages.created_on DESC", conn);
        var dataReader = cmd.ExecuteReader();
        while (dataReader.Read())
        {
            var model = new MessageDto((uint)dataReader.GetInt64(dataReader.GetOrdinal("id")))
                .WithUserId(dataReader.GetInt32(dataReader.GetOrdinal("user_id")))
                .WithUsername(dataReader.GetString(dataReader.GetOrdinal("username")))
                .WithContent(dataReader.GetString(dataReader.GetOrdinal("content")))
                .WithCreatedOn(dataReader.GetDateTime(dataReader.GetOrdinal("created_on")));
            
            messageModels.Add(model);
        }

        return messageModels;
    }

    public IEnumerable<MessageDto> GetMessagesOfUserById(int userId)
    {
        var messageModels = new List<MessageDto>();
        var conn = new NpgsqlConnection(_connectionString);
        conn.Open();
        
        using var cmd = new NpgsqlCommand("SELECT * FROM messages WHERE user_id = @userId ORDER BY created_on DESC", conn)
            {
                Parameters = {
                    new NpgsqlParameter { ParameterName = "userId", Value = userId }
                }
            };
        
        var dataReader = cmd.ExecuteReader();
        while (dataReader.Read())
        {
            var model = new MessageDto((uint)dataReader.GetInt64(dataReader.GetOrdinal("id")))
                .WithUserId(dataReader.GetInt32(dataReader.GetOrdinal("user_id")))
                .WithUsername(dataReader.GetString(dataReader.GetOrdinal("username")))
                .WithContent(dataReader.GetString(dataReader.GetOrdinal("content")))
                .WithCreatedOn(dataReader.GetDateTime(dataReader.GetOrdinal("created_on")));
            
            messageModels.Add(model);
        }

        return messageModels;
    }

    public bool UserExist(int userId, string username)
    {
        var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("select exists(select id from accounts where id= $1 and username= $2);", conn)
        {
            Parameters =
            {
                new NpgsqlParameter { Value = userId },
                new NpgsqlParameter { Value = username}
            }
        };
        var result = cmd.ExecuteScalar();

        if (result is not bool x) return false;
        if ((bool?)x == true)
        {
            return true;
        }
        return false;
    }
}