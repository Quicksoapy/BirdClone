using BirdClone.Domain.Messages;
using Npgsql;

namespace BirdClone.Domain;

public interface IMessageRepository
{
    void PostMessageHandler(MessageDto messageModel);

    Task<IEnumerable<MessageDto>> GetMessagesHandler();

    Task<IEnumerable<MessageDto>> GetMessagesOfUserById(int userId);
}