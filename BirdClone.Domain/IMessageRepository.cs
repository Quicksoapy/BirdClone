using BirdClone.Domain.Messages;
using Npgsql;

namespace BirdClone.Domain;

public interface IMessageRepository
{
    void PostMessageHandler(MessageDto messageModel, NpgsqlConnection conn);

    Task<IEnumerable<MessageDto>> GetMessagesHandler(NpgsqlConnection conn);

    Task<IEnumerable<MessageDto>> GetMessagesOfUserById(int userId, NpgsqlConnection conn);
}