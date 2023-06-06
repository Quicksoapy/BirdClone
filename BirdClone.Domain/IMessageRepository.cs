using BirdClone.Domain.Messages;
using Npgsql;

namespace BirdClone.Domain;

public interface IMessageRepository
{
    int PostMessageHandler(MessageDto messageModel);

    IEnumerable<MessageDto> GetMessagesHandler();

    IEnumerable<MessageDto> GetMessagesOfUserById(int userId);
    IEnumerable<RepostDto> GetRepostsOfUserById(int userId);
    bool UserExist(int userId, string username);
}